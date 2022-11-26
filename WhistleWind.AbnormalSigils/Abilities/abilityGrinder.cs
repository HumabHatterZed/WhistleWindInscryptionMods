using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Grinder()
        {
            const string rulebookName = "Grinder";
            const string rulebookDescription = "Pay 1 Health and gain an Energy Cell.\n[creature] gains the stats of the cards sacrificed to play it.";
            const string dialogue = "Now everything will be just fine.";
            Grinder.ability = AbnormalAbilityHelper.CreateActivatedAbility<Grinder>(
                Artwork.sigilGrinder, Artwork.sigilGrinder_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    public class Grinder : BetterActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingHealthCost => 1;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return !fromCombat && Singleton<BoardManager>.Instance.currentSacrificeDemandingCard == base.Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            base.Card.AddTemporaryMod(new(card.Attack, card.Health));
            base.Card.OnStatsChanged();
            yield return new WaitForSeconds(0.25f);
            yield return base.LearnAbility(0.4f);
        }

        public override IEnumerator Activate()
        {
            View view = Singleton<ViewManager>.Instance.CurrentView;
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Default);
            yield return Singleton<ResourcesManager>.Instance.AddMaxEnergy(1);
            yield return Singleton<ResourcesManager>.Instance.AddEnergy(1);
            yield return base.LearnAbility(0.2f);
            yield return HelperMethods.ChangeCurrentView(view);
        }
    }
}
