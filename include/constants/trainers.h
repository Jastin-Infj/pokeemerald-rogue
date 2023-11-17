#ifndef GUARD_TRAINERS_H
#define GUARD_TRAINERS_H

#include "constants/opponents.h"
#include "constants/battle_frontier_trainers.h"

// Special Trainer Ids.
//      0-299 are frontier trainers
#define TRAINER_RECORD_MIXING_FRIEND        FRONTIER_TRAINERS_COUNT
#define TRAINER_RECORD_MIXING_APPRENTICE    400
#define TRAINER_EREADER                     500
#define TRAINER_FRONTIER_BRAIN              1022
#define TRAINER_PLAYER                      1023
#define TRAINER_SECRET_BASE                 1024
#define TRAINER_LINK_OPPONENT               2048
#define TRAINER_UNION_ROOM                  3072
#define TRAINER_STEVEN_PARTNER              3075

#define TRAINER_PIC_HIKER                  0
#define TRAINER_PIC_AQUA_GRUNT_M           1
#define TRAINER_PIC_POKEMON_BREEDER_F      2
#define TRAINER_PIC_COOLTRAINER_M          3
#define TRAINER_PIC_BIRD_KEEPER            4
#define TRAINER_PIC_COLLECTOR              5
#define TRAINER_PIC_AQUA_GRUNT_F           6
#define TRAINER_PIC_SWIMMER_M              7
#define TRAINER_PIC_MAGMA_GRUNT_M          8
#define TRAINER_PIC_EXPERT_M               9
#define TRAINER_PIC_AQUA_ADMIN_M          10
#define TRAINER_PIC_BLACK_BELT            11
#define TRAINER_PIC_AQUA_ADMIN_F          12
#define TRAINER_PIC_AQUA_LEADER_ARCHIE    13
#define TRAINER_PIC_HEX_MANIAC            14
#define TRAINER_PIC_AROMA_LADY            15
#define TRAINER_PIC_RUIN_MANIAC           16
#define TRAINER_PIC_INTERVIEWER           17
#define TRAINER_PIC_TUBER_F               18
#define TRAINER_PIC_TUBER_M               19
#define TRAINER_PIC_COOLTRAINER_F         20
#define TRAINER_PIC_LADY                  21
#define TRAINER_PIC_BEAUTY                22
#define TRAINER_PIC_RICH_BOY              23
#define TRAINER_PIC_EXPERT_F              24
#define TRAINER_PIC_POKEMANIAC            25
#define TRAINER_PIC_MAGMA_GRUNT_F         26
#define TRAINER_PIC_GUITARIST             27
#define TRAINER_PIC_KINDLER               28
#define TRAINER_PIC_CAMPER                29
#define TRAINER_PIC_PICNICKER             30
#define TRAINER_PIC_BUG_MANIAC            31
#define TRAINER_PIC_POKEMON_BREEDER_M     32
#define TRAINER_PIC_PSYCHIC_M             33
#define TRAINER_PIC_PSYCHIC_F             34
#define TRAINER_PIC_GENTLEMAN             35
#define TRAINER_PIC_ELITE_FOUR_SIDNEY     36
#define TRAINER_PIC_ELITE_FOUR_PHOEBE     37
#define TRAINER_PIC_ELITE_FOUR_GLACIA     38
#define TRAINER_PIC_ELITE_FOUR_DRAKE      39
#define TRAINER_PIC_LEADER_ROXANNE        40
#define TRAINER_PIC_LEADER_BRAWLY         41
#define TRAINER_PIC_LEADER_WATTSON        42
#define TRAINER_PIC_LEADER_FLANNERY       43
#define TRAINER_PIC_LEADER_NORMAN         44
#define TRAINER_PIC_LEADER_WINONA         45
#define TRAINER_PIC_LEADER_TATE_AND_LIZA  46
#define TRAINER_PIC_LEADER_JUAN           47
#define TRAINER_PIC_SCHOOL_KID_M          48
#define TRAINER_PIC_SCHOOL_KID_F          49
#define TRAINER_PIC_SR_AND_JR             50
#define TRAINER_PIC_POKEFAN_M             51
#define TRAINER_PIC_POKEFAN_F             52
#define TRAINER_PIC_YOUNGSTER             53
#define TRAINER_PIC_CHAMPION_WALLACE      54
#define TRAINER_PIC_FISHERMAN             55
#define TRAINER_PIC_CYCLING_TRIATHLETE_M  56
#define TRAINER_PIC_CYCLING_TRIATHLETE_F  57
#define TRAINER_PIC_RUNNING_TRIATHLETE_M  58
#define TRAINER_PIC_RUNNING_TRIATHLETE_F  59
#define TRAINER_PIC_SWIMMING_TRIATHLETE_M 60
#define TRAINER_PIC_SWIMMING_TRIATHLETE_F 61
#define TRAINER_PIC_DRAGON_TAMER          62
#define TRAINER_PIC_NINJA_BOY             63
#define TRAINER_PIC_BATTLE_GIRL           64
#define TRAINER_PIC_PARASOL_LADY          65
#define TRAINER_PIC_SWIMMER_F             66
#define TRAINER_PIC_TWINS                 67
#define TRAINER_PIC_SAILOR                68
#define TRAINER_PIC_MAGMA_ADMIN           69
#define TRAINER_PIC_WALLY                 70
#define TRAINER_PIC_BRENDAN               71
#define TRAINER_PIC_MAY                   72
#define TRAINER_PIC_BUG_CATCHER           73
#define TRAINER_PIC_POKEMON_RANGER_M      74
#define TRAINER_PIC_POKEMON_RANGER_F      75
#define TRAINER_PIC_MAGMA_LEADER_MAXIE    76
#define TRAINER_PIC_LASS                  77
#define TRAINER_PIC_YOUNG_COUPLE          78
#define TRAINER_PIC_OLD_COUPLE            79
#define TRAINER_PIC_SIS_AND_BRO           80
#define TRAINER_PIC_STEVEN                81
#define TRAINER_PIC_SALON_MAIDEN_ANABEL   82
#define TRAINER_PIC_DOME_ACE_TUCKER       83
#define TRAINER_PIC_PALACE_MAVEN_SPENSER  84
#define TRAINER_PIC_ARENA_TYCOON_GRETA    85
#define TRAINER_PIC_FACTORY_HEAD_NOLAND   86
#define TRAINER_PIC_PIKE_QUEEN_LUCY       87
#define TRAINER_PIC_PYRAMID_KING_BRANDON  88
#define TRAINER_PIC_RED                   89
#define TRAINER_PIC_LEAF                  90
#define TRAINER_PIC_RS_BRENDAN            91
#define TRAINER_PIC_RS_MAY                92

