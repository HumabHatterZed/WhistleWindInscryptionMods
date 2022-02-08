using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_BroodMother()
        {
            const string rulebookName = "Brood Mother";
            const string rulebookDescription = "When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.";
            const string dialogue = "A small spider takes refuge in your hand.";
            return WstlUtils.CreateAbility<BroodMother>(
                Resources.sigilBroodMother,
                rulebookName, rulebookDescription, dialogue, 4, true);
        }
    }
    public class BroodMother : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return source;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();

            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_spiderling");
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.5f);
        }
    }
}
