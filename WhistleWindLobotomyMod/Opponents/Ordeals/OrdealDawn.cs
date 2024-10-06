using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.P03KayceeRun.Encounters;
using InscryptionAPI.Encounters;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    /// <summary>
    /// Appears in R0, R1
    /// Difficulty range: (1 - 4, 5 - 10) +[0,2]
    /// </summary>
    public class OrdealDawn : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealDawn", typeof(OrdealDawn)).Id;

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            switch (ordealType)
            {
                case OrdealType.Green:
                    ConstructGreenDawn(encounterData);
                    break;
                case OrdealType.Violet:
                    ConstructVioletDawn(encounterData);
                    break;
                case OrdealType.Crimson:
                    ConstructCrimsonDawn(encounterData);
                    break;
                case OrdealType.Amber:
                    ConstructAmberDawn(encounterData);
                    break;
                default:
                    ConstructWhiteDawn(encounterData);
                    break;
            }

            return encounterData;
        }
        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | Turn 4 | ## | HP | Atk
        /// 0 | A      | A      | A A    | -      | 4  | 4  | 4
        /// 3 | A      | A A    | B A    | -      | 5  | 6  | 5
        /// 5 | A A    | B A    | B B    | Y      | 7  | 11 | 8
        /// 7 | A A    | B A    | B B A  | O      | 8  | 13 | 9
        /// 9 | A A    | B B A  | B Y B  | O Y    | 10 | 18 | 13
        /// </summary>
        private void ConstructGreenDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_doubtA")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 5)
            };
            List<EncounterBlueprintData.CardBlueprint> turn3 = new()
            {
                HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 3)
            };
            List<EncounterBlueprintData.CardBlueprint> turn4 = new();

            if (encounterData.Difficulty >= 3)
            {
                turn2.Add(HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 9));
            }
            if (encounterData.Difficulty >= 5)
            {
                turn1.Add(EncounterManager.NewCardBlueprint("wstl_doubtA"));
                turn3.Add(HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 9));
                turn4.Add(HelperMethods.NewDifficultyCard("wstl_doubtY", "wstl_doubtO", 7));
                if (encounterData.Difficulty >= 7)
                {
                    turn3.Add(HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 9));
                    if (encounterData.Difficulty >= 9)
                    {
                        turn2.Add(EncounterManager.NewCardBlueprint("wstl_doubtA"));
                        turn4.Add(EncounterManager.NewCardBlueprint("wstl_doubtY"));
                    }
                }
            }
            else
            {
                turn3.Add(EncounterManager.NewCardBlueprint("wstl_doubtA"));
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3, turn4);
        }
        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
        /// 0 | F      |        | F F    | 3  | 12 | 0
        /// 5 | F      | F      | F F    | 4  | 16 | 0
        /// 9 | F F    | F F    | F F    | 6  | 24 | 0
        /// </summary>
        private void ConstructVioletDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new();
            List<EncounterBlueprintData.CardBlueprint> turn3 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding")
            };

            if (encounterData.Difficulty >= 5)
            {
                turn3.Add(EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding"));
                if (encounterData.Difficulty >= 9)
                {
                    turn1.Add(EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding"));
                    turn2.Add(EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding"));
                    turn3.Add(EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding"));
                }
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
        }
        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
        /// 0 | C C    |        | C      | 3  | 9  | 3
        /// 5 | C C    |        | C C    | 4  | 12 | 4
        /// 9 | C C C  |        | C C C  | 6  | 24 | 6
        /// </summary>
        private void ConstructCrimsonDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinCheers"),
                EncounterManager.NewCardBlueprint("wstl_skinCheers")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new();
            List<EncounterBlueprintData.CardBlueprint> turn3 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinCheers")
            };

            if (encounterData.Difficulty >= 5)
            {
                turn3.Add(EncounterManager.NewCardBlueprint("wstl_skinCheers"));
                if (encounterData.Difficulty >= 9)
                {
                    turn1.Add(EncounterManager.NewCardBlueprint("wstl_skinCheers"));
                    turn3.Add(EncounterManager.NewCardBlueprint("wstl_skinCheers"));
                }
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
        }
        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | Turn 4 | ## | HP | Atk
        /// 0 | P      | P P    | P      | P P    | 6  | 6  | 6
        /// 5 | P P    | P P    | P      | P P P  | 8  | 8  | 8
        /// 7 | P P    | P P P  | P P    | P P P P| 11 | 11 | 11
        /// </summary>
        private void ConstructAmberDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_perfectFood")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_perfectFood"),
                EncounterManager.NewCardBlueprint("wstl_perfectFood")
            };
            List<EncounterBlueprintData.CardBlueprint> turn3 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_perfectFood")
            };
            List<EncounterBlueprintData.CardBlueprint> turn4 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_perfectFood"),
                EncounterManager.NewCardBlueprint("wstl_perfectFood")
            };
            if (encounterData.Difficulty >= 5)
            {
                turn1.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                turn4.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                if (encounterData.Difficulty >= 7)
                {
                    turn2.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                    turn3.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                    turn4.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                }
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3, turn4);
        }
        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
        /// 0 | C C    |        | C      | 3  | 9  | 3
        /// 5 | C C    |        | C C    | 4  | 12 | 4
        /// 9 | C C C  |        | C C C  | 6  | 24 | 6
        /// </summary>
        private void ConstructWhiteDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint(UnityEngine.Random.RandomRangeInt(0, 3) switch
                {
                    0 => "wstl_fixerWhite",
                    1 => "wstl_fixerBlack",
                    _ => "wstl_fixerRed"
                })
            };
            encounterData.Blueprint.AddTurns(turn1);
        }
    }
}