// RogueNote: Extra trainer pics
#define TRAINER_PIC_KANTO_BROCK           93
#define TRAINER_PIC_KANTO_MISTY           94
#define TRAINER_PIC_KANTO_LTSURGE         95
#define TRAINER_PIC_KANTO_ERIKA           96
#define TRAINER_PIC_KANTO_KOGA            97
#define TRAINER_PIC_KANTO_SABRINA         98
#define TRAINER_PIC_KANTO_BLAINE          99
#define TRAINER_PIC_KANTO_GIOVANNI        100

#define TRAINER_PIC_KANTO_LORELEI         101
#define TRAINER_PIC_KANTO_BRUNO           102
#define TRAINER_PIC_KANTO_AGATHA          103
#define TRAINER_PIC_KANTO_LANCE           104

#define TRAINER_PIC_KANTO_BLUE            105
#define TRAINER_PIC_KANTO_PROFOAK         106

#define TRAINER_PIC_JOHTO_FALKNER         107
#define TRAINER_PIC_JOHTO_BUGSY           108
#define TRAINER_PIC_JOHTO_WHITNEY         109
#define TRAINER_PIC_JOHTO_MORTY           110
#define TRAINER_PIC_JOHTO_CHUCK           111
#define TRAINER_PIC_JOHTO_JASMINE         112
#define TRAINER_PIC_JOHTO_PRYCE           113
#define TRAINER_PIC_JOHTO_CLAIR           114

#define TRAINER_PIC_JOHTO_WILL            115
#define TRAINER_PIC_JOHTO_KAREN           116

#define TRAINER_PIC_JOHTO_RED             117


