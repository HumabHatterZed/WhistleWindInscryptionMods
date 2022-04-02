using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_FlagBearer()
        {
            const string rulebookName = "Flag Bearer";
            const string rulebookDescription = "Adjacent cards gain 2 Health.";
            const string dialogue = "Morale runs high.";

            FlagBearer.ability = WstlUtils.CreateAbility<FlagBearer>(
                Resources.sigilFlagBearer,
                rulebookName, rulebookDescription, dialogue, 3).Id;
        }
    }
    public class FlagBearer : ExtendedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool ProvidesPassiveHealthBuff => true;

        public override int[] GetPassiveHealthBuffs()
        {
            List<int> slots = new() { 0, 0, 0, 0 };
            foreach (CardSlot slot in (base.Card.OpponentCard ? Singleton<BoardManager>.Instance.opponentSlots : Singleton<BoardManager>.Instance.playerSlots))
            {
                if (slot == base.Card.Slot)
                {
                    CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(slot, true);
                    CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(slot, false);
                    if (leftSlot != null && leftSlot.Card != null)
                    {
                        slots[leftSlot.Index] += 2;
                    }
                    if (rightSlot != null && rightSlot.Card != null)
                    {
                        slots[rightSlot.Index] += 2;
                    }
                }
            }
            return slots.ToArray();
        }
        public override bool RespondsToResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                return true;
            }
            return false;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }
        
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card == otherCard)
                {
                    return true;
                }
            }
            return false;

        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }
    }
}
