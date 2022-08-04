using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Test()
        {
            const string rulebookName = "Test";
            const string rulebookDescription = "Test ability";
            const string dialogue = "femboy";
            Test.ability = AbilityHelper.CreateAbility<Test>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: true).Id;
        }
    }
    public class Test : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public const string ModSingletonId = "Maternal_Submerge";

        private static readonly CardModificationInfo SubmergeMod = new(Ability.Submerge);

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            // this just tells the game that this ability is triggering
            // it also does a null check on the base card, so that's neat
            yield return base.PreSuccessfulTriggerSequence();

            // got rid of the IEnumerator AddSubmergeToCards since it was only being used here
            foreach(CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where((CardSlot s) => s.Card != null))
            {
                if (IsValidCard(slot.Card))
                {
                    yield return AddSubmerge(slot.Card);
                }
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return IsValidCard(otherCard);
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();

            // check adjacent slots for otherCard
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where((CardSlot s) => s.Card != null))
            {
                if (slot.Card == otherCard)
                {
                    yield return AddSubmerge(otherCard);
                }
            }
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            // only respond if otherCard has the SubmergeMod
            return otherCard != null && otherCard.OpponentCard == base.Card.OpponentCard && otherCard.TemporaryMods.Contains(SubmergeMod);
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            // check otherCard slots
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(otherCard.Slot).Where((CardSlot s) => s.Card != null))
            {
                // if it has Maternal, break and keep mod
                if (slot.Card.HasAbility(ability))
                {
                    yield break;
                }
            }
            yield return this.RevertSubmerge(otherCard);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            // for each adjacent card that isn't null
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where((CardSlot s) => s.Card != null))
            {
                bool remove = true;
                // for each adjacent card that isn't this card (1 slot)
                foreach (CardSlot slot2 in Singleton<BoardManager>.Instance.GetAdjacentSlots(slot).Where((CardSlot s) => s.Card != this.Card))
                {
                    // if not null and has Maternal, remove = false
                    if (slot2.Card != null && slot2.Card.HasAbility(ability))
                    {
                        remove = false;
                    }
                }
                // If no other adjacent Maternal card, remove Submerge
                if (remove)
                {
                    yield return RevertSubmerge(slot.Card);
                }
            }
        }

        private bool IsValidCard(PlayableCard card)
        {
            // putting these checks into a separate bool so the code looks nicer, could all be put into one line if you want
            bool isValid = !card.HasAbility(Ability.Submerge) && !card.TemporaryMods.Contains(SubmergeMod) && !card.HasAbility(ability);

            // not null, same side of the board, does not have Submerge, Maternal, or SubmergeMod
            return card != null && card.OpponentCard == base.Card.OpponentCard && isValid;
        }

        private IEnumerator AddSubmerge(PlayableCard playableCard)
        {
            playableCard.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.25f);
            //playableCard.RemoveTemporaryMod(Test.NegateSubmergeMod);
            playableCard.AddTemporaryMod(SubmergeMod);
        }
        private IEnumerator RevertSubmerge(PlayableCard playableCard)
        {
            playableCard.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.25f);
            //playableCard.AddTemporaryMod(Test.NegateSubmergeMod);
            playableCard.RemoveTemporaryMod(SubmergeMod);
        }
    }
}