#define TRAINER_PIC_SINNOH_AARON          118
#define TRAINER_PIC_SINNOH_BERTHA         119
#define TRAINER_PIC_SINNOH_BYRON          120
#define TRAINER_PIC_SINNOH_CANDICE        121
#define TRAINER_PIC_SINNOH_CRASHER_WAKE   122
#define TRAINER_PIC_SINNOH_CYNTHIA        123
#define TRAINER_PIC_SINNOH_FANTINA        124
#define TRAINER_PIC_SINNOH_FLINT          125
#define TRAINER_PIC_SINNOH_GARDENIA       126
#define TRAINER_PIC_SINNOH_LUCIAN         127
#define TRAINER_PIC_SINNOH_MAYLENE        128
#define TRAINER_PIC_SINNOH_ROARK          129
#define TRAINER_PIC_SINNOH_VOLKNER        130

#define TRAINER_PIC_UNOVA_ALDER           131
#define TRAINER_PIC_UNOVA_BRYCEN          132
#define TRAINER_PIC_UNOVA_BURGH           133
#define TRAINER_PIC_UNOVA_CAITLIN         134
#define TRAINER_PIC_UNOVA_CHILI           135
#define TRAINER_PIC_UNOVA_CILAN           136
#define TRAINER_PIC_UNOVA_CLAY            137
#define TRAINER_PIC_UNOVA_CRESS           138
#define TRAINER_PIC_UNOVA_DRAYDEN         139
#define TRAINER_PIC_UNOVA_ELESA           140
#define TRAINER_PIC_UNOVA_GRIMSLEY        141
#define TRAINER_PIC_UNOVA_IRIS            142
#define TRAINER_PIC_UNOVA_LENORA          143
#define TRAINER_PIC_UNOVA_MARLON          144
#define TRAINER_PIC_UNOVA_MARSHAL         145
#define TRAINER_PIC_UNOVA_ROXIE           146
#define TRAINER_PIC_UNOVA_SHAUNTAL        147
#define TRAINER_PIC_UNOVA_SKYLA           148

#define TRAINER_PIC_KALOS_CLEMONT         149
#define TRAINER_PIC_KALOS_DIANTHA         150
#define TRAINER_PIC_KALOS_DRASNA          151
#define TRAINER_PIC_KALOS_GRANT           152
#define TRAINER_PIC_KALOS_KORRINA         153
#define TRAINER_PIC_KALOS_MALVA           154
#define TRAINER_PIC_KALOS_OLYMPIA         155
#define TRAINER_PIC_KALOS_RAMOS           156
#define TRAINER_PIC_KALOS_SIEBOLD         157
#define TRAINER_PIC_KALOS_VALERIE         158
#define TRAINER_PIC_KALOS_VIOLA           159
#define TRAINER_PIC_KALOS_WIKSTROM        160
#define TRAINER_PIC_KALOS_WULFRIC         161

#define TRAINER_PIC_ALOLA_ACEROLA         162
#define TRAINER_PIC_ALOLA_HALA            163
#define TRAINER_PIC_ALOLA_HAPU            164
#define TRAINER_PIC_ALOLA_ILIMA           165
#define TRAINER_PIC_ALOLA_KAHILI          166
#define TRAINER_PIC_ALOLA_KIAWE           167
#define TRAINER_PIC_ALOLA_KUKUI           168
#define TRAINER_PIC_ALOLA_LANA            169
#define TRAINER_PIC_ALOLA_MALLOW          170
#define TRAINER_PIC_ALOLA_MINA            171
#define TRAINER_PIC_ALOLA_MOLAYNE         172
#define TRAINER_PIC_ALOLA_NANU            173
#define TRAINER_PIC_ALOLA_OLIVIA          174
#define TRAINER_PIC_ALOLA_SOPHOCLES       175

// gap for now as there are some more gens on the way

#define TRAINER_PIC_ETHAN                 200
#define TRAINER_PIC_LYRA                  201

#define TRAINER_PIC_LUCAS                 202
#define TRAINER_PIC_DAWN                  203
#define TRAINER_PIC_HILBERT               204
#define TRAINER_PIC_HILDA                 205
#define TRAINER_PIC_NATE                  206
#define TRAINER_PIC_ROSA                  207
#define TRAINER_PIC_CALEM                 208
#define TRAINER_PIC_SERENA                209

