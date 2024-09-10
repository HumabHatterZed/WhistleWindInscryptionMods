using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
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
        private void Ability_Barreler()
        {
            const string rulebookName = "Barreler";
            const string rulebookDescription = "At the end of the owner's turn, this card moves in the sigil's direction through other cards to the furthest empty space.";
            const string dialogue = "This beast is in quite the rush.";
            const string triggerText = "[creature] barrels on through!";
            Barreler.ability = AbnormalAbilityHelper.CreateAbility<Barreler>(
                "sigilBarreler",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    public class Barreler : Strafe
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override IEnumerator DoStrafe(CardSlot toLeft, CardSlot toRight)
        {
            // if this card can move at least one slot in either direction
            bool canMoveLeft = toLeft != null && GetFurthestEmptySlot(true, toLeft) != null;
            bool canMoveRight = toRight != null && GetFurthestEmptySlot(false, toRight) != null;

            // switch direction if we can't move
            if (base.movingLeft && !canMoveLeft)
                base.movingLeft = false;

            if (!base.movingLeft && !canMoveRight)
                base.movingLeft = true;

            
            bool destinationValid = canMoveLeft || canMoveRight;
            if (destinationValid)
            {
                CardSlot destination = GetFurthestEmptySlot(base.movingLeft, base.movingLeft ? toLeft : toRight);
                yield return BarrelThroughCards(base.movingLeft, destination);
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.LearnAbility();
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }
        }
        private IEnumerator BarrelThroughCards(bool movingLeft, CardSlot destination)
        {
            List<CardSlot> barrelSlots = BoardManager.Instance.GetCardSlots(!base.Card.OpponentCard, x => x.Card != null);
            if (movingLeft)
            {
                barrelSlots.RemoveAll(x => x.Index >= base.Card.Slot.Index || x.Index <= destination.Index);
                barrelSlots.Reverse();
            }
            else
            {
                barrelSlots.RemoveAll(x => x.Index <= base.Card.Slot.Index || x.Index >= destination.Index);
            }

            base.Card.RenderInfo.SetAbilityFlipped(this.Ability, base.movingLeft);
            base.Card.RenderInfo.flippedPortrait = base.movingLeft && base.Card.Info.flipPortraitForStrafe;
            base.Card.RenderCard();
            base.Card.Anim.StrongNegationEffect();
            CardSlot oldSlot = base.Card.Slot;
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.Card, destination);
            foreach (CardSlot slot in barrelSlots)
            {
                slot.Card.Anim.PlayQuickRiffleSound();
                slot.Card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.02f);
            }
            yield return this.PostSuccessfulMoveSequence(oldSlot);
            yield return new WaitForSeconds(0.25f);
        }
        private bool CardCanBePassedThrough(PlayableCard card) => card != null && card.LacksAbility(Unyielding.ability);
        private CardSlot GetFurthestEmptySlot(bool movingLeft, CardSlot slotToCheck, List<CardSlot> possibleValidSlots = null)
        {
            possibleValidSlots ??= new();
            if (slotToCheck.Card == null)
            {
                possibleValidSlots.Add(slotToCheck);
            }
            else if (!CardCanBePassedThrough(slotToCheck.Card))
            {
                return possibleValidSlots.LastOrDefault();
            }

            CardSlot adjacent = slotToCheck.GetAdjacent(movingLeft);
            if (adjacent != null)
                return GetFurthestEmptySlot(movingLeft, adjacent, possibleValidSlots);

            return possibleValidSlots.LastOrDefault();
        }
    }
}
