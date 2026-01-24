using Core.Helpers;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Lonely() {
            const string rulebookName = "Lonely";
            const string rulebookDescription = "Give Pebble to a chosen creature, then return this card to your hand. When a card with Pebble perishes, kill all ally cards with Pebble and inflict Grief on the remaining allies.";
            const string dialogue = "A friend to stay.";
            Lonely.ability = AbnormalAbilityHelper.CreateAbility<Lonely>(
                "sigilLonely",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2)
                .SetAbilityRedirect("Pebble", Pebble.iconId, GameColors.Instance.gray)
                .SetAbilityRedirect("Grief", Grief.iconId, GameColors.Instance.red)
                .Id;
        }
    }
    /// <summary>
    /// Choose one of your cards to gain Pebble unless a card with Pebble already exists, then return this card to your hand.
    /// </summary>
    [HarmonyPatch]
    public class Lonely : AbilityBehaviour, IOnOtherCardDieInHand {
        public static Ability ability;
        public override Ability Ability => ability;
        public bool IsValidTarget(CardSlot slot) {
            if (slot.Card != null && !slot.Card.HasStatusEffect<Pebble>() && slot.Card.LacksAllTraits(Trait.Terrain, Trait.Pelt)) {
                return base.Card.OpponentCard == slot.Card.OpponentCard || (slot.Card.OpponentCard && base.Card.OriginatedFromQueue);
            }
            return false;
        }
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => IsValidTarget(slot);
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            yield return slot.Card.AddStatusEffectToFaceDown<Pebble>();
            yield return CombatHelpers.QueueOrCreateDrawnCard(base.Card.Info.Clone() as CardInfo, slot.Card.OpponentCard);
            yield return base.LearnAbility(0.4f);
        }

        public bool RespondsToOtherCardDieInHand(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            return card.OpponentCard == base.Card.OpponentCard && card.HasStatusEffect<Pebble>();
        }

        public IEnumerator OnOtherCardDieInHand(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            List<PlayableCard> cards = BoardManager.Instance.GetCards(!base.Card.OpponentCard, x => !x.Dead);
            foreach (PlayableCard c in cards) {
                if (c.HasStatusEffect<Pebble>()) {
                    yield return c.Die(false);
                }
                else {
                    yield return c.AddStatusEffect<Grief>(1, modifyTurnGained: delegate (int i) { return ModifyTurnGriefGained(c, i); });
                }
            }
        }

        private int ModifyTurnGriefGained(PlayableCard c, int i) {
            // if the card was killed during the opponent's turn, don't modify the turn number
            if (c.OpponentCard != TurnManager.Instance.IsPlayerTurn) {
                return i;
            }
            return i + 1;
        }

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.QueuedCardIsBlocked))]
        private static void DontPlayLonelyIfAllPebbles(PlayableCard queuedCard, ref bool __result) {
            if (!__result && queuedCard.HasAbility(Lonely.ability)) {
                // don't play Lonely cards from the queue if the opponent doesn't have any valid target cards
                __result = BoardManager.Instance.GetOpponentCards(x => x.LacksAllTraits(Trait.Terrain, Trait.Pelt) && !x.HasStatusEffect<Pebble>()).Count == 0;
            }
        }
    }
}
