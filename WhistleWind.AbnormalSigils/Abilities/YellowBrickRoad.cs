using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_YellowBrickRoad() {
            const string rulebookName = "Follow the Leader";
            const string rulebookDescription = "At the end of its owner's turn, this card moves in the sigil's direction, looping around the side of the board. Allied cards move towards this card as far as possible, following the same rules.";
            const string dialogue = "Let's go, together.";
            const string triggerText = "[creature] leads your creatures forward.";
            YellowBrickRoad.ID = AbnormalAbilityHelper.CreateAbility<YellowBrickRoad>(
                "sigilYellowBrickRoad", rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2, special: true)
                .Id;
        }
    }
    /// <summary>
    /// At the end of its owner's turn, this card moves in the sigil's direction, looping around the side of the board. Allied cards move towards this card as far as possible, following the same rules.
    /// </summary>
    public class YellowBrickRoad : Strafe {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.25f);
            yield return this.DoStrafe(toLeft, toRight);
        }
        public override IEnumerator DoStrafe(CardSlot toLeft, CardSlot toRight) {
            if (base.Card.HasTrait(Trait.Giant)) // do nothing for giant cards
                yield break;

            List<CardSlot> slotsCopy = BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard);
            CardSlot oldSlot = base.Card.Slot;

            if (base.Card.LacksAbility(Unyielding.ID)) {
                CardSlot destination;
                bool destinationValid;
                bool atEndOfBoard = base.Card.Slot == slotsCopy.First() || base.Card.Slot == slotsCopy.Last();

                // if slot exists and card is empty
                bool canMoveLeftNormally = toLeft != null && toLeft.Card == null;
                bool canMoveRightNormally = toRight != null && toRight.Card == null;

                // if at the end of the board and we can't move normally, see if it's possible for us to loop around
                bool canLoopAround = CheckIfCanLoop(atEndOfBoard, base.movingLeft, canMoveLeftNormally, canMoveRightNormally,
                    base.Card.Slot, slotsCopy.First(), slotsCopy.Last());

                // switch direction if we can't move
                if (base.movingLeft && !canMoveLeftNormally && !canLoopAround) {
                    base.movingLeft = false;
                    // update to see if can loop
                    canLoopAround = CheckIfCanLoop(atEndOfBoard, base.movingLeft, canMoveLeftNormally, canMoveRightNormally,
                        base.Card.Slot, slotsCopy.First(), slotsCopy.Last());
                }
                if (!base.movingLeft && !canMoveRightNormally && !canLoopAround) {
                    base.movingLeft = true;
                    canLoopAround = CheckIfCanLoop(atEndOfBoard, base.movingLeft, canMoveLeftNormally, canMoveRightNormally,
                        base.Card.Slot, slotsCopy.First(), slotsCopy.Last());
                }

                // flip card
                base.Card.RenderInfo.SetAbilityFlipped(Ability, base.movingLeft);
                base.Card.RenderInfo.flippedPortrait = base.movingLeft && base.Card.Info.flipPortraitForStrafe;
                base.Card.RenderCard();

                // different destination and validation if we're looping or moving normally
                if (canLoopAround) {
                    destination = base.movingLeft ? slotsCopy.Last() : slotsCopy.First();
                    destinationValid = destination.Card == null;
                }
                else {
                    destination = base.movingLeft ? toLeft : toRight;
                    destinationValid = base.movingLeft ? canMoveLeftNormally : canMoveRightNormally;
                }

                if (destination != null && destinationValid) {
                    yield return base.PreSuccessfulTriggerSequence();
                    if (canLoopAround) // if this card is at the end of the board, cycle to the other side
                        yield return MoveToEndOfBoard(base.Card, destination, oldSlot, destinationValid);
                    else // standard movement behaviour
                        yield return MoveToAdjacentSlot(base.Card, destination, destinationValid);
                    yield return base.PostSuccessfulMoveSequence(oldSlot);
                }
                else {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.15f);
                }
            }
            else {
                yield return Unyielding.OnPreventMovement(this);
            }

            // we don't want to move the base card again
            // and we remove cards that cannot be moved from consideration
            slotsCopy.Remove(base.Card.Slot);
            //allySlots.RemoveAll(x => x.Card != null && (x.Card.HasAnyOfTraits(Trait.Terrain, Trait.Pelt) || !Unyielding.CardCanBeMoved(x.Card)));
            
            // if there are other cards to move
            if (slotsCopy.Count > 0) {
                yield return base.LearnAbility();
                yield return MoveFollowingCards(oldSlot, BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard));
            }
        }
        private IEnumerator MoveToAdjacentSlot(PlayableCard target, CardSlot destination, bool destinationValid) {
            if (destination != null && destinationValid) {
                yield return Singleton<BoardManager>.Instance.AssignCardToSlot(target, destination);
                yield return new WaitForSeconds(0.25f);
            }
            else {
                target.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }
        }
        private IEnumerator MoveToEndOfBoard(PlayableCard target, CardSlot destination, CardSlot oldSlot, bool destinationValid) {
            yield return new WaitForSeconds(0.05f);
            yield return base.PreSuccessfulTriggerSequence();

            if (destination != null && destinationValid) {
                // Move out of slot
                Vector3 vector = target.Slot.IsPlayerSlot ? Vector3.back : Vector3.forward;
                Tween.Position(target.transform, target.transform.position + vector * 2f + Vector3.up * 0.25f, 0.15f, 0f, Tween.EaseOut);
                yield return new WaitForSeconds(0.15f);
                Tween.Position(target.transform, new Vector3(destination.transform.position.x, target.transform.position.y, target.transform.position.z), 0.1f, 0f);
                yield return new WaitForSeconds(0.1f);
                yield return Singleton<BoardManager>.Instance.AssignCardToSlot(target, destination);
                yield return new WaitForSeconds(0.25f);
            }
            else {
                target.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }
        }
        private IEnumerator MoveFollowingCards(CardSlot oldSlot, List<CardSlot> boardSlots) {
            // create a copy of Paved Slots to use for iteration
            List<CardSlot> boardSlotsCopy = new(boardSlots);

            if (!base.movingLeft)
                boardSlotsCopy.Reverse();

            // when we iterate, we want to check oldSlot first, then the slots behind this card,
            // then the slots ahead of it based on this card's direction
            // 2 1 0 3
            List<CardSlot> orderedSlots = new() { oldSlot };
            List<CardSlot> orderedMoveTargets;
            // add slots behind the card
            orderedSlots.AddRange(boardSlotsCopy.FindAll(x => base.movingLeft ? x.Index > oldSlot.Index : x.Index < oldSlot.Index));
            // add slots in front of the card
            orderedSlots.AddRange(boardSlotsCopy.FindAll(x => base.movingLeft ? x.Index < oldSlot.Index : x.Index > oldSlot.Index));

            orderedMoveTargets = orderedSlots.FindAll(x => x.Card != null && Unyielding.CardCanBeMoved(x.Card));

            if (orderedMoveTargets.Count == 0) {
                //Debug.Log("No valid movement targets");
                yield break;
            }

            // go through all possible movement targets
            foreach (CardSlot slot in orderedMoveTargets) {
                //Debug.Log($"Target to move: {slot.Index}");
                if (slot.Card == null || slot == base.Card.Slot) {
                    continue;
                }

                CardSlot destination = oldSlot;
                //Debug.Log($"Initial dest: {destination.Index}");

                if (destination.Card != null) {
                    foreach (CardSlot dest in orderedSlots) {
                        // get the dest card closest to the target card
                        CardSlot nextPossibleDest = BoardManager.Instance.GetAdjacent(destination, !base.movingLeft);
                        nextPossibleDest ??= base.movingLeft ? boardSlots[0] : boardSlots[boardSlots.Count - 1];

                        if (nextPossibleDest != base.Card.Slot && nextPossibleDest.Card == null) {
                            destination = nextPossibleDest;
                            //Debug.Log($"Update dest: {destination.Index}");
                        }
                    }
                }

                bool destAtEndOfBoard = destination == boardSlots[0] || destination == boardSlots[boardSlots.Count - 1];
                bool slotAtEndOfBoard = slot == boardSlots[0] || slot == boardSlots[boardSlots.Count - 1];

                bool destinationValid = destination != null && destination.Card == null;
                if (base.movingLeft) {
                    if (destination.Index > slot.Index) {
                        yield return MoveToEndOfBoard(slot.Card, destination, slot, destinationValid);
                    }
                    else {
                        yield return MoveToAdjacentSlot(slot.Card, destination, destination != null && destination.Card == null);
                    }
                }
                else if (destination.Index < slot.Index) {
                    yield return MoveToEndOfBoard(slot.Card, destination, slot, destinationValid);
                }
                else {
                    yield return MoveToAdjacentSlot(slot.Card, destination, destination != null && destination.Card == null);
                }
            }
        }

        private static bool CheckIfCanLoop(bool atEndOfBoard, bool movingLeft, bool canMoveLeft, bool canMoveRight, CardSlot originalSlot, CardSlot firstSlot, CardSlot lastSlot) {
            if (atEndOfBoard) {
                if (movingLeft && !canMoveLeft && originalSlot == firstSlot)
                    return lastSlot.Card == null;
                if (!movingLeft && !canMoveRight && originalSlot == lastSlot)
                    return firstSlot.Card == null;
            }
            return false;
        }
    }
}
