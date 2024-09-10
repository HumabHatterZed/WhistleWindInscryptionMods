using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Cycler()
        {
            const string rulebookName = "Cycler";
            const string rulebookDescription = "At the end of the owner's turn, this card moves in the sigil's direction, looping around the owner's side of the board.";
            const string dialogue = "A never-ending cycle.";
            const string triggerText = "[creature] moves to a new space, going around its side of the board.";
            Cycler.ability = AbnormalAbilityHelper.CreateAbility<Cycler>(
                "sigilCycler",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    public class Cycler : Strafe
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

            yield return new WaitForSeconds(0.05f);
            yield return base.PreSuccessfulTriggerSequence();

            if (destination != null && destinationValid) // end of sequence
            {
                if (canLoopAround) // if this card is at the end of the board, cycle to the other side
                {
                    yield return LoopToSlot(destination, oldSlot);
                    yield return base.LearnAbility();
                }
                else // standard movement behaviour
                    yield return MoveToSlot(destination, oldSlot);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }
        }
        private IEnumerator LoopToSlot(CardSlot destination, CardSlot oldSlot)
        {
            // Move out of slot
            Vector3 vector = base.Card.Slot.IsPlayerSlot ? Vector3.back : Vector3.forward;
            Tween.Position(base.Card.transform, base.Card.transform.position + vector * 2f + Vector3.up * 0.25f, 0.15f, 0f, Tween.EaseOut);
            yield return new WaitForSeconds(0.15f);
            Tween.Position(base.Card.transform, new Vector3(destination.transform.position.x, base.Card.transform.position.y, base.Card.transform.position.z), 0.1f, 0f);
            yield return new WaitForSeconds(0.1f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.Card, destination);
            yield return base.PostSuccessfulMoveSequence(oldSlot);
            yield return new WaitForSeconds(0.25f);
        }
        private IEnumerator MoveToSlot(CardSlot destination, CardSlot oldSlot)
        {
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.Card, destination);
            yield return base.PostSuccessfulMoveSequence(oldSlot);
            yield return new WaitForSeconds(0.25f);
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
