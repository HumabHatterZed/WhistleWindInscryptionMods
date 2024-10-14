/*using DiskCardGame;
using GrimoraMod.Extensions;
using InscryptionAPI.Encounters;
using InscryptionAPI.Nodes;
using InscryptionAPI.TalkingCards;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using WhistleWindLobotomyMod.Opponents.Prospector;

namespace WhistleWindLobotomyMod
{
    /// <summary>
    /// Appears in R1
    /// Difficulty range: (5 - 9) +[0,2]
    /// 
    /// Begin with short version of Dawn encounter then do Noon proper
    /// </summary>
    public class OrdealNoon : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealNoon", typeof(OrdealNoon)).Id;

        private CardSlot[] loveSlots = null;
        public override IEnumerator OpponentUpkeep()
        {
            if (loveSlots == null || Opponent.NumTurnsTaken < Opponent.TurnPlan.Count + 1)
                yield break;

            CleanupTargetIcons();
            if (loveSlots[0].Card != null)
                yield return loveSlots[0].Card.Die(false);

            if (loveSlots[1].Card != null)
                yield return BoardManager.Instance.OpponentSlotsCopy[loveSlots[1].Index + 1].Card.Die(false);

            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_grantUsLove"), loveSlots[0]);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            yield return new WaitForSeconds(1f);
            loveSlots = null;
        }
        public override bool PlayerHasDefeatedOrdeal() => loveSlots != null ? false : base.PlayerHasDefeatedOrdeal();

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            // if the next turn is the final turn in the turn plan, set up Grant Us Love
            if (ordealType == OrdealType.Violet && loveSlots == null && Opponent.NumTurnsTaken == Opponent.TurnPlan.Count)
            {
                int slotIndex = UnityEngine.Random.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count - 1);
                loveSlots = new CardSlot[] { BoardManager.Instance.OpponentSlotsCopy[slotIndex], BoardManager.Instance.OpponentSlotsCopy[slotIndex + 1] };
                CreateTargetIcon(loveSlots[0], GameColors.Instance.darkPurple);
                CreateTargetIcon(loveSlots[1], GameColors.Instance.darkPurple);
                yield break;
            }

            yield return base.OnTurnEnd(playerTurnEnd);
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // noon of violet only ends when Grant Us Love dies
            if (ordealType == OrdealType.Violet && card.Info.name != "wstl_grantUsLove")
            {
                yield break;
            }
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        public override void ModifyQueuedCard(PlayableCard card)
        {
            if (ordealType != OrdealType.Violet)
                return;

            if (Opponent.Difficulty == 8)
            {
                if (Opponent.NumTurnsTaken == 0)
                    card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
            }
            else if (Opponent.Difficulty < 8)
            {
                if (Opponent.NumTurnsTaken < 1)
                    card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability, StartingDecay.ability } });
                else
                    card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
            }
            else
            {
                if (Opponent.NumTurnsTaken < 2)
                    card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
            }
        }

        private void ConstructGreenNoon(EncounterData encounterData)
        {

        }

        /// <summary>
        /// D | Turn 1 | Turn 3 | Turn 4 | ## | HP | Atk
        /// 5 | H H    | -      | -      | 2  | 12 | 0
        /// 7 | H H    | -      | H H    | 4  | 12 | 0
        /// 9 | H H    | H H    | -      | 4  | 16 | 0
        /// </summary>
        private void ConstructCrimsonNoon(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wst_skinHarmony"),
                EncounterManager.NewCardBlueprint("wst_skinHarmony")
            };
            encounterData.Blueprint.AddTurn(turn1);
            if (encounterData.Difficulty >= 7)
            {
                List<EncounterBlueprintData.CardBlueprint> turn2 = new()
                {
                    EncounterManager.NewCardBlueprint("wst_skinHarmony"),
                    EncounterManager.NewCardBlueprint("wst_skinHarmony")
                };
                int turnNum = 4 - (encounterData.Difficulty - 5) / 2;
                for (int i = 0; i < turnNum; i++)
                    encounterData.Blueprint.AddTurn();

                encounterData.Blueprint.AddTurn(turn2);
            }
        }
        private void ConstructIndigoNoon(EncounterData encounterData)
        {
            int numTurns = (encounterData.Difficulty + 1) / 2;
            for (int i = 0; i < numTurns; i++)
            {
                if (i % 2 == 0) encounterData.Blueprint.AddTurn();

                List<EncounterBlueprintData.CardBlueprint> turn = new()
                {
                    EncounterManager.NewCardBlueprint("wstl_sweeper"),
                    EncounterManager.NewCardBlueprint("wstl_sweeper")
                };
                encounterData.Blueprint.AddTurn(turn);
            }
        }
        private void ConstructWhiteNoon(EncounterData encounterData)
        {
            List<string> possibleFixers = new()
            {
                "wstl_fixerRed", "wstl_fixerWhite", "wstl_fixerWhite"
            };
            possibleFixers.Remove(OrdealDawn.chosenWhiteDawnFixer);
            possibleFixers.Randomize();

            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint(possibleFixers[0])
            };
            List<EncounterBlueprintData.CardBlueprint> turn4 = new()
            {
                EncounterManager.NewCardBlueprint(possibleFixers[1])
            };
            encounterData.Blueprint.AddTurns(new(), turn2, new(), turn4);
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            switch (ordealType)
            {
                case OrdealType.Green:
                    ConstructGreenNoon(encounterData);
                    break;
                case OrdealType.Violet:
                    OrdealDawn.ConstructVioletDawn(encounterData, 8);
                    break;
                case OrdealType.Crimson:
                    ConstructCrimsonNoon(encounterData);
                    break;
                case OrdealType.Indigo:
                    ConstructIndigoNoon(encounterData);
                    break;
                default:
                    ConstructWhiteNoon(encounterData);
                    break;
            }
            return encounterData;
        }
    }
}*/