#define TRAINER_PIC_POKABBIE              210
#define TRAINER_PIC_POKABBIE_JUST_DITTO   211 // Just used for battle transition
#define TRAINER_PIC_POKABBIE_AND_DITTO    212
#define TRAINER_PIC_GLITCH_KATE           213
#define TRAINER_PIC_GLITCH_TAILS          214
#define TRAINER_PIC_GLITCH_ERMA           215
#define TRAINER_PIC_GLITCH_RAVEN          TRAINER_PIC_POKEMON_BREEDER_F

#define TRAINER_PIC_RIVAL_BRENDAN         216
#define TRAINER_PIC_RIVAL_MAY             217
#define TRAINER_PIC_RIVAL_SILVER          218
#define TRAINER_PIC_RIVAL_BARRY           219
#define TRAINER_PIC_RIVAL_BIANCA          220
#define TRAINER_PIC_RIVAL_CHEREN          221
#define TRAINER_PIC_RIVAL_N               222
#define TRAINER_PIC_RIVAL_HUGH            223
#define TRAINER_PIC_RIVAL_SHAUNA          224
#define TRAINER_PIC_RIVAL_TREVOR          225
#define TRAINER_PIC_RIVAL_TIERNO          226
#define TRAINER_PIC_RIVAL_HAU             227
#define TRAINER_PIC_RIVAL_GLADION         228
#define TRAINER_PIC_RIVAL_HOP             229
#define TRAINER_PIC_RIVAL_MARNIE          230
#define TRAINER_PIC_RIVAL_BEDE            231


// Special cases
#define TRAINER_PIC_PLAYER_AVATAR           254
#define TRAINER_PIC_PLAYER_OPPOSITE_AVATAR  255

// The player back pics are assumed to alternate according to the gender values (MALE/FEMALE)
#define TRAINER_BACK_PIC_NONE                   0
#define TRAINER_BACK_PIC_BRENDAN                1
#define TRAINER_BACK_PIC_MAY                    2
#define TRAINER_BACK_PIC_RED                    3
#define TRAINER_BACK_PIC_LEAF                   4
#define TRAINER_BACK_PIC_ETHAN                  5
#define TRAINER_BACK_PIC_LYRA                   6
#define TRAINER_BACK_PIC_RUBY_SAPPHIRE_BRENDAN  7
#define TRAINER_BACK_PIC_RUBY_SAPPHIRE_MAY      8
#define TRAINER_BACK_PIC_WALLY                  9
#define TRAINER_BACK_PIC_STEVEN                 10
#define TRAINER_BACK_PIC_LUCAS                  11
#define TRAINER_BACK_PIC_DAWN                   12

#define TRAINER_BACK_PIC_PLAYER                 0xFF

