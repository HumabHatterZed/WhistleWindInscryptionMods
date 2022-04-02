using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Assimilator()
        {
            const string rulebookName = "Assimilator";
            const string rulebookDescription = "When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.";
            const string dialogue = "From the many, one.";
            Assimilator.ability = WstlUtils.CreateAbility<Assimilator>(
                Resources.sigilAssimilator,
                rulebookName, rulebookDescription, dialogue, 4).Id;
        }
    }
    public class Assimilator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly CardModificationInfo mod = new(1, 1);

        private readonly string growDialogue = "With each body added, its appetite grows.";
        private readonly string dieDialogue = "Don't worry, bodies are in no short supply.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            base.Card.AddTemporaryMod(mod);
            base.Card.OnStatsChanged();
            base.Card.Anim.StrongNegationEffect();

            // checks if this card is one of MoSB's first two forms
            var cardName = base.Card.Info.name.ToLowerInvariant();
            if (cardName.Contains("mountainofbodies") && !cardName.Equals("wstl_mountainofbodies3"))
            {
                CardInfo evolution = CardLoader.GetCardByName("wstl_mountainOfBodies2");
                if (cardName.Equals("wstl_mountainofbodies2"))
                {
                    evolution = CardLoader.GetCardByName("wstl_mountainOfBodies3");
                }
                yield return new WaitForSeconds(0.25f);
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    evolution.Mods.Add(cardModificationInfo);
                }
                yield return base.Card.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);
                if (!PersistentValues.HasSeenMountainGrow)
                {
                    PersistentValues.HasSeenMountainGrow = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(growDialogue, -0.65f, 0.4f);
                }
            }

            yield return new WaitForSeconds(0.2f);
            yield return LearnAbility(0.4f);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            // checks if this card is one of MoSB's evolutions, excluding its first forme
            var cardName = base.Card.Info.name.ToLowerInvariant();
            if (cardName.Contains("mountainofbodies") && !cardName.Equals("wstl_mountainofbodies"))
            {
                yield return PreSuccessfulTriggerSequence();

                CardInfo previous = CardLoader.GetCardByName("wstl_mountainOfBodies");
                if (cardName.Equals("wstl_mountainofbodies3"))
                {
                    previous = CardLoader.GetCardByName("wstl_mountainOfBodies2");
                }
                yield return new WaitForSeconds(0.25f);
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    previous.Mods.Add(cardModificationInfo);
                }
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(previous, base.Card.Slot, 0.15f);
                yield return new WaitForSeconds(0.25f);
                if (!PersistentValues.HasSeenMountainShrink2)
                {
                    PersistentValues.HasSeenMountainShrink2 = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dieDialogue, -0.65f, 0.4f);
                }
            }
            else
            {
                yield break;
            }
        }
    }
}
