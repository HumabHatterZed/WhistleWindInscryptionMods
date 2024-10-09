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
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// </summary>
    public class OrdealDawn : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealDawn", typeof(OrdealDawn)).Id;
        public static string chosenWhiteDawnFixer;

        public override void ModifyQueuedCard(PlayableCard card)
        {
            if (ordealType != OrdealType.Violet)
                return;

            switch (Opponent.Difficulty)
            {
                case 1:
                case 2:
                    if (Opponent.NumTurnsTaken == 0)
                        card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability, StartingDecay.ability } });
                    else
                        card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
                    break;
                case 3:
                    if (Opponent.NumTurnsTaken < 2)
                        card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
                    break;
                default:
                    if (Opponent.NumTurnsTaken == 1)
                        card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
                    break;
            }
        }

        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
        /// 1 | A A    | B A    | Y B    | 6  | 9  | 7
        /// 2 | B A    | Y B    | O B    | 6  | 12 | 8
        /// 3 | B B    | Y B    | O Y A  | 7  | 14 | 10
        /// 4 | Y B    | O Y    | O Y B  | 7  | 16 | 12
        /// 5 | Y B    | O Y B  | O Y Y B| 7  | 19 | 15
        /// </summary>
        private void ConstructGreenDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1, turn2, turn3;
            if (encounterData.Difficulty <= 2)
            {
                turn1 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 1),
                    EncounterManager.NewCardBlueprint("wstl_doubtA")
                };
                turn2 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 1),
                    HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 1)
                };
                turn3 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtY", "wstl_doubtO", 1),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
            }
            else if (encounterData.Difficulty <= 4)
            {
                turn1 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 3),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
                turn2 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtY", "wstl_doubtO", 3),
                    HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 3)
                };
                turn3 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtO"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 3)
                };
            }
            else
            {
                turn1 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
                turn2 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtO"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
                turn3 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtO"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
        }

        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
        /// 1 | F4     | F3     | F3     | 3  | 12 | 0
        /// 3 | F3     | F3     | F2     | 3  | 12 | 0
        /// 5 | F3     | F2 F2  | F2     | 4  | 16 | 0
        /// </summary>
        public static void ConstructVioletDawn(EncounterData encounterData, int maxDifficultyNoModifier)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new() {
                EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new() {
                EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding")
            };
            List<EncounterBlueprintData.CardBlueprint> turn3 = new() { 
                EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding")
            };
            
            if (encounterData.Difficulty >= maxDifficultyNoModifier + 2)
                turn2.Add(EncounterManager.NewCardBlueprint("wstl_fruitUnderstanding"));

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
            if (encounterData.Difficulty < maxDifficultyNoModifier)
                encounterData.Blueprint.AddTurn();
        }
        /// <summary>
        /// D | Turn 1 | ## | HP | Atk
        /// 1 | C C    | 2  | 6  | 0
        /// 3 | C C C  | 3  | 9  | 0
        /// 5 | C C C C| 4  | 12 | 0
        /// </summary>
        private void ConstructCrimsonDawn(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinCheers"),
                EncounterManager.NewCardBlueprint("wstl_skinCheers")
            };

            if (encounterData.Difficulty >= 3)
            {
                turn1.Add(EncounterManager.NewCardBlueprint("wstl_skinCheers"));
                if (encounterData.Difficulty >= 5)
                    turn1.Add(EncounterManager.NewCardBlueprint("wstl_skinCheers"));
            }

            encounterData.Blueprint.AddTurns(turn1);
        }
        /// <summary>
        /// D | Turn 1 | Turn 2 | Turn 3 | Turn 4 | ## | HP | Atk
        /// 1 | P      | P P    | P      | P P    | 6  | 6  | 6
        /// 2 | P P    | P P    | P P    | P P    | 8  | 8  | 8
        /// 3 | P P    | P P P  | P P    | P P P  | 10 | 10 | 10
        /// 4 | P P P  | P P P  | P P P  | P P P  | 12 | 12 | 12
        /// 5 | P P P  | P P P P| P P P  | P P P P| 14 | 14 | 14
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

            for (int i = 2; i < encounterData.Difficulty + 1; i++)
            {
                if (i % 2 == 0)
                {
                    turn1.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                    turn3.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                }
                else
                {
                    turn2.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                    turn4.Add(EncounterManager.NewCardBlueprint("wstl_perfectFood"));
                }

                if (turn1.Count >= 4 && turn2.Count >= 4) break;
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
            chosenWhiteDawnFixer = UnityEngine.Random.RandomRangeInt(0, 3) switch
            {
                0 => "wstl_fixerWhite",
                1 => "wstl_fixerBlack",
                _ => "wstl_fixerRed"
            };

            encounterData.Blueprint.AddTurns(new List<EncounterBlueprintData.CardBlueprint>() {
                EncounterManager.NewCardBlueprint(chosenWhiteDawnFixer)
            });
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            switch (ordealType)
            {
                case OrdealType.Green:
                    ConstructGreenDawn(encounterData);
                    break;
                case OrdealType.Violet:
                    ConstructVioletDawn(encounterData, 3);
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
    }
}