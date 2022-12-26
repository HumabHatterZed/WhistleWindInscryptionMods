using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_SerpentsNest()
        {
            const string rulebookName = "Serpent's Nest";
            const string rulebookDescription = "When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.";
            const string dialogue = "The infection spreads.";

            SerpentsNest.ability = AbilityHelper.CreateAbility<SerpentsNest>(
                Resources.sigilSerpentsNest, Resources.sigilSerpentsNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                addModular: true, opponent: true, canStack: false, isPassive: false).Id;
        }
    }
    public class SerpentsNest : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override CardInfo CardToDraw
        {
            get
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_theNakedWorm");
                cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                return cardByName;
            }
        }
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
            {
                return source.Health > 0;
            }
            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();

            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return source.TakeDamage(1, base.Card);
            yield return new WaitForSeconds(0.4f);
            yield return base.QueueOrCreateDrawnCard();
            if (!base.Card.OpponentCard)
            {
                yield return new WaitForSeconds(0.45f);
            }
            yield return base.LearnAbility(0.5f);
        }
    }
}
