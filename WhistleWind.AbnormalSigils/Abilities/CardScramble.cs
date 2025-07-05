using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

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
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// Pay 3 Energy to shuffle the position of all creatures on the board.
    /// </summary>
    public class CardScramble : DelayedActivatedAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingEnergyCost => 1;

        public override IEnumerator Activate() {
            int rand = base.GetRandomSeed();
            List<Tuple<PlayableCard, CardSlot>> cardAndNewSlot = new();
            List<CardSlot> allSlots = GetOccupiedSlotsMovable(BoardManager.Instance.AllSlotsCopy)
                .Concat(BoardManager.Instance.GetPlayerOpenSlots())
                .Concat(BoardManager.Instance.GetOpponentOpenSlots())
                .ToList();

            List<CardSlot> allSlotsCopy = new(allSlots);
            foreach (PlayableCard card in UnassignCardsFromSlots(allSlotsCopy)) {
                CardSlot newSlot = allSlotsCopy.GetSeededRandom(rand++);
                cardAndNewSlot.Add(new(card, newSlot));
                allSlotsCopy.Remove(newSlot);
            }

            for (int i = 0; i < 2; i++) {
                allSlotsCopy = new(allSlots);
                foreach (Tuple<PlayableCard, CardSlot> pair in cardAndNewSlot) {
                    CardSlot slot = allSlotsCopy.GetSeededRandom(rand++);
                    allSlotsCopy.Remove(slot);

                    CustomCoroutine.Instance.StartCoroutine(RelocateCardToSlot(pair.Item1, slot, 0.1f));
                    yield return new WaitForSeconds(0.1f);
                }
            }

            foreach (CardSlot slot in allSlotsCopy.Where(x => x.Card != null)) {
                slot.Card.Slot = null;
                slot.Card = null;
            }
            foreach (Tuple<PlayableCard, CardSlot> pair in cardAndNewSlot.Where(x => x.Item1.Slot != null)) {
                pair.Item1.Slot.Card = null;
                pair.Item1.Slot = null;
            }

            foreach (Tuple<PlayableCard, CardSlot> pair in cardAndNewSlot) {
                CustomCoroutine.Instance.StartCoroutine(MoveToNewSlot(pair.Item1, pair.Item2, 0.1f));
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }

        public override bool CanActivateOpponent() // make opponent activation rarer
        {
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

        public static IEnumerator RandomiseCardsInSlots(List<CardSlot> slots, int randomSeed, float waitAfter = 0.5f, Func<CardSlot, int> sortPredicate = null) {
            //AbnormalPlugin.Log.LogDebug("[CardScramble.RandomiseCardsInSlots] Start");
            if (slots.Count == 0)
                yield break;

            if (sortPredicate != null)
                slots.Sort((CardSlot a, CardSlot b) => sortPredicate(b) - sortPredicate(a));

            List<PlayableCard> cards = UnassignCardsFromSlots(slots);
            List<CardSlot> openSlots = BoardManager.Instance.GetOpenSlots(!cards[0].OpponentCard);
            //AbnormalPlugin.Log.LogDebug($"[CardScramble.RandomiseCardsInSlots] Open: {openSlots.Count} Cards: {cards.Count}");
            foreach (PlayableCard card in cards) {
                if (openSlots.Count == 0)
                    break;

                CardSlot slot = openSlots.GetSeededRandom(randomSeed++);
                if (openSlots.Count == 1 && openSlots[0] == card.Slot) {
                    // assign back to its original slot
                    slot.Card = card;
                    card.Slot = slot;
                    break;
                }

                while (slot == card.Slot) {
                    slot = openSlots.GetSeededRandom(randomSeed++);
                }

                //AbnormalPlugin.Log.LogDebug("Move to new slot");
                card.Slot = null;
                openSlots.Remove(slot);
                CustomCoroutine.Instance.StartCoroutine(MoveToNewSlot(card, slot, 0.1f));
                yield return new WaitForSeconds(0.1f);
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

            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(card, slot);
            yield return new WaitForSeconds(waitAfter);
        }
    }
}
