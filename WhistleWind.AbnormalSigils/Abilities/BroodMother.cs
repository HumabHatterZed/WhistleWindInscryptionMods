using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_BroodMother()
        {
            const string rulebookName = "Broodmother";
            const string rulebookDescription = "When [creature] is struck, create a Spiderling in your hand. [define:wstl_spiderling]";
            const string dialogue = "A small spider takes refuge in your hand.";
            const string triggerText = "[creature] drops a spiderling!";
            BroodMother.ability = AbnormalAbilityHelper.CreateAbility<BroodMother>(
                "sigilBroodMother",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: true, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class BroodMother : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override CardInfo CardToDraw
        {
            get
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_spiderling");
                cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                return cardByName;
            }
        }
        public override bool RespondsToTakeDamage(PlayableCard source) => true;
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.QueueOrCreateDrawnCard();
            yield return base.LearnAbility(0.5f);
        }
    }
}
