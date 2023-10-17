﻿using Newtonsoft.Json.Linq;
using PokemonDataGenerator.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDataGenerator.Pokedex
{
	public static class PokemonProfileGenerator
	{
		private class MovesetSettings
		{
			private static MovesetSettings s_VanillaSettings;
			private static MovesetSettings s_ExSettings;

			public List<string> levelUpPreference = new List<string>();
			public Dictionary<string, string[]> allowedTutorMoves = new Dictionary<string, string[]>();

			public static MovesetSettings VanillaSettings
			{
				get
				{
					if(s_VanillaSettings == null)
					{
						s_VanillaSettings = new MovesetSettings();

						// Priority order list of level up moves
						s_VanillaSettings.levelUpPreference.Add("rogue");
						s_VanillaSettings.levelUpPreference.Add("emerald");
						s_VanillaSettings.levelUpPreference.Add("firered-leafgreen");

						// Orderless versions we will base tutor moves off
						s_VanillaSettings.allowedTutorMoves.Add("rogue", new[] { "rogue", "emerald", "firered-leafgreen", "xd", "colosseum" });
						s_VanillaSettings.allowedTutorMoves.Add("emerald", new[] { "rogue", "emerald", "firered-leafgreen", "xd", "colosseum" });
						s_VanillaSettings.allowedTutorMoves.Add("firered-leafgreen", new[] { "rogue", "emerald", "firered-leafgreen", "xd", "colosseum" });
					}
					return s_VanillaSettings;
				}
			}

			public static MovesetSettings ExSettings
			{
				get
				{
					if (s_ExSettings == null)
					{
						s_ExSettings = new MovesetSettings();

						// Priority order list of level up moves
						s_ExSettings.levelUpPreference.Add("rogue");
						s_ExSettings.levelUpPreference.Add("sword-shield");
						s_ExSettings.levelUpPreference.Add("ultra-sun-ultra-moon");
						s_ExSettings.levelUpPreference.Add("scarlet-violet");

						// Orderless versions we will base tutor moves off
						s_ExSettings.allowedTutorMoves.Add("rogue", new[] { "rogue", "sword-shield", "ultra-sun-ultra-moon" });
						s_ExSettings.allowedTutorMoves.Add("sword-shield", new[] { "rogue", "sword-shield" });
						s_ExSettings.allowedTutorMoves.Add("ultra-sun-ultra-moon", new[] { "rogue", "ultra-sun-ultra-moon" });
						s_ExSettings.allowedTutorMoves.Add("scarlet-violet", new[] { "rogue", "scarlet-violet" });

						// Just needed for cosplay pikachu
						s_ExSettings.levelUpPreference.Add("omega-ruby-alpha-sapphire");
						s_ExSettings.allowedTutorMoves.Add("omega-ruby-alpha-sapphire", new[] { "rogue", "omega-ruby-alpha-sapphire" });
					}
					return s_ExSettings;
				}
			}

			public void RemoveInvalidMoves(List<MoveInfo> moves)
			{
				// Find out which game we're going to base the level up moveset off
				int levelUpIndex = int.MaxValue;

				foreach(var move in moves)
				{
					int versionIndex = levelUpPreference.IndexOf(move.versionName);
					if(versionIndex != -1 && versionIndex < levelUpIndex)
					{
						levelUpIndex = versionIndex;

						// Found ideal preference
						if (levelUpIndex == 0)
							break;
					}
				}

				if (levelUpIndex >= levelUpPreference.Count)
					throw new NotImplementedException("Unable to find valid level up moveset version");

				string moveGroupName = levelUpPreference[levelUpIndex];

				moves.RemoveAll((move) =>
				{
					if (move.originMethod == MoveInfo.LearnMethod.LevelUp)
					{
						return move.versionName != moveGroupName;
					}
					else
					{
						// Treat all other moves as tutor moves
						return !allowedTutorMoves[moveGroupName].Contains(move.versionName);
					}

				});
			}
		}


		private struct MoveInfo
		{
			public enum LearnMethod
			{
				Invalid,
				Egg,
				TM,
				LevelUp,
				Tutor,
			}

			public LearnMethod originMethod;
			public int learnLevel;
			public string moveName;
			public string versionName;
		}

		private class PokemonProfile
		{
			public string Species;
			public List<MoveInfo> Moves;
			public List<PokemonCompetitiveSet> CompetitiveSets;
			public string[] Types = new string[2];
			public string[] Abilities = new string[3];

			public PokemonProfile(string name)
			{
				Species = name;
				Moves = new List<MoveInfo>();
				CompetitiveSets = new List<PokemonCompetitiveSet>();

				for (int i = 0; i < Types.Length; ++i)
					Types[i] = "none";

				for (int i = 0; i < Abilities.Length; ++i)
					Abilities[i] = "none";
			}

			public string PrettySpeciesName
			{ 
				get
				{
					string name = "";
					foreach(var word in Species.Split('_').Skip(1))
					{
						name += word.Substring(0, 1).ToUpper();
						name += word.Substring(1).ToLower();
					}

					return name;
				}
			}

			public void CollapseForExport()
			{
				//Moves.RemoveAll((m) => PokemonMoveHelpers.IsMoveUnsupported(m.moveName));

				var movesetPreferences = (GameDataHelpers.IsVanillaVersion ? MovesetSettings.VanillaSettings : MovesetSettings.ExSettings);
				movesetPreferences.RemoveInvalidMoves(Moves);

				// Simplify the definitions now and then we'll remove any duplicates
				List<MoveInfo> newMoves = new List<MoveInfo>();

				foreach(var oldMove in Moves)
				{
					MoveInfo newMove = oldMove;
					newMove.moveName = FormatMoveName(newMove.moveName, true);
					newMove.versionName = "rogue";

					if (IsBannedMove(newMove.moveName))
						continue;

					if (!GameDataHelpers.MoveDefines.ContainsKey(newMove.moveName))
						throw new InvalidDataException();

					if (newMove.originMethod == MoveInfo.LearnMethod.LevelUp)
					{
						var existingLevelUpMove = newMoves.Where(m => m.moveName == newMove.moveName && m.originMethod == MoveInfo.LearnMethod.LevelUp).FirstOrDefault();

						// If we already have the same level up move only take it at the lowest learn level
						if (existingLevelUpMove.moveName == newMove.moveName)
						{
							existingLevelUpMove.learnLevel = Math.Min(existingLevelUpMove.learnLevel, newMove.learnLevel);
							continue;
						}
					}
					else
					{
						newMove.originMethod = MoveInfo.LearnMethod.Tutor;

						var anyLevelUpMove = Moves.Where(m => m.moveName == newMove.moveName && m.originMethod == MoveInfo.LearnMethod.LevelUp).FirstOrDefault();
						if (anyLevelUpMove.moveName == newMove.moveName)
						{
							// If we learn this as a level up move, don't include an entry as a tutor move
							continue;
						}
					}

					if (!newMoves.Contains(newMove))
						newMoves.Add(newMove);
				}

				Moves = newMoves;

				// Now we've added the sets, add any moves that we can't currently learn as tutor moves
				foreach(var set in CompetitiveSets)
				{
					foreach(var move in set.Moves)
					{
						bool canLearnMove = Moves.Where(m => m.moveName == move).Any();
						if(!canLearnMove)
						{
							MoveInfo newMove = new MoveInfo();
							newMove.moveName = move;
							newMove.versionName = "rogue";
							newMove.originMethod = MoveInfo.LearnMethod.Tutor;
							Moves.Add(newMove);
						}
					}
				}

				Moves = Moves.OrderBy((m) => m.originMethod == MoveInfo.LearnMethod.LevelUp ? m.learnLevel.ToString("000") : "999" + m.moveName).ToList();
			}

			public HashSet<string> GetMoveSourceVerions()
			{
				HashSet<string> versions = new HashSet<string>();
				foreach(var move in Moves)
				{
					versions.Add(move.versionName);
				}

				return versions;
			}

			private static IEnumerable<MoveInfo> EnsureMovesUnique(IEnumerable<MoveInfo> moves)
			{
				HashSet<string> visitedMoves = new HashSet<string>();

				foreach(var move in moves)
				{
					if(!visitedMoves.Contains(move.moveName))
					{
						visitedMoves.Add(move.moveName);
						yield return move;
					}
				}
			}

			public IEnumerable<MoveInfo> GetLevelUpMoves()
			{
				return EnsureMovesUnique(Moves.Where((m) => m.originMethod == MoveInfo.LearnMethod.LevelUp).OrderBy((m) => m.learnLevel));
			}

			public IEnumerable<MoveInfo> GetEggMoves()
			{
				return EnsureMovesUnique(Moves.Where((m) => m.originMethod == MoveInfo.LearnMethod.Egg));
			}

			public IEnumerable<MoveInfo> GetTMHMMoves()
			{
				return EnsureMovesUnique(Moves.Where((m) => GameDataHelpers.MoveToTMHMItem.ContainsKey(FormatKeyword(m.moveName))));
			}

			public IEnumerable<MoveInfo> GetTutorMoves()
			{
				return EnsureMovesUnique(Moves.Where((m) => GameDataHelpers.TutorMoveDefines.ContainsKey(FormatKeyword(m.moveName))));
			}

			public IEnumerable<MoveInfo> GetLeftoverMoves()
			{
				return EnsureMovesUnique(Moves.Where((m) =>
					m.originMethod != MoveInfo.LearnMethod.LevelUp &&
					m.originMethod != MoveInfo.LearnMethod.Egg &&
					GameDataHelpers.MoveToTMHMItem.ContainsKey(FormatKeyword(m.moveName)) &&
					GameDataHelpers.TutorMoveDefines.ContainsKey(FormatKeyword(m.moveName)))
				);
			}
		}

		private class PokemonCompetitiveSet
		{
			public List<string> Moves = new List<string>();
			public string Ability;
			public string Item;
			public string Nature;
			public string HiddenPower;
			public List<string> SourceTiers = new List<string>();

			public bool IsCompatibleWith(PokemonCompetitiveSet other)
			{
				if (Ability != other.Ability)
					return false;
				if (Item != other.Item)
					return false;
				if (Nature != other.Nature)
					return false;
				if (HiddenPower != other.HiddenPower)
					return false;

				if (Moves.Count != other.Moves.Count)
					return false;

				for(int i = 0; i < Moves.Count; ++i)
				{
					if (Moves[i] != other.Moves[i])
						return false;
				}

				return true;
			}

			public static PokemonCompetitiveSet ParseFrom(string sourceTier, JObject json)
			{
				PokemonCompetitiveSet output = new PokemonCompetitiveSet();
				output.SourceTiers.Add(sourceTier);

				string ability = json["ability"].Value<string>();
				if (ability != "No Ability")
				{
					output.Ability = "ABILITY_" + GameDataHelpers.FormatKeyword(ability);

					switch(output.Ability)
					{
						case "ABILITY_AS_ONE_(GLASTRIER)":
							output.Ability = "ABILITY_AS_ONE_ICE_RIDER";
							break;
						case "ABILITY_AS_ONE_(SPECTRIER)":
							output.Ability = "ABILITY_AS_ONE_SHADOW_RIDER";
							break;
					}

					if (!GameDataHelpers.AbilityDefines.ContainsKey(output.Ability))
						throw new InvalidDataException();
				}

				if (json.ContainsKey("item"))
				{
					output.Item = FormatItemName(json["item"].Value<string>());

					if (!GameDataHelpers.ItemDefines.ContainsKey(output.Item))
						throw new InvalidDataException();
				}

				if (json.ContainsKey("nature"))
				{
					output.Nature = "NATURE_" + GameDataHelpers.FormatKeyword(json["nature"].Value<string>());

					if (!GameDataHelpers.NatureDefines.ContainsKey(output.Nature))
						throw new InvalidDataException();
				}

				foreach (var move in json["moves"])
				{
					string moveName = move.Value<string>();

					if(moveName.StartsWith("Hidden Power", StringComparison.CurrentCultureIgnoreCase))
					{
						output.HiddenPower = moveName.Substring("Hidden Power".Length).Trim();
						output.HiddenPower = "TYPE_" + GameDataHelpers.FormatKeyword(output.HiddenPower);
						moveName = "Hidden Power";

						if (!GameDataHelpers.TypesDefines.ContainsKey(output.HiddenPower))
							throw new InvalidDataException();
					}

					moveName = FormatMoveName(moveName, false);

					if (!IsBannedMove(moveName))
					{
						output.Moves.Add(moveName);

						if (!GameDataHelpers.MoveDefines.ContainsKey(moveName))
							throw new InvalidDataException();
					}
				}

				return output;
			}
		}

		private static string FormatMoveName(string moveName, bool fromLearnsets)
		{
			// Fix some spellings
			if (GameDataHelpers.IsVanillaVersion)
			{
				switch (moveName.ToLower().Replace("-", " "))
				{
					case "feint attack":
						moveName = "faint attack";
						break;

					case "high jump kick":
						moveName = "hi jump kick";
						break;

					case "smelling salts":
						moveName = "smelling salt";
						break;
				}
			}
			else
			{
				switch (moveName.ToLower().Replace("-", " "))
				{
					case "vice grip":
						moveName = "vise grip";
						break;
				}
			}

			string outputName = "MOVE_" + GameDataHelpers.FormatKeyword(moveName);

			if (!GameDataHelpers.MoveDefines.ContainsKey(outputName))
			{
				// Attempt to remove spaces and find a matching name, as that's pretty minor and it's easier to do this
				string testName = outputName.Replace("_", "");

				foreach(var kvp in GameDataHelpers.MoveDefines)
				{
					if (testName == kvp.Key.Replace("_", ""))
						return kvp.Key;
				}
			}

			return outputName;
		}

		private static string FormatItemName(string itemName)
		{
			if (!GameDataHelpers.IsVanillaVersion)
			{
				switch(itemName.ToLower().Replace("-", " "))
				{
					case "stick":
						itemName = "leek";
						break;
				}
			}

			string outputName = "ITEM_" + GameDataHelpers.FormatKeyword(itemName);

			if (!GameDataHelpers.ItemDefines.ContainsKey(outputName))
			{
				// Attempt to remove spaces and find a matching name, as that's pretty minor and it's easier to do this
				string testName = outputName.Replace("_", "");

				foreach (var kvp in GameDataHelpers.ItemDefines)
				{
					if (testName == kvp.Key.Replace("_", ""))
						return kvp.Key;
				}
			}

			return outputName;
		}

		private static bool IsBannedMove(string moveName)
		{
			switch(moveName)
			{
				case "MOVE_TERA_BLAST":
				case "MOVE_PSYSHIELD_BASH":
				case "MOVE_TRAILBLAZE":
				case "MOVE_STONE_AXE":
				case "MOVE_POUNCE":
				case "MOVE_HEADLONG_RUSH":
				case "MOVE_WAVE_CRASH":
				case "MOVE_LAST_RESPECTS":
				case "MOVE_SNOWSCAPE":
				case "MOVE_CHILLING_WATER":
				case "MOVE_DIRE_CLAW":
				case "MOVE_BARB_BARRAGE":
				case "MOVE_SPRINGTIDE_STORM":
				case "MOVE_RAGING_FURY":
				case "MOVE_CHLOROBLAST":
				case "MOVE_INFERNAL_PARADE":
				case "MOVE_CEASELESS_EDGE":
				case "MOVE_VICTORY_DANCE":
				case "MOVE_AXE_KICK":
				case "MOVE_ICE_SPINNER":
				case "MOVE_BITTER_MALICE":
				case "MOVE_COMEUPPANCE":
				case "MOVE_ESPER_WING":
				case "MOVE_SHELTER":
				case "MOVE_MOUNTAIN_GALE":
				case "MOVE_TRIPLE_ARROWS":

					// Sanity check as, when these moves are added they need to be removed from the banned list
					if (GameDataHelpers.MoveDefines.ContainsKey(moveName))
						throw new InvalidDataException();
					return true;
			}

			return false;
		}

		public static void GatherProfiles()
		{
			List<PokemonProfile> profiles = new List<PokemonProfile>();
			Dictionary<string, string> redirectedSpecies = new Dictionary<string, string>();

			foreach (var kvp in GameDataHelpers.SpeciesDefines)
			{
				string speciesName;

				try
				{
					speciesName = kvp.Key;

					if (!GameDataHelpers.IsUniqueSpeciesDefine(speciesName))
						continue;

					if (GameDataHelpers.IsVanillaVersion && speciesName.StartsWith("SPECIES_UNOWN_"))
					{
						// Skip these as they appear after NUM_SPECIES
						continue;
					}

					// Skipped species (Handle elsewhere)
					string redirectSpecies = null;

					if (speciesName.StartsWith("SPECIES_UNOWN_"))
					{
						redirectSpecies = "SPECIES_UNOWN";
					}
					else if (speciesName.StartsWith("SPECIES_CASTFORM_"))
					{
						redirectSpecies = "SPECIES_CASTFORM";
					}

					if (redirectSpecies == null)
					{
						if (!GameDataHelpers.IsVanillaVersion)
						{
							// Only redirect species which are functionally identical for rogue spawning
							if (speciesName.EndsWith("_MEGA"))
							{
								redirectSpecies = speciesName.Substring(0, speciesName.Length - "_MEGA".Length);
							}
							else if (speciesName.EndsWith("_MEGA_X") || speciesName.EndsWith("_MEGA_Y"))
							{
								redirectSpecies = speciesName.Substring(0, speciesName.Length - "_MEGA_X".Length);
							}
							else if (speciesName.EndsWith("_PRIMAL"))
							{
								redirectSpecies = speciesName.Substring(0, speciesName.Length - "_PRIMAL".Length);
							}
							else if (speciesName.StartsWith("SPECIES_BURMY_"))
							{
								redirectSpecies = "SPECIES_BURMY";
							}
							else if (speciesName.StartsWith("SPECIES_ARCEUS_"))
							{
								redirectSpecies = "SPECIES_ARCEUS";
							}
							else if (speciesName.StartsWith("SPECIES_DEERLING_"))
							{
								redirectSpecies = "SPECIES_DEERLING";
							}
							else if (speciesName.StartsWith("SPECIES_SAWSBUCK_"))
							{
								redirectSpecies = "SPECIES_SAWSBUCK";
							}
							else if (speciesName.StartsWith("SPECIES_GENESECT_"))
							{
								redirectSpecies = "SPECIES_GENESECT";
							}
							else if (speciesName.StartsWith("SPECIES_VIVILLON_"))
							{
								redirectSpecies = "SPECIES_VIVILLON";
							}
							else if (speciesName.StartsWith("SPECIES_FLABEBE_"))
							{
								redirectSpecies = "SPECIES_FLABEBE";
							}
							else if (speciesName.StartsWith("SPECIES_FLOETTE_"))
							{
								redirectSpecies = "SPECIES_FLOETTE";
							}
							else if (speciesName.StartsWith("SPECIES_FLORGES_"))
							{
								redirectSpecies = "SPECIES_FLORGES";
							}
							else if (speciesName.StartsWith("SPECIES_FURFROU_"))
							{
								redirectSpecies = "SPECIES_FURFROU";
							}
							else if (speciesName.StartsWith("SPECIES_PUMPKABOO_"))
							{
								redirectSpecies = "SPECIES_PUMPKABOO";
							}
							else if (speciesName.StartsWith("SPECIES_GOURGEIST_"))
							{
								redirectSpecies = "SPECIES_GOURGEIST";
							}
							else if (speciesName.StartsWith("SPECIES_SILVALLY_"))
							{
								redirectSpecies = "SPECIES_SILVALLY";
							}
							else if (speciesName.StartsWith("SPECIES_MINIOR_"))
							{
								redirectSpecies = "SPECIES_MINIOR";
							}
							else if (speciesName.StartsWith("SPECIES_CRAMORANT_"))
							{
								redirectSpecies = "SPECIES_CRAMORANT";
							}
							else if (speciesName.StartsWith("SPECIES_ALCREMIE_"))
							{
								redirectSpecies = "SPECIES_ALCREMIE";
							}
							else
							{
								switch (speciesName)
								{
									case "SPECIES_CHERRIM_SUNSHINE":
										redirectSpecies = "SPECIES_CHERRIM";
										break;

									case "SPECIES_SHELLOS_EAST_SEA":
										redirectSpecies = "SPECIES_SHELLOS";
										break;

									case "SPECIES_GASTRODON_EAST_SEA":
										redirectSpecies = "SPECIES_GASTRODON";
										break;

									case "SPECIES_DARMANITAN_ZEN_MODE":
										redirectSpecies = "SPECIES_DARMANITAN";
										break;

									case "SPECIES_DARMANITAN_ZEN_MODE_GALARIAN":
										redirectSpecies = "SPECIES_DARMANITAN_GALARIAN";
										break;

									case "SPECIES_KELDEO_RESOLUTE":
										redirectSpecies = "SPECIES_KELDEO";
										break;

									case "SPECIES_MELOETTA_PIROUETTE":
										redirectSpecies = "SPECIES_MELOETTA";
										break;

									case "SPECIES_AEGISLASH_BLADE":
										redirectSpecies = "SPECIES_AEGISLASH";
										break;

									case "SPECIES_XERNEAS_ACTIVE":
										redirectSpecies = "SPECIES_XERNEAS";
										break;

									case "SPECIES_ZYGARDE_50_POWER_CONSTRUCT":
										redirectSpecies = "SPECIES_ZYGARDE";
										break;

									case "SPECIES_WISHIWASHI_SCHOOL":
										redirectSpecies = "SPECIES_WISHIWASHI";
										break;

									case "SPECIES_MIMIKYU_BUSTED":
										redirectSpecies = "SPECIES_MIMIKYU";
										break;

									case "SPECIES_MAGEARNA_ORIGINAL_COLOR":
										redirectSpecies = "SPECIES_MAGEARNA";
										break;

									case "SPECIES_SINISTEA_ANTIQUE":
										redirectSpecies = "SPECIES_SINISTEA";
										break;
									case "SPECIES_POLTEAGEIST_ANTIQUE":
										redirectSpecies = "SPECIES_POLTEAGEIST";
										break;

									case "SPECIES_EISCUE_NOICE_FACE":
										redirectSpecies = "SPECIES_EISCUE";
										break;

									case "SPECIES_MORPEKO_HANGRY":
										redirectSpecies = "SPECIES_MORPEKO";
										break;

									case "SPECIES_ETERNATUS_ETERNAMAX":
										redirectSpecies = "SPECIES_ETERNATUS";
										break;

									case "SPECIES_ZARUDE_DADA":
										redirectSpecies = "SPECIES_ZARUDE";
										break;

									case "SPECIES_WOBBUFFET_ROGUEIAN_PUNCHING":
										redirectSpecies = "SPECIES_WOBBUFFET_ROGUEIAN";
										break;
								}

							}
						}
					}

					if (redirectSpecies != null)
					{
						if (!GameDataHelpers.SpeciesDefines.ContainsKey(redirectSpecies))
							throw new InvalidDataException();

						redirectedSpecies[speciesName] = redirectSpecies;
						continue;
					}

					PokemonProfile profile = GatherProfileFor(speciesName);
					profiles.Add(profile);
				}
				catch(AggregateException e)
				{
					if (e.InnerException is HttpRequestException)
						Console.WriteLine($"\tCaught Http Exception '{e.InnerException.Message}'");
					//else
					throw e;
				}
			}

			ExportProfiles(profiles, redirectedSpecies, Path.Combine(GameDataHelpers.RootDirectory, "src\\data\\rogue_pokemon_profiles.h"));

			//ExportLevelUpLearnsets(Path.Combine(outputDir, "level_up_learnsets.h"), profiles);
			//ExportLevelUpLearnsetsPointers(Path.Combine(outputDir, "level_up_learnsets_pointers.h"), profiles);
			//ExportTMHMsLearnsets(Path.Combine(outputDir, "tmhm_learnsets.h"), profiles);
			//ExportTutorLearnsets(Path.Combine(outputDir, "tutor_learnsets.h"), profiles);
			//ExportEggMoves(Path.Combine(outputDir, "egg_moves.h"), profiles);
		}

		private static PokemonProfile GatherProfileFor(string speciesName)
		{
			Console.WriteLine($"Gathering '{speciesName}'");


			PokemonProfile profile = new PokemonProfile(speciesName);

			JObject monEntry = PokeAPI.GetPokemonSpeciesEntry(speciesName);

			foreach (var obj in monEntry["abilities"])
			{
				string abilityName = obj["ability"]["name"].ToString();
				string rawSlot = obj["slot"].ToString();

				profile.Abilities[int.Parse(rawSlot) - 1] = abilityName;
			}

			foreach (var obj in monEntry["types"])
			{
				string name = obj["type"]["name"].ToString();
				string rawSlot = obj["slot"].ToString();

				profile.Types[int.Parse(rawSlot) - 1] = name;
			}

			foreach (var moveObj in monEntry["moves"])
			{
				foreach (var versionObj in moveObj["version_group_details"])
				{
					MoveInfo moveInfo = new MoveInfo();
					moveInfo.moveName = moveObj["move"]["name"].ToString();
					moveInfo.versionName = versionObj["version_group"]["name"].ToString();

					string method = versionObj["move_learn_method"]["name"].ToString();
					switch (method)
					{
						case "egg":
							moveInfo.originMethod = MoveInfo.LearnMethod.Egg;
							break;
						case "machine":
							moveInfo.originMethod = MoveInfo.LearnMethod.TM;
							break;
						case "tutor":
							moveInfo.originMethod = MoveInfo.LearnMethod.Tutor;
							break;
						case "level-up":
							moveInfo.originMethod = MoveInfo.LearnMethod.LevelUp;
							moveInfo.learnLevel = int.Parse(versionObj["level_learned_at"].ToString());
							break;

						// Special cases
						//case "stadium-surfing-pikachu":
						//case "light-ball-egg":
						default:
							moveInfo.originMethod = MoveInfo.LearnMethod.Tutor;
							break;

							//default:
							//	throw new NotImplementedException();
					}

					profile.Moves.Add(moveInfo);
				}
			}

			JObject competitiveSets = PokeAPI.GetPokemonSpeciesCompetitiveSets(speciesName);

			foreach(var tierKvp in competitiveSets)
			{
				foreach(var currentSet in tierKvp.Value.Value<JArray>())
				{
					string tierName = GameDataHelpers.FormatKeyword(tierKvp.Key);
					PokemonCompetitiveSet compSet = PokemonCompetitiveSet.ParseFrom(tierName, currentSet.Value<JObject>());

					bool hasMerged = false;

					foreach(var existingSet in profile.CompetitiveSets)
					{
						// No need to contain duplicate sets
						if(existingSet.IsCompatibleWith(compSet))
						{
							existingSet.SourceTiers.Add(tierName);
							hasMerged = true;
							break;
						}
					}

					if (!hasMerged)
						profile.CompetitiveSets.Add(compSet);
				}
			}

			profile.CollapseForExport();
			return profile;
		}

		private static void ExportProfiles(List<PokemonProfile> profiles, Dictionary<string, string> redirectedSpecies, string filePath)
		{
			Console.WriteLine($"Exporting profiles to '{filePath}'");

			StringBuilder upperBlock = new StringBuilder();
			StringBuilder lowerBlock = new StringBuilder();

			upperBlock.AppendLine("// == WARNING ==");
			upperBlock.AppendLine("// DO NOT EDIT THIS FILE");
			upperBlock.AppendLine("// This file was automatically generated by PokemonDataGenerator");
			upperBlock.AppendLine("//");
			upperBlock.AppendLine();

			lowerBlock.AppendLine("struct RoguePokemonProfile const gRoguePokemonProfiles[NUM_SPECIES] = \n{");

			foreach(var profile in profiles)
			{
				// Level moves
				upperBlock.AppendLine($"static struct LevelUpMove const sLevelUpMoves_{profile.Species}[] = \n{{");
				foreach(var move in profile.Moves.Where(m => m.originMethod == MoveInfo.LearnMethod.LevelUp))
				{
					upperBlock.AppendLine($"\t{{ .move={move.moveName}, .level={move.learnLevel} }},");
				}
				upperBlock.AppendLine($"\t{{ .move=MOVE_NONE, .level=0 }},");
				upperBlock.AppendLine($"}};");
				upperBlock.AppendLine();

				// Tutor moves
				upperBlock.AppendLine($"static u16 const sTutorMoves_{profile.Species}[] = \n{{");
				foreach (var move in profile.Moves.Where(m => m.originMethod != MoveInfo.LearnMethod.LevelUp))
				{
					upperBlock.AppendLine($"\t{move.moveName},");
				}
				upperBlock.AppendLine($"\tMOVE_NONE,");
				upperBlock.AppendLine($"}};");
				upperBlock.AppendLine();

				// Comp sets
				upperBlock.AppendLine($"static struct RoguePokemonCompetitiveSet const sCompetitiveSets_{profile.Species}[] = \n{{");
				foreach(var compSet in profile.CompetitiveSets)
				{
					upperBlock.AppendLine($"\t{{");

					if(compSet.Item != null)
						upperBlock.AppendLine($"\t\t.heldItem={compSet.Item},");

					if (compSet.Ability != null)
						upperBlock.AppendLine($"\t\t.ability={compSet.Ability},");

					if (compSet.HiddenPower != null)
						upperBlock.AppendLine($"\t\t.hiddenPowerType={compSet.HiddenPower},");

					if (compSet.Nature != null)
						upperBlock.AppendLine($"\t\t.nature={compSet.Nature},");

					upperBlock.AppendLine($"\t\t.moves=\n\t\t{{");
					foreach(var move in compSet.Moves)
					{
						upperBlock.AppendLine($"\t\t\t{move},");
					}
					upperBlock.AppendLine($"\t\t}},");

					upperBlock.AppendLine($"\t}},");
				}
				upperBlock.AppendLine($"}};");
				upperBlock.AppendLine();


				// Add to species lookup below
				lowerBlock.AppendLine($"\t[{profile.Species}] = \n\t{{");
				lowerBlock.AppendLine($"\t\t.levelUpMoves = sLevelUpMoves_{profile.Species},");
				lowerBlock.AppendLine($"\t\t.tutorMoves = sTutorMoves_{profile.Species},");
				lowerBlock.AppendLine($"\t\t.competitiveSets = sCompetitiveSets_{profile.Species},");
				lowerBlock.AppendLine($"\t\t.competitiveSetCount = ARRAY_COUNT(sCompetitiveSets_{profile.Species}),");
				lowerBlock.AppendLine($"\t}},");
			}

			// Attach redirected species info too
			foreach(var kvp in redirectedSpecies)
			{
				lowerBlock.AppendLine($"\t[{kvp.Key}] = \n\t{{");
				lowerBlock.AppendLine($"\t\t.levelUpMoves = sLevelUpMoves_{kvp.Value},");
				lowerBlock.AppendLine($"\t\t.tutorMoves = sTutorMoves_{kvp.Value},");
				lowerBlock.AppendLine($"\t\t.competitiveSets = sCompetitiveSets_{kvp.Value},");
				lowerBlock.AppendLine($"\t\t.competitiveSetCount = ARRAY_COUNT(sCompetitiveSets_{kvp.Value}),");
				lowerBlock.AppendLine($"\t}},");
			}

			lowerBlock.AppendLine("};");

			File.WriteAllText(filePath, upperBlock.ToString() + "\n"+ lowerBlock.ToString());
		}

		private static void ExportLevelUpLearnsets(string filePath, IEnumerable<PokemonProfile> profiles)
		{
			StringBuilder content = new StringBuilder();

			content.AppendLine("// == WARNING ==");
			content.AppendLine("// DO NOT EDIT THIS FILE");
			content.AppendLine("// This file was automatically generated by PokemonDataGenerator");
			content.AppendLine("//");

			foreach(var profile in profiles)
			{
				var moves = profile.GetLevelUpMoves();

				if(moves.Any())
				{
					content.AppendLine($"");
					content.AppendLine($"static const struct LevelUpMove s{profile.PrettySpeciesName}LevelUpLearnset[] = {{");

					foreach (var move in moves)
					{
						content.AppendLine($"\tLEVEL_UP_MOVE({move.learnLevel}, MOVE_{FormatKeyword(move.moveName)}),");
					}
					content.AppendLine($"\tLEVEL_UP_END");

					content.AppendLine($"}};");
				}
			}

			string fullStr = content.ToString().Replace("\t", "    ");
			File.WriteAllText(filePath, fullStr);
		}

		private static void ExportLevelUpLearnsetsPointers(string filePath, IEnumerable<PokemonProfile> profiles)
		{
			StringBuilder content = new StringBuilder();

			content.AppendLine("// == WARNING ==");
			content.AppendLine("// DO NOT EDIT THIS FILE");
			content.AppendLine("// This file was automatically generated by PokemonDataGenerator");
			content.AppendLine("//");
			content.AppendLine($"");

			content.AppendLine($"const struct LevelUpMove *const gLevelUpLearnsets[NUM_SPECIES] =\n{{");

			foreach (var profile in profiles)
			{
				var moves = profile.GetLevelUpMoves();

				if (moves.Any())
				{
					content.AppendLine($"\t[{profile.Species}] = s{profile.PrettySpeciesName}LevelUpLearnset,");
				}
			}

			content.AppendLine($"}};");

			string fullStr = content.ToString().Replace("\t", "    ");
			File.WriteAllText(filePath, fullStr);
		}

		private static void ExportTMHMsLearnsets(string filePath, IEnumerable<PokemonProfile> profiles)
		{
			StringBuilder content = new StringBuilder();

			content.AppendLine("// == WARNING ==");
			content.AppendLine("// DO NOT EDIT THIS FILE");
			content.AppendLine("// This file was automatically generated by PokemonDataGenerator");
			content.AppendLine("//");
			content.AppendLine($"");

			content.AppendLine($"#define TMHM_ToBitIndex(item) (item > ITEM_LAST_VALID_TM ? (ITEM_LAST_VALID_TM - ITEM_TM01 + item - ITEM_HM01) : (item - ITEM_TM01))");
			content.AppendLine($"");
			content.AppendLine($"#define TMHM_EMPTY_LEARNSET() {{ ITEM_TMHM_COUNT }}");
			content.AppendLine($"#define TMHM_LEARNSET(moves) {{ moves ITEM_TMHM_COUNT }}");
			content.AppendLine($"#define TMHM(tmhm) (u16)(ITEM_##tmhm - ITEM_TM01),");
			content.AppendLine($"");

			content.AppendLine($"const u8 gTMHMLearnsets[][64] =\n{{");

			foreach (var profile in profiles)
			{
				var moves = profile.GetTMHMMoves();

				if (moves.Any())
				{
					content.AppendLine($"\t[{profile.Species}] = TMHM_LEARNSET(");

					foreach (var move in moves)
					{
						string item = GameDataHelpers.MoveToTMHMItem[FormatKeyword(move.moveName)];
						content.AppendLine($"\t\tTMHM({item.Substring("ITEM_".Length)})");
					}

					content.AppendLine($"\t),");
				}
				else
				{
					content.AppendLine($"\t[{profile.Species}] = TMHM_EMPTY_LEARNSET(),");
				}
			}

			content.AppendLine($"}};");

			string fullStr = content.ToString().Replace("\t", "    ");
			File.WriteAllText(filePath, fullStr);
		}

		private static void ExportTutorLearnsets(string filePath, IEnumerable<PokemonProfile> profiles)
		{
			StringBuilder content = new StringBuilder();

			content.AppendLine("// == WARNING ==");
			content.AppendLine("// DO NOT EDIT THIS FILE");
			content.AppendLine("// This file was automatically generated by PokemonDataGenerator");
			content.AppendLine("//");
			content.AppendLine($"");

			content.AppendLine(@"const u16 gTutorMoves[] =
{
    [TUTOR_MOVE_MEGA_PUNCH] = MOVE_MEGA_PUNCH,
    [TUTOR_MOVE_SWORDS_DANCE] = MOVE_SWORDS_DANCE,
    [TUTOR_MOVE_MEGA_KICK] = MOVE_MEGA_KICK,
    [TUTOR_MOVE_BODY_SLAM] = MOVE_BODY_SLAM,
    [TUTOR_MOVE_DOUBLE_EDGE] = MOVE_DOUBLE_EDGE,
    [TUTOR_MOVE_COUNTER] = MOVE_COUNTER,
    [TUTOR_MOVE_SEISMIC_TOSS] = MOVE_SEISMIC_TOSS,
    [TUTOR_MOVE_MIMIC] = MOVE_MIMIC,
    [TUTOR_MOVE_METRONOME] = MOVE_METRONOME,
    [TUTOR_MOVE_SOFT_BOILED] = MOVE_SOFT_BOILED,
    [TUTOR_MOVE_DREAM_EATER] = MOVE_DREAM_EATER,
    [TUTOR_MOVE_THUNDER_WAVE] = MOVE_THUNDER_WAVE,
    [TUTOR_MOVE_EXPLOSION] = MOVE_EXPLOSION,
    [TUTOR_MOVE_ROCK_SLIDE] = MOVE_ROCK_SLIDE,
    [TUTOR_MOVE_SUBSTITUTE] = MOVE_SUBSTITUTE,
    [TUTOR_MOVE_DYNAMIC_PUNCH] = MOVE_DYNAMIC_PUNCH,
    [TUTOR_MOVE_ROLLOUT] = MOVE_ROLLOUT,
    [TUTOR_MOVE_PSYCH_UP] = MOVE_PSYCH_UP,
    [TUTOR_MOVE_SNORE] = MOVE_SNORE,
    [TUTOR_MOVE_ICY_WIND] = MOVE_ICY_WIND,
    [TUTOR_MOVE_ENDURE] = MOVE_ENDURE,
    [TUTOR_MOVE_MUD_SLAP] = MOVE_MUD_SLAP,
    [TUTOR_MOVE_ICE_PUNCH] = MOVE_ICE_PUNCH,
    [TUTOR_MOVE_SWAGGER] = MOVE_SWAGGER,
    [TUTOR_MOVE_SLEEP_TALK] = MOVE_SLEEP_TALK,
    [TUTOR_MOVE_SWIFT] = MOVE_SWIFT,
    [TUTOR_MOVE_DEFENSE_CURL] = MOVE_DEFENSE_CURL,
    [TUTOR_MOVE_THUNDER_PUNCH] = MOVE_THUNDER_PUNCH,
    [TUTOR_MOVE_FIRE_PUNCH] = MOVE_FIRE_PUNCH,
    [TUTOR_MOVE_FURY_CUTTER] = MOVE_FURY_CUTTER,
};

#define TUTOR_LEARNSET(moves) ((u32)(moves))
#define TUTOR(move) ((u64)1 << (TUTOR_##move))
");

			content.AppendLine($"static const u32 sTutorLearnsets[] =\n{{");

			foreach (var profile in profiles)
			{
				var moves = profile.GetTutorMoves();

				if (moves.Any())
				{
					content.AppendLine($"\t[{profile.Species}] = TUTOR_LEARNSET(");

					foreach (var move in moves)
					{
						content.AppendLine($"\t\tTUTOR(MOVE_{FormatKeyword(move.moveName)})");
					}

					content.AppendLine($"\t),");
				}
				else
				{
					content.AppendLine($"\t[{profile.Species}] = (0),");
				}
			}

			content.AppendLine($"}};");

			string fullStr = content.ToString().Replace("\t", "    ");
			File.WriteAllText(filePath, fullStr);
		}

		private static void ExportEggMoves(string filePath, IEnumerable<PokemonProfile> profiles)
		{
			StringBuilder content = new StringBuilder();

			content.AppendLine("// == WARNING ==");
			content.AppendLine("// DO NOT EDIT THIS FILE");
			content.AppendLine("// This file was automatically generated by PokemonDataGenerator");
			content.AppendLine("//");
			content.AppendLine($"");

			content.AppendLine(@"#define EGG_MOVES_SPECIES_OFFSET 20000
#define EGG_MOVES_TERMINATOR 0xFFFF
#define egg_moves(species, moves...) (SPECIES_##species + EGG_MOVES_SPECIES_OFFSET), moves
");

			content.AppendLine($"const u16 gEggMoves[] = {{\n");

			foreach (var profile in profiles)
			{
				var moves = profile.GetEggMoves();

				if (moves.Any())
				{
					content.AppendLine($"\tegg_moves({profile.Species.Substring("SPECIES_".Length)}");
					bool isFirst = true;

					foreach (var move in moves)
					{
						content.AppendLine($"\t\t,MOVE_{FormatKeyword(move.moveName)}");
					}

					content.AppendLine($"\t),");
				}
			}

			content.AppendLine($"\tEGG_MOVES_TERMINATOR\n}};");

			string fullStr = content.ToString().Replace("\t", "    ");
			File.WriteAllText(filePath, fullStr);
		}

		private static string FormatKeyword(string keyword)
		{
			return keyword.Trim()
				.Replace(".", "")
				.Replace("’", "")
				.Replace("'", "")
				.Replace("%", "")
				.Replace(":", "")
				.Replace(" ", "_")
				.Replace("-", "_")
				.Replace("é", "e")
				.ToUpper();
		}
	}
}
