#include "BakeHelpers.h"

#include <string>
#include <fstream>

extern "C"
{
	#include "rogue_baked.h"
}

u16 eggLookup[NUM_SPECIES]{ SPECIES_NONE };
u8 evolutionCountLookup[NUM_SPECIES]{ 0 };

int main()
{
	for (int s = SPECIES_NONE; s < NUM_SPECIES; ++s)
	{
		if (s == SPECIES_NONE)
		{
			eggLookup[s] = s;
			evolutionCountLookup[s] = 0;
		}
		else
		{
			eggLookup[s] = Rogue_GetEggSpecies(s);
			evolutionCountLookup[s] = Rogue_GetEvolutionCount(s);
		}
	}

	std::string const c_OutputPath = "..\\..\\..\\..\\src\\data\\rogue_bake_data.h";

	std::ofstream file;
	file.open(c_OutputPath, std::ios::out);

	file << "// == WARNING ==\n";
	file << "// DO NOT EDIT THIS FILE\n";
	file << "// This file was automatically generated by PokemonQueryBaker\n";
	file << "\n";

	file << "const u16 gRogueBake_EggSpecies[NUM_SPECIES] =\n{\n";
	for (int s = SPECIES_NONE; s < NUM_SPECIES; ++s)
	{
		file << "\t" << (int)eggLookup[s] << ",\n";
	}
	file << "};\n";
	file << "\n";

	file << "const u8 gRogueBake_EvolutionCount[NUM_SPECIES] =\n{\n";
	for (int s = SPECIES_NONE; s < NUM_SPECIES; ++s)
	{
		file << "\t" << (int)evolutionCountLookup[s] << ",\n";
	}
	file << "};\n";

	file << "\n";
	file.close();
	return 0;
}
