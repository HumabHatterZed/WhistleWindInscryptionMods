using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Abusive()
        {
            const string rulebookName = "Abusive";
            const string rulebookDescription = "At the end of the owner's turn, [creature] will strike adjacent creatures that failed to strike another card during combat.";
            const string dialogue = "Worthless mutt.";
            const string triggerText = "[creature] punishes lazy beasts.";
            Abusive.ability = AbnormalAbilityHelper.CreateAbility<Abusive>(
                "sigilAbusive",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: -2,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Abusive : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly List<CardSlot> CardsAttackedThisTurn = new();

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            return slot.IsPlayerSlot != base.Card.OpponentCard && slot != base.Card.Slot;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardsAttackedThisTurn.Add(slot);
            yield break;
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<PlayableCard> adjacent = base.Card.Slot.GetAdjacentCards();
            if (adjacent.Exists(x => !CardsAttackedThisTurn.Contains(x.Slot)))
            {
                base.Card.Anim.StrongNegationEffect();
                yield return base.LearnAbility(0.5f);
                yield return new WaitForSeconds(0.5f);

                foreach (PlayableCard card in adjacent)
                {
                    if (!CardsAttackedThisTurn.Contains(card.Slot))
                    {
                        yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(base.Card.Slot, card.Slot, adjacent.Count > 1 ? 0.1f : 0f);
                    }
                }
            }

            CardsAttackedThisTurn.Clear();
        }
    }
}