#define FACILITY_CLASS_HIKER                 0x0
#define FACILITY_CLASS_AQUA_GRUNT_M          0x1
#define FACILITY_CLASS_PKMN_BREEDER_F        0x2
#define FACILITY_CLASS_COOLTRAINER_M         0x3
#define FACILITY_CLASS_BIRD_KEEPER           0x4
#define FACILITY_CLASS_COLLECTOR             0x5
#define FACILITY_CLASS_AQUA_GRUNT_F          0x6
#define FACILITY_CLASS_SWIMMER_M             0x7
#define FACILITY_CLASS_MAGMA_GRUNT_M         0x8
#define FACILITY_CLASS_EXPERT_M              0x9
#define FACILITY_CLASS_BLACK_BELT            0xa
#define FACILITY_CLASS_AQUA_LEADER_ARCHIE    0xb
#define FACILITY_CLASS_HEX_MANIAC            0xc
#define FACILITY_CLASS_AROMA_LADY            0xd
#define FACILITY_CLASS_RUIN_MANIAC           0xe
#define FACILITY_CLASS_INTERVIEWER           0xf
#define FACILITY_CLASS_TUBER_F               0x10
#define FACILITY_CLASS_TUBER_M               0x11
#define FACILITY_CLASS_COOLTRAINER_F         0x12
#define FACILITY_CLASS_LADY                  0x13
#define FACILITY_CLASS_BEAUTY                0x14
#define FACILITY_CLASS_RICH_BOY              0x15
#define FACILITY_CLASS_EXPERT_F              0x16
#define FACILITY_CLASS_POKEMANIAC            0x17
#define FACILITY_CLASS_MAGMA_GRUNT_F         0x18
#define FACILITY_CLASS_GUITARIST             0x19
#define FACILITY_CLASS_KINDLER               0x1a
#define FACILITY_CLASS_CAMPER                0x1b
#define FACILITY_CLASS_PICNICKER             0x1c
#define FACILITY_CLASS_BUG_MANIAC            0x1d
#define FACILITY_CLASS_PSYCHIC_M             0x1e
#define FACILITY_CLASS_PSYCHIC_F             0x1f
#define FACILITY_CLASS_GENTLEMAN             0x20
#define FACILITY_CLASS_ELITE_FOUR_SIDNEY     0x21
#define FACILITY_CLASS_ELITE_FOUR_PHOEBE     0x22
#define FACILITY_CLASS_LEADER_ROXANNE        0x23
#define FACILITY_CLASS_LEADER_BRAWLY         0x24
#define FACILITY_CLASS_LEADER_TATE_AND_LIZA  0x25
#define FACILITY_CLASS_SCHOOL_KID_M          0x26
#define FACILITY_CLASS_SCHOOL_KID_F          0x27
#define FACILITY_CLASS_SR_AND_JR             0x28
#define FACILITY_CLASS_POKEFAN_M             0x29
#define FACILITY_CLASS_POKEFAN_F             0x2a
#define FACILITY_CLASS_YOUNGSTER             0x2b
#define FACILITY_CLASS_CHAMPION_WALLACE      0x2c
#define FACILITY_CLASS_FISHERMAN             0x2d
#define FACILITY_CLASS_CYCLING_TRIATHLETE_M  0x2e
#define FACILITY_CLASS_CYCLING_TRIATHLETE_F  0x2f
#define FACILITY_CLASS_RUNNING_TRIATHLETE_M  0x30
#define FACILITY_CLASS_RUNNING_TRIATHLETE_F  0x31
#define FACILITY_CLASS_SWIMMING_TRIATHLETE_M 0x32
#define FACILITY_CLASS_SWIMMING_TRIATHLETE_F 0x33
#define FACILITY_CLASS_DRAGON_TAMER          0x34
#define FACILITY_CLASS_NINJA_BOY             0x35
#define FACILITY_CLASS_BATTLE_GIRL           0x36
#define FACILITY_CLASS_PARASOL_LADY          0x37
#define FACILITY_CLASS_SWIMMER_F             0x38
#define FACILITY_CLASS_TWINS                 0x39
#define FACILITY_CLASS_SAILOR                0x3a
#define FACILITY_CLASS_WALLY                 0x3b
#define FACILITY_CLASS_BRENDAN               0x3c
#define FACILITY_CLASS_BRENDAN_2             0x3d
#define FACILITY_CLASS_BRENDAN_3             0x3e
#define FACILITY_CLASS_MAY                   0x3f
#define FACILITY_CLASS_MAY_2                 0x40
#define FACILITY_CLASS_MAY_3                 0x41
#define FACILITY_CLASS_PKMN_BREEDER_M        0x42
#define FACILITY_CLASS_BUG_CATCHER           0x43
#define FACILITY_CLASS_PKMN_RANGER_M         0x44
#define FACILITY_CLASS_PKMN_RANGER_F         0x45
#define FACILITY_CLASS_MAGMA_LEADER_MAXIE    0x46
#define FACILITY_CLASS_LASS                  0x47
#define FACILITY_CLASS_YOUNG_COUPLE          0x48
#define FACILITY_CLASS_OLD_COUPLE            0x49
#define FACILITY_CLASS_SIS_AND_BRO           0x4a
#define FACILITY_CLASS_STEVEN                0x4b
#define FACILITY_CLASS_SALON_MAIDEN_ANABEL   0x4c
#define FACILITY_CLASS_DOME_ACE_TUCKER       0x4d
#define FACILITY_CLASS_RED                   0x4e
#define FACILITY_CLASS_LEAF                  0x4f
#define FACILITY_CLASS_RS_BRENDAN            0x50
#define FACILITY_CLASS_RS_MAY                0x51
#define FACILITY_CLASS_ETHAN                 0x52
#define FACILITY_CLASS_LYRA                  0x53

