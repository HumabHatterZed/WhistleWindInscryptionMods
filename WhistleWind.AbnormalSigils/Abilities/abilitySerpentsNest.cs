using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_SerpentsNest()
        {
            const string rulebookName = "Serpent's Nest";
            const string rulebookDescription = "When [creature] is struck, the striker is dealt 1 damage and a Worm is created in your hand. [define:wstl_theNakedWorm]";
            const string dialogue = "The infection spreads.";

            SerpentsNest.ability = AbnormalAbilityHelper.CreateAbility<SerpentsNest>(
                Artwork.sigilSerpentsNest, Artwork.sigilSerpentsNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: true, opponent: true, canStack: false).Id;
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
                return source.Health > 0;

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
                yield return new WaitForSeconds(0.45f);

            yield return base.LearnAbility(0.5f);
        }
    }
}
