using DiskCardGame;
using InscryptionAPI.Card;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Barreler()
        {
            const string rulebookName = "Barreler";
            const string rulebookDescription = "At the end of the owner's turn, this card moves in the sigil's direction to the end of the board, moving any cards in the way.";
            const string dialogue = "Make room.";
            Barreler.ability = AbnormalAbilityHelper.CreateAbility<Barreler>(
                Artwork.sigilBarreler, Artwork.sigilBarreler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: true, opponent: true).Id;
        }
    }

    public class Barreler : Strafe
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override IEnumerator DoStrafe(CardSlot toLeft, CardSlot toRight)
        {
            if (base.Card.HasTrait(Trait.Giant)) // do nothing for giant cards
                yield break;

            List<CardSlot> allySlots = HelperMethods.GetSlotsCopy(base.Card.OpponentCard);
            CardSlot oldSlot = base.Card.Slot;

            CardSlot destination;
            bool destinationValid;

            // if slot exists and card is empty
            bool canMoveLeft = toLeft != null;
            bool canMoveRight = toRight != null;

            // switch direction if we can't move
            if (base.movingLeft && !canMoveLeft)
                base.movingLeft = false;
            if (!base.movingLeft && !canMoveRight)
                base.movingLeft = true;

            // flip card
            base.Card.RenderInfo.SetAbilityFlipped(Ability, base.movingLeft);
            base.Card.RenderInfo.flippedPortrait = base.movingLeft && base.Card.Info.flipPortraitForStrafe;
            base.Card.RenderCard();

            // different destination and validation if we're looping or moving normally
            destination = base.movingLeft ? allySlots.First() : allySlots.Last();
            destinationValid = destination != null && oldSlot != null;

            if (destination != null && destinationValid)
            {
                yield return SwapSlots(destination, oldSlot, allySlots);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }

            if (destination != null && destinationValid) // end of sequence
            {
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.LearnAbility();
            }
        }
        private IEnumerator SwapSlots(CardSlot destination, CardSlot originalSlot, List<CardSlot> boardSlots)
        {
            List<CardSlot> cardSlotsCopy = new(boardSlots);
            if (base.movingLeft)
                cardSlotsCopy.Reverse();

            List<CardSlot> orderedSlots = cardSlotsCopy.FindAll(x => base.movingLeft ? x.Index < originalSlot.Index : x.Index > originalSlot.Index);

            Dictionary<PlayableCard, CardSlot> swappedCards = new();
            CardSlot slotToMoveTo = originalSlot;
            foreach (CardSlot slot in orderedSlots)
            {
                if (slot.Card != null)
                {
                    PlayableCard swappedCard = slot.Card;
                    float x = (swappedCard.Slot.transform.position.x + slotToMoveTo.transform.position.x) / 2f;
                    float y = swappedCard.transform.position.y + 0.35f;
                    float z = swappedCard.transform.position.z;
                    Tween.Position(swappedCard.transform, new Vector3(x, y, z), 0.3f, slot.Index * 0.01f, Tween.EaseOut);
                    swappedCards.Add(swappedCard, slotToMoveTo);
                }
                slotToMoveTo = slot;
            }

            yield return MoveToAdjacentSlot(base.Card, destination, true);

            foreach (KeyValuePair<PlayableCard, CardSlot> pair in swappedCards)
            {
                if (pair.Key != null && pair.Key.NotDead())
                    yield return Singleton<BoardManager>.Instance.AssignCardToSlot(pair.Key, pair.Value);
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
    }
}