#define FACILITY_CLASSES_COUNT               0x54

#define RS_FACILITY_CLASS_AQUA_LEADER_ARCHIE    0x0
#define RS_FACILITY_CLASS_AQUA_GRUNT_M          0x1
#define RS_FACILITY_CLASS_AQUA_GRUNT_F          0x2
#define RS_FACILITY_CLASS_AROMA_LADY            0x3
#define RS_FACILITY_CLASS_RUIN_MANIAC           0x4
#define RS_FACILITY_CLASS_INTERVIEWER           0x5
#define RS_FACILITY_CLASS_TUBER_F               0x6
#define RS_FACILITY_CLASS_TUBER_M               0x7
#define RS_FACILITY_CLASS_COOLTRAINER_M         0x8
#define RS_FACILITY_CLASS_COOLTRAINER_F         0x9
#define RS_FACILITY_CLASS_HEX_MANIAC            0xA
#define RS_FACILITY_CLASS_LADY                  0xB
#define RS_FACILITY_CLASS_BEAUTY                0xC
#define RS_FACILITY_CLASS_RICH_BOY              0xD
#define RS_FACILITY_CLASS_POKEMANIAC            0xE
#define RS_FACILITY_CLASS_SWIMMER_M             0xF
#define RS_FACILITY_CLASS_BLACK_BELT            0x10
#define RS_FACILITY_CLASS_GUITARIST             0x11
#define RS_FACILITY_CLASS_KINDLER               0x12
#define RS_FACILITY_CLASS_CAMPER                0x13
#define RS_FACILITY_CLASS_BUG_MANIAC            0x14
#define RS_FACILITY_CLASS_PSYCHIC_M             0x15
#define RS_FACILITY_CLASS_PSYCHIC_F             0x16
#define RS_FACILITY_CLASS_GENTLEMAN             0x17
#define RS_FACILITY_CLASS_ELITE_FOUR_M          0x18
#define RS_FACILITY_CLASS_ELITE_FOUR_F          0x19
#define RS_FACILITY_CLASS_LEADER_F              0x1A
#define RS_FACILITY_CLASS_LEADER_M              0x1B
#define RS_FACILITY_CLASS_LEADER_MF             0x1C
#define RS_FACILITY_CLASS_SCHOOL_KID_M          0x1D
#define RS_FACILITY_CLASS_SCHOOL_KID_F          0x1E
#define RS_FACILITY_CLASS_SR_AND_JR             0x1F
#define RS_FACILITY_CLASS_POKEFAN_M             0x20
#define RS_FACILITY_CLASS_POKEFAN_F             0x21
#define RS_FACILITY_CLASS_EXPERT_M              0x22
#define RS_FACILITY_CLASS_EXPERT_F              0x23
#define RS_FACILITY_CLASS_YOUNGSTER             0x24
#define RS_FACILITY_CLASS_CHAMPION              0x25
#define RS_FACILITY_CLASS_FISHERMAN             0x26
#define RS_FACILITY_CLASS_CYCLING_TRIATHLETE_M  0x27
#define RS_FACILITY_CLASS_CYCLING_TRIATHLETE_F  0x28
#define RS_FACILITY_CLASS_RUNNING_TRIATHLETE_M  0x29
#define RS_FACILITY_CLASS_RUNNING_TRIATHLETE_F  0x2A
#define RS_FACILITY_CLASS_SWIMMING_TRIATHLETE_M 0x2B
#define RS_FACILITY_CLASS_SWIMMING_TRIATHLETE_F 0x2C
#define RS_FACILITY_CLASS_DRAGON_TAMER          0x2D
#define RS_FACILITY_CLASS_BIRD_KEEPER           0x2E
#define RS_FACILITY_CLASS_NINJA_BOY             0x2F
#define RS_FACILITY_CLASS_BATTLE_GIRL           0x30
#define RS_FACILITY_CLASS_PARASOL_LADY          0x31
#define RS_FACILITY_CLASS_SWIMMER_F             0x32
#define RS_FACILITY_CLASS_PICNICKER             0x33
#define RS_FACILITY_CLASS_TWINS                 0x34
#define RS_FACILITY_CLASS_SAILOR                0x35
#define RS_FACILITY_CLASS_BOARDER_1             0x36
#define RS_FACILITY_CLASS_BOARDER_2             0x37
#define RS_FACILITY_CLASS_COLLECTOR             0x38
#define RS_FACILITY_CLASS_WALLY                 0x39
#define RS_FACILITY_CLASS_BRENDAN_1             0x3A
#define RS_FACILITY_CLASS_BRENDAN_2             0x3B
#define RS_FACILITY_CLASS_BRENDAN_3             0x3C
#define RS_FACILITY_CLASS_MAY_1                 0x3D
#define RS_FACILITY_CLASS_MAY_2                 0x3E
#define RS_FACILITY_CLASS_MAY_3                 0x3F
#define RS_FACILITY_CLASS_PKMN_BREEDER_M        0x40
#define RS_FACILITY_CLASS_PKMN_BREEDER_F        0x41
#define RS_FACILITY_CLASS_PKMN_RANGER_M         0x42
#define RS_FACILITY_CLASS_PKMN_RANGER_F         0x43
#define RS_FACILITY_CLASS_MAGMA_LEADER          0x44
#define RS_FACILITY_CLASS_MAGMA_GRUNT_M         0x45
#define RS_FACILITY_CLASS_MAGMA_GRUNT_F         0x46
#define RS_FACILITY_CLASS_LASS                  0x47
#define RS_FACILITY_CLASS_BUG_CATCHER           0x48
#define RS_FACILITY_CLASS_HIKER                 0x49
#define RS_FACILITY_CLASS_YOUNG_COUPLE          0x4A
#define RS_FACILITY_CLASS_OLD_COUPLE            0x4B
#define RS_FACILITY_CLASS_SIS_AND_BRO           0x4C

