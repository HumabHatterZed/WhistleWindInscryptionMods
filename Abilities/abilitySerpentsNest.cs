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
        private void Ability_SerpentsNest()
        {
            const string rulebookName = "Serpent's Nest";
            const string rulebookDescription = "When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.";
            const string dialogue = "The infection spreads.";

            SerpentsNest.ability = AbilityHelper.CreateAbility<SerpentsNest>(
                Resources.sigilSerpentsNest,// Resources.sigilSerpentsNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                addModular: true).Id;
        }
    }
    public class SerpentsNest : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
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

            if (base.Card.Slot.IsPlayerSlot)
            {
                if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                {
                    yield return new WaitForSeconds(0.2f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                    yield return new WaitForSeconds(0.2f);
                }
                CardInfo cardInfo = CardLoader.GetCardByName("wstl_theNakedWorm");
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);

                yield return new WaitForSeconds(0.45f);
                yield return base.LearnAbility(0.5f);
            }
        }
    }
}
