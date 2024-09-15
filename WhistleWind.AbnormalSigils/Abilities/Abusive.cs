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
            const string rulebookDescription = "At the end of the owner's turn, [creature] will strike adjacent creatures that failed to deal any damage during combat.";
            const string dialogue = "This beast will not tolerate 'laziness'.";
            const string triggerText = "[creature] 'motivates' lazy beasts.";
            Abusive.ability = AbnormalAbilityHelper.CreateAbility<Abusive>(
                "sigilAbusive",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: -3,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Abusive : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly List<PlayableCard> CardsAttackedThisTurn = new();

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
            => attacker.OpponentCard == slot.IsPlayerSlot && base.Card.Slot.GetAdjacentCards().Contains(attacker);

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardsAttackedThisTurn.Add(attacker);
            return base.OnCardGettingAttacked(attacker);
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<PlayableCard> adjacent = base.Card.Slot.GetAdjacentCards();
            if (adjacent.Exists(x => !CardsAttackedThisTurn.Contains(x)))
            {
                yield return base.PreSuccessfulTriggerSequence();
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);

                foreach (PlayableCard card in adjacent)
                {
                    if (!CardsAttackedThisTurn.Contains(card))
                    {
                        yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(base.Card.Slot, card.Slot, adjacent.Count > 1 ? 0.1f : 0f);
                    }
                }
                yield return base.LearnAbility(0.5f);
            }

            CardsAttackedThisTurn.Clear();
        }
    }
}
