using DiskCardGame;
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
    public class OrdealVioletNoon : OrdealVioletDawn
    {
        private CardSlot[] loveSlots = null;

        public override IEnumerator OpponentUpkeep()
        {
            if (loveSlots == null || Opponent.NumTurnsTaken < Opponent.TurnPlan.Count + 1)
                yield break;

            CleanupTargetIcons();

            loveSlots[0].Card?.Anim.PlayDeathAnimation();
            loveSlots[1].Card?.Anim.PlayDeathAnimation();

            if (loveSlots[0].Card != null) yield return loveSlots[0].Card.Die(false);

            if (loveSlots[1].Card != null) yield return loveSlots[1].Card.Die(false);

            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_grantUsLove"), loveSlots[0]);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            yield return new WaitForSeconds(1f);
            loveSlots = null;
        }
        public override bool PlayerHasDefeatedOrdeal() => loveSlots == null && base.PlayerHasDefeatedOrdeal();

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            // if the next turn is the final turn in the turn plan, set up Grant Us Love
            if (loveSlots == null && Opponent.NumTurnsTaken == Opponent.TurnPlan.Count)
            {
                int slotIndex = UnityEngine.Random.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count - 1);
                loveSlots = new CardSlot[] { BoardManager.Instance.OpponentSlotsCopy[slotIndex], BoardManager.Instance.OpponentSlotsCopy[slotIndex + 1] };
                CreateTargetIcon(loveSlots[0], GameColors.Instance.darkPurple);
                CreateTargetIcon(loveSlots[1], GameColors.Instance.darkPurple);
            }
            else
            {
                yield return base.OnTurnEnd(playerTurnEnd);
            }
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // noon of violet only ends when Grant Us Love dies
            if (card.Info.name != "wstl_grantUsLove")
                yield break;

            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        public override void ModifyQueuedCard(PlayableCard card)
        {
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
        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            base.ConstructVioletDawn(encounterData, 9);
            return encounterData;
        }
    }
}