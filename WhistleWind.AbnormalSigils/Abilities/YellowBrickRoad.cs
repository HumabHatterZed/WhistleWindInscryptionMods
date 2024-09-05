using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_YellowBrickRoad()
        {
            const string rulebookName = "Follow the Leader";
            const string rulebookDescription = "At the end of its owner's turn, this card moves in the sigil's direction, looping around the owner's side of the board. Allied creatures towards this card in the sigil's direction as far as possible.";
            const string dialogue = "Let's go, together.";
            const string triggerText = "[creature] leads your creatures forward.";
            YellowBrickRoad.ability = AbnormalAbilityHelper.CreateAbility<YellowBrickRoad>(
                "sigilYellowBrickRoad", rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2, special: true).Id;
        }
    }

    public class YellowBrickRoad : Strafe
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override IEnumerator DoStrafe(CardSlot toLeft, CardSlot toRight)
        {
            if (base.Card.HasTrait(Trait.Giant)) // do nothing for giant cards
                yield break;

            List<CardSlot> allySlots = BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard);
            CardSlot oldSlot = base.Card.Slot;

            CardSlot destination;
            bool destinationValid;

            bool atEndOfBoard = base.Card.Slot == allySlots.First() || base.Card.Slot == allySlots.Last();

            // if slot exists and card is empty
            bool canMoveLeftNormally = toLeft != null && toLeft.Card == null;
            bool canMoveRightNormally = toRight != null && toRight.Card == null;

            // if at the end of the board and we can't move normally, see if it's possible for us to loop around
            bool canLoopAround = CheckIfCanLoop(atEndOfBoard, base.movingLeft, canMoveLeftNormally, canMoveRightNormally,
                base.Card.Slot, allySlots.First(), allySlots.Last());

            // switch direction if we can't move
            if (base.movingLeft && !canMoveLeftNormally && !canLoopAround)
            {
                base.movingLeft = false;
                // update to see if can loop
                canLoopAround = CheckIfCanLoop(atEndOfBoard, base.movingLeft, canMoveLeftNormally, canMoveRightNormally,
                    base.Card.Slot, allySlots.First(), allySlots.Last());
            }
            if (!base.movingLeft && !canMoveRightNormally && !canLoopAround)
            {
                base.movingLeft = true;
                canLoopAround = CheckIfCanLoop(atEndOfBoard, base.movingLeft, canMoveLeftNormally, canMoveRightNormally,
                    base.Card.Slot, allySlots.First(), allySlots.Last());
            }

            // flip card
            base.Card.RenderInfo.SetAbilityFlipped(Ability, base.movingLeft);
            base.Card.RenderInfo.flippedPortrait = base.movingLeft && base.Card.Info.flipPortraitForStrafe;
            base.Card.RenderCard();

            // different destination and validation if we're looping or moving normally
            if (canLoopAround)
            {
                destination = base.movingLeft ? allySlots.Last() : allySlots.First();
                destinationValid = destination.Card == null;
            }
            else
            {
                destination = base.movingLeft ? toLeft : toRight;
                destinationValid = base.movingLeft ? canMoveLeftNormally : canMoveRightNormally;
            }

            if (destination != null && destinationValid)
            {
                if (canLoopAround) // if this card is at the end of the board, cycle to the other side
                    yield return MoveToEndOfBoard(base.Card, destination, oldSlot, destinationValid);
                else // standard movement behaviour
                    yield return MoveToAdjacentSlot(base.Card, destination, destinationValid);
                yield return base.PostSuccessfulMoveSequence(oldSlot);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }

            // if there are other cards to move
            if (allySlots.FindAll(x => x.Card != null && x.Card != base.Card && x.Card.LacksAbility(Unyielding.ability)).Count > 0)
            {
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.LearnAbility();
                yield return MoveFollowingCards(oldSlot, allySlots);
            }
        }
        private IEnumerator MoveToAdjacentSlot(PlayableCard target, CardSlot destination, bool destinationValid)
        {
            if (destination != null && destinationValid)
            {
                yield return Singleton<BoardManager>.Instance.AssignCardToSlot(target, destination);
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                target.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }
        }
        private IEnumerator MoveToEndOfBoard(PlayableCard target, CardSlot destination, CardSlot oldSlot, bool destinationValid)
        {
            yield return new WaitForSeconds(0.05f);
            yield return base.PreSuccessfulTriggerSequence();

            if (destination != null && destinationValid)
            {
                // Move out of slot
                Vector3 vector = target.Slot.IsPlayerSlot ? Vector3.back : Vector3.forward;
                Tween.Position(target.transform, target.transform.position + vector * 2f + Vector3.up * 0.25f, 0.15f, 0f, Tween.EaseOut);
                yield return new WaitForSeconds(0.15f);
                Tween.Position(target.transform, new Vector3(destination.transform.position.x, target.transform.position.y, target.transform.position.z), 0.1f, 0f);
                yield return new WaitForSeconds(0.1f);
                yield return Singleton<BoardManager>.Instance.AssignCardToSlot(target, destination);
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                target.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }
        }
        private IEnumerator MoveFollowingCards(CardSlot oldSlot, List<CardSlot> boardSlots)
        {
            // create a copy of Paved Slots to use for iteration
            List<CardSlot> boardSlotsCopy = new(boardSlots);

            if (!base.movingLeft)
                boardSlotsCopy.Reverse();

            // when we iterate, we can to check oldSlot first, then the slots behind this card, then the slots ahead of it
            // based on this card's direction
            List<CardSlot> orderedSlots = new() { oldSlot };

            // add slots behind the card
            orderedSlots.AddRange(boardSlotsCopy.FindAll(x => base.movingLeft ? x.Index > oldSlot.Index : x.Index < oldSlot.Index));
            // add slots in front of the card
            orderedSlots.AddRange(boardSlotsCopy.FindAll(x => base.movingLeft ? x.Index < oldSlot.Index : x.Index > oldSlot.Index));

            // we want following cards to cling as close as possible to the base card
            bool multipleMovements = true;
            while (multipleMovements)
            {
                // break loop if oldSlot is occupied or there aren't any cards to loop
                multipleMovements = oldSlot.Card == null;
                CardSlot destination = oldSlot;
                foreach (CardSlot slot in orderedSlots)
                {
                    CardSlot slotToCheck = Singleton<BoardManager>.Instance.GetAdjacent(destination, adjacentOnLeft: !base.movingLeft);
                    // if the slot is null, check for loop
                    slotToCheck ??= base.movingLeft ? boardSlots.First() : boardSlots.Last();

                    bool atEndOfBoardSlot = destination == boardSlots.First() || destination == boardSlots.Last();
                    bool atEndOfBoardSlotToCheck = slotToCheck == boardSlots.First() || slotToCheck == boardSlots.Last();

                    if (slotToCheck.Card != null && destination.Card == null &&
                        slotToCheck != base.Card.Slot && destination != base.Card.Slot)
                    {
                        if (atEndOfBoardSlot && atEndOfBoardSlotToCheck)
                            yield return MoveToEndOfBoard(slotToCheck.Card, destination, slotToCheck.Card.Slot, destination != null && destination.Card == null);
                        else
                            yield return MoveToAdjacentSlot(slotToCheck.Card, destination, destination != null && destination.Card == null);
                    }
                    destination = slotToCheck;
                }
            }
        }

        private static bool CheckIfCanLoop(bool atEndOfBoard, bool movingLeft, bool canMoveLeft, bool canMoveRight, CardSlot originalSlot, CardSlot firstSlot, CardSlot lastSlot)
        {
            if (atEndOfBoard)
            {
                if (movingLeft && !canMoveLeft && originalSlot == firstSlot)
                    return lastSlot.Card == null;
                if (!movingLeft && !canMoveRight && originalSlot == lastSlot)
                    return firstSlot.Card == null;
            }
            return false;
        }
    }
}
