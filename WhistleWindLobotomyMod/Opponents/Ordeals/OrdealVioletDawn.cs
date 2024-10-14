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
    /// 
    /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
    /// 1 | F4     | F3     | F3     | 3  | 12 | 0
    /// 3 | F3     | F3     | F2     | 3  | 12 | 0
    /// 5 | F3     | F2 F2  | F2     | 4  | 16 | 0
    /// </summary>
    public class OrdealVioletDawn : OrdealBattleSequencer
    {
        public override void ModifyQueuedCard(PlayableCard card)
        {
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

        public void ConstructVioletDawn(EncounterData encounterData, int maxDifficultyNoModifier)
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

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            ConstructVioletDawn(encounterData, 3);
            return encounterData;
        }
    }
}