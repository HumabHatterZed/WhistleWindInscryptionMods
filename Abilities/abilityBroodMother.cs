using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_BroodMother()
        {
            const string rulebookName = "Broodmother";
            const string rulebookDescription = "When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.";
            const string dialogue = "A small spider takes refuge in your hand.";
            BroodMother.ability = AbilityHelper.CreateAbility<BroodMother>(
                Resources.sigilBroodMother,
                rulebookName, rulebookDescription, dialogue, 4,
                addModular: true).Id;
        }
    }
    public class BroodMother : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return true;
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
