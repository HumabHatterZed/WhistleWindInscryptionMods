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
        private void Ability_Aggravating()
        {
            const string rulebookName = "Aggravating";
            const string rulebookDescription = "While this card is on the board, all opposing cards gain 1 Power.";
            const string dialogue = "The presence of your creature drives my beasts to bloodlust.";

            Aggravating.ability = WstlUtils.CreateAbility<Aggravating>(
                Resources.sigilAggravating,
                rulebookName, rulebookDescription, dialogue, -3).Id;
        }
    }
    public class Aggravating : WstlAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool ProvidesPassiveAttackBuffOpponent => true;
        public override int[] GetPassiveAttackBuffsOpponent()
        {
            List<int> slots = new() { 0, 0, 0, 0 };
            foreach (CardSlot slot in (base.Card.OpponentCard ? Singleton<BoardManager>.Instance.playerSlots : Singleton<BoardManager>.Instance.opponentSlots))
            {
                if (slot.Card != null)
                {
                    slots[slot.Index] -= 1;
                }
            }
            return slots.ToArray();
        }

        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }

        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }

        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }
        public bool ActivateOnPlay()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(false))
            {
                if (slot.Card != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
