using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_CardScramble() {
            const string rulebookName = "Board Shuffle";
            const string rulebookDescription = "Pay 3 Energy to shuffle the position of all creatures on the board.";
            const string dialogue = "What a mess.";
            CardScramble.ability = AbnormalAbilityHelper.CreateAbility<CardScramble>(
                "sigilCardScramble",
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: false)
                .Id;
        }
    }
    /// <summary>
    /// Pay 3 Energy to shuffle the position of all creatures on the board.
    /// </summary>
    public class CardScramble : DelayedActivatedAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingEnergyCost => 3;

        public override bool CanActivate() {
            return base.CanActivate() && GetOccupiedSlotsMovable(BoardManager.Instance.AllSlotsCopy).Count > 0;
        }
        public override IEnumerator Activate() {
            yield return base.PreSuccessfulTriggerSequence();
            yield return RandomiseCardsInSlots(
                GetOccupiedSlotsMovable(BoardManager.Instance.AllSlotsCopy),
                BoardManager.Instance.AllSlotsCopy,
                base.GetRandomSeed()
                );

            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }

        // make opponent activation rarer
        public override bool CanActivateOpponent() {
            return base.CanActivateOpponent() && SeededRandom.Bool((base.GetRandomSeed() + 1) * 2);
        }

        /**
         * Filters the given List of CardSlots and returns a copy containing all slots that contain Cards that can be moved (non-Giant, non-Unyielding)
         * <param name="slots" List of CardSlots to filter
         */
        public static List<CardSlot> GetOccupiedSlotsMovable(List<CardSlot> slots) {
            slots.RemoveAll(x => x.Card == null || x.Card.HasAbility(Unyielding.ability) || x.Card.HasAnyOfTraits(Trait.Giant, Trait.Structure));
            return slots;
        }

        public static List<PlayableCard> UnassignCardsFromSlots(List<CardSlot> slots, bool nullifyCardSlot = false) {
            List<PlayableCard> unassignedCards = new();
            foreach (CardSlot slot in slots) {
                PlayableCard card = slot.Card;
                if (card == null) continue;
                if (!unassignedCards.Contains(card))
                    unassignedCards.Add(card);

                card.UnassignFromSlot();
                if (nullifyCardSlot)
                    card.Slot = null;
            }
            return unassignedCards;
        }

        public static IEnumerator RelocateCardToSlot(PlayableCard card, CardSlot slot, float transitionDuration) {
            float x = slot.transform.position.x;
            float y = slot.transform.position.y + 0.1f;
            float z = slot.transform.position.z;

            card.SetEnabled(false);
            Tween.Position(card.transform, new Vector3(x, y, z), 0.2f, 0.05f, Tween.EaseOut);

            yield return new WaitForSeconds(0.2f);

            Tween.LocalPosition(card.transform, Vector3.up * (BoardManager.Instance.SlotHeightOffset + card.SlotHeightOffset), transitionDuration, 0.05f, Tween.EaseOut, Tween.LoopType.None, null, delegate {
                card.Anim.PlayRiffleSound();
            });

            yield return new WaitForSeconds(transitionDuration);
        }

        public static bool OpposingCardCanKill(PlayableCard current, PlayableCard opposing) {
            if (opposing == null) return false;

            bool weakToInstaDeath = current.CanBeInstaKilled();
            // opposing can attack (not counting multistrike sigils)
            if (opposing.Attack > 0) {
                if (current.HasShield() && current.LacksAbility(Piercing.ability)) {
                    return false;
                }

                // if we can regular die
                if (opposing.Attack >= current.Health) {
                    return true;
                }

                if (weakToInstaDeath && opposing.HasAbility(Ability.Deathtouch)) {
                    return true;
                }
            }
            
            // if we can attack (not counting multistrike sigils)
            if (current.Attack > 0) {
                // if we will trigger Punisher
                if (weakToInstaDeath && opposing.HasAbility(Punisher.ability) && current.Attack >= opposing.Health) {
                    return true;
                }

                // if reflector will murk us
                if (opposing.HasAbility(Reflector.ability) && current.Attack >= current.Health) {
                    return true;
                }

                int sharpStacks = opposing.GetAbilityStacks(Ability.Sharp) + opposing.GetAbilityStacks(Bloodletter.ability);
                if (sharpStacks > 0) {
                    return current.Health <= sharpStacks;
                }
            }

            return false;
        }

        /// <summary>
        /// Randomly moves the cards in the given slots around on their owner's side of the board.
        /// </summary>
        /// <param name="slots">Slots whose cards need to be randomised</param>
        /// <param name="randomSeed">Random seed to base new slots on.</param>
        /// <param name="opponent">If true, be more discerning where cards are placed.</param>
        public static IEnumerator RandomiseCardsInSlots(List<CardSlot> slots, List<CardSlot> allOpenSlots, int randomSeed, bool smartCheck = false, float waitAfter = 0.5f, Func<CardSlot, int> sortPredicate = null) {
            //AbnormalPlugin.Log.LogDebug("[CardScramble.RandomiseCardsInSlots] Start");
            if (slots.Count == 0)
                yield break;

            allOpenSlots.RemoveAll(x => x.Card != null && !slots.Contains(x)); // remove occupied slots that aren't being randomised

            if (sortPredicate != null)
                slots.Sort((CardSlot a, CardSlot b) => sortPredicate(b) - sortPredicate(a));

            List<PlayableCard> cards = UnassignCardsFromSlots(slots);
            List<CardSlot> oldSlots = new();
            //AbnormalPlugin.Log.LogDebug($"[CardScramble.RandomiseCardsInSlots] Open: {allOpenSlots.Count} Cards: {cards.Count}");
            foreach (PlayableCard card in cards) {
                CardSlot slot;
                if (allOpenSlots.Count > 0) {
                    List<CardSlot> bestSlots = new(allOpenSlots);

                    if (smartCheck) {
                        bestSlots.RemoveAll(x => OpposingCardCanKill(card, x.Card));
                    }

                    if (bestSlots.Count > 0) {
                        //AbnormalPlugin.Log.LogDebug($"[CardScramble.RandomiseCardsInSlots] select best slot");
                        slot = bestSlots.GetSeededRandom(randomSeed++);
                    }
                    //else if (allOpenSlots.Contains(card.Slot)) {
                    //    slot = card.Slot;
                    //}
                    else {
                        //AbnormalPlugin.Log.LogDebug($"[CardScramble.RandomiseCardsInSlots] random open");
                        slot = allOpenSlots.GetSeededRandom(randomSeed++);
                    }

                    //AbnormalPlugin.Log.LogDebug("Move to new slot");
                    allOpenSlots.Remove(slot);
                }
                else {
                    //AbnormalPlugin.Log.LogDebug($"[CardScramble.RandomiseCardsInSlots] same slot");
                    slot = card.Slot;
                }
                oldSlots.Add(card.Slot);
                card.Slot = null;
                slot.Card = card; // prevent new cards being created when moving to a new slot (Bone Elk, etc.)
                CustomCoroutine.Instance.StartCoroutine(MoveToNewSlot(card, slot, 0.1f));
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);

            // manually trigger assign to slot triggers after all cards have been moved
            for (int i = 0; i < cards.Count; i++) {
                PlayableCard card = cards[i];
                CardSlot slot = oldSlots[i];
                if (slot != card.Slot) {
                    yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.OtherCardAssignedToSlot, false, card);
                    yield return CustomTriggerFinder.TriggerAll(triggerFacedown: false, (IOnCardAssignedToSlotContext x) => x.RespondsToCardAssignedToSlotContext(card, slot, card.Slot), (IOnCardAssignedToSlotContext x) => x.OnCardAssignedToSlotContext(card, slot, card.Slot));
                    yield return CustomTriggerFinder.TriggerInHand((IOnOtherCardAssignedToSlotInHand x) => x.RespondsToOtherCardAssignedToSlotInHand(card), (IOnOtherCardAssignedToSlotInHand x) => x.OnOtherCardAssignedToSlotInHand(card));
                    yield return CustomTriggerFinder.TriggerAll(triggerFacedown: false, (IOnCardAssignedToSlotNoResolve x) => x.RespondsToCardAssignedToSlotNoResolve(card), (IOnCardAssignedToSlotNoResolve x) => x.OnCardAssignedToSlotNoResolve(card));
                }
            }

            yield return new WaitForSeconds(waitAfter);
        }

        public static IEnumerator MoveToNewSlot(PlayableCard card, CardSlot slot, float waitAfter, bool showTarget = false) {
            float x = slot.transform.position.x;
            float y = slot.transform.position.y + 0.1f;
            float z = slot.transform.position.z;

            GameObject targetIcon = null;
            if (showTarget) {
                targetIcon = TargetIconHelper.CreateTargetIcon(slot);
                yield return new WaitForSeconds(0.25f);
            }

            // if we're moving to the other side of the board
            if (card.OpponentCard == slot.IsPlayerSlot) {
                card.SetIsOpponentCard(!card.OpponentCard);
                card.transform.eulerAngles += new Vector3(0f, 0f, -180f);
            }

            Tween.Position(card.transform, new Vector3(x, y, z), 0.2f, 0f, Tween.EaseOut);
            yield return new WaitForSeconds(0.25f);

            if (targetIcon != null)
                TargetIconHelper.CleanUpTargetIcon(targetIcon);

            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(card, slot, resolveTriggers: false);
            //AbnormalPlugin.Log.LogDebug($"[CardScramble.RandomiseCardsInSlots] assign to slot {card} {slot.Index}");
            yield return new WaitForSeconds(waitAfter);
        }
    }
}
