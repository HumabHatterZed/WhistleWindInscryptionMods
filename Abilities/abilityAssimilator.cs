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
            Assimilator.ability = AbilityHelper.CreateAbility<Assimilator>(
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
            return killer == base.Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            base.Card.AddTemporaryMod(mod);
            
            if (!base.Card.Dead)
            {
                base.Card.Anim.StrongNegationEffect();
                yield return LearnAbility(0.4f);

                // checks if this card is one of MoSB's first two formes
                var cardName = base.Card.Info.name;
                if (cardName.Equals("wstl_mountainOfBodies") || cardName.Equals("wstl_mountainOfBodies2"))
                {
                    CardInfo evolution = cardName.Equals("wstl_mountainOfBodies") ? CardLoader.GetCardByName("wstl_mountainOfBodies2") : CardLoader.GetCardByName("wstl_mountainOfBodies3");
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
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            var cardName = base.Card.Info.name;
            return !wasSacrifice && (cardName.Equals("wstl_mountainOfBodies3") || cardName.Equals("wstl_mountainOfBodies2"));
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();

            var cardName = base.Card.Info.name;
            CardInfo previous = cardName.Equals("wstl_mountainOfBodies2") ? CardLoader.GetCardByName("wstl_mountainOfBodies") : CardLoader.GetCardByName("wstl_mountainOfBodies2");
            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                previous.Mods.Add(cardModificationInfo);
            }
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(previous, base.Card.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            if (!PersistentValues.HasSeenMountainShrink)
            {
                PersistentValues.HasSeenMountainShrink = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dieDialogue, -0.65f, 0.4f);
            }
        }
    }
}