#define RS_FACILITY_CLASSES_COUNT               0x4D

#define TRAINER_CLASS_PKMN_TRAINER_1 0x0  // Unused
#define TRAINER_CLASS_PKMN_TRAINER_2 0x1  // Unused
#define TRAINER_CLASS_HIKER          0x2
#define TRAINER_CLASS_TEAM_AQUA      0x3
#define TRAINER_CLASS_PKMN_BREEDER   0x4
#define TRAINER_CLASS_COOLTRAINER    0x5
#define TRAINER_CLASS_BIRD_KEEPER    0x6
#define TRAINER_CLASS_COLLECTOR      0x7
#define TRAINER_CLASS_SWIMMER_M      0x8
#define TRAINER_CLASS_TEAM_MAGMA     0x9
#define TRAINER_CLASS_EXPERT         0xa
#define TRAINER_CLASS_AQUA_ADMIN     0xb
#define TRAINER_CLASS_BLACK_BELT     0xc
#define TRAINER_CLASS_AQUA_LEADER    0xd
#define TRAINER_CLASS_HEX_MANIAC     0xe
#define TRAINER_CLASS_AROMA_LADY     0xf
#define TRAINER_CLASS_RUIN_MANIAC    0x10
#define TRAINER_CLASS_INTERVIEWER    0x11
#define TRAINER_CLASS_TUBER_F        0x12
#define TRAINER_CLASS_TUBER_M        0x13
#define TRAINER_CLASS_LADY           0x14
#define TRAINER_CLASS_BEAUTY         0x15
#define TRAINER_CLASS_RICH_BOY       0x16
#define TRAINER_CLASS_POKEMANIAC     0x17
#define TRAINER_CLASS_GUITARIST      0x18
#define TRAINER_CLASS_KINDLER        0x19
#define TRAINER_CLASS_CAMPER         0x1a
#define TRAINER_CLASS_PICNICKER      0x1b
#define TRAINER_CLASS_BUG_MANIAC     0x1c
#define TRAINER_CLASS_PSYCHIC        0x1d
#define TRAINER_CLASS_GENTLEMAN      0x1e
#define TRAINER_CLASS_ELITE_FOUR     0x1f
#define TRAINER_CLASS_LEADER         0x20
#define TRAINER_CLASS_SCHOOL_KID     0x21
#define TRAINER_CLASS_SR_AND_JR      0x22
#define TRAINER_CLASS_WINSTRATE      0x23
#define TRAINER_CLASS_POKEFAN        0x24
#define TRAINER_CLASS_YOUNGSTER      0x25
#define TRAINER_CLASS_CHAMPION       0x26
#define TRAINER_CLASS_FISHERMAN      0x27
#define TRAINER_CLASS_TRIATHLETE     0x28
#define TRAINER_CLASS_DRAGON_TAMER   0x29
#define TRAINER_CLASS_NINJA_BOY      0x2a
#define TRAINER_CLASS_BATTLE_GIRL    0x2b
#define TRAINER_CLASS_PARASOL_LADY   0x2c
#define TRAINER_CLASS_SWIMMER_F      0x2d
#define TRAINER_CLASS_TWINS          0x2e
#define TRAINER_CLASS_SAILOR         0x2f
#define TRAINER_CLASS_COOLTRAINER_2  0x30 // Used for only one trainer.
#define TRAINER_CLASS_MAGMA_ADMIN    0x31
#define TRAINER_CLASS_RIVAL          0x32
#define TRAINER_CLASS_BUG_CATCHER    0x33
#define TRAINER_CLASS_PKMN_RANGER    0x34
#define TRAINER_CLASS_MAGMA_LEADER   0x35
#define TRAINER_CLASS_LASS           0x36
#define TRAINER_CLASS_YOUNG_COUPLE   0x37
#define TRAINER_CLASS_OLD_COUPLE     0x38
#define TRAINER_CLASS_SIS_AND_BRO    0x39
#define TRAINER_CLASS_SALON_MAIDEN   0x3a
#define TRAINER_CLASS_DOME_ACE       0x3b
#define TRAINER_CLASS_PALACE_MAVEN   0x3c
#define TRAINER_CLASS_ARENA_TYCOON   0x3d
#define TRAINER_CLASS_FACTORY_HEAD   0x3e
#define TRAINER_CLASS_PIKE_QUEEN     0x3f
#define TRAINER_CLASS_PYRAMID_KING   0x40
#define TRAINER_CLASS_RS_PROTAG      0x41
#define TRAINER_CLASS_TOTEM_LEADER   0x42

#define TRAINER_ENCOUNTER_MUSIC_MALE         0 // standard male encounter music
#define TRAINER_ENCOUNTER_MUSIC_FEMALE       1 // standard female encounter music
#define TRAINER_ENCOUNTER_MUSIC_GIRL         2 // used for male Tubers and Young Couples too
#define TRAINER_ENCOUNTER_MUSIC_SUSPICIOUS   3
#define TRAINER_ENCOUNTER_MUSIC_INTENSE      4
#define TRAINER_ENCOUNTER_MUSIC_COOL         5
#define TRAINER_ENCOUNTER_MUSIC_AQUA         6
#define TRAINER_ENCOUNTER_MUSIC_MAGMA        7
#define TRAINER_ENCOUNTER_MUSIC_SWIMMER      8
#define TRAINER_ENCOUNTER_MUSIC_TWINS        9 // used for other trainer classes too
#define TRAINER_ENCOUNTER_MUSIC_ELITE_FOUR  10
#define TRAINER_ENCOUNTER_MUSIC_HIKER       11 // used for other trainer classes too
#define TRAINER_ENCOUNTER_MUSIC_INTERVIEWER 12
#define TRAINER_ENCOUNTER_MUSIC_RICH        13 // Rich Boys and Gentlemen

#define F_TRAINER_FEMALE (1 << 7)

// All trainer parties specify the IV, level, and species for each Pokémon in the
// party. Some trainer parties also specify held items and custom moves for each
// Pokémon.
#define F_TRAINER_PARTY_CUSTOM_MOVESET (1 << 0)
#define F_TRAINER_PARTY_HELD_ITEM      (1 << 1)

#endif  // GUARD_TRAINERS_H
