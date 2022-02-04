using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_QueenNest()
        {
            const string rulebookName = "Queen Nest";
            const string rulebookDescription = "When a card bearing this sigil is played, create a Worker Bee in your hand. Create an additional Worker Bee whenever another card dies.";
            const string dialogue = "Awfully fleshy for a bee.";
            return WstlUtils.CreateAbility<QueenNest>(
                Resources.sigilIdol,
                rulebookName, rulebookDescription, dialogue, 3, true);
        }
    }
    public class QueenNest : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }

        public override IEnumerator OnResolveOnBoard()
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

            CardInfo cardInfo = CardLoader.GetCardByName("wstl_workerBee");
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.5f);
        }
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && base.Card.Slot.IsPlayerSlot && base.Card != null;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_workerBee");
            if (card.name != "Card (Worker Bee)" && card != base.Card && card != null && base.Card != null)
            {
                yield return PreSuccessfulTriggerSequence();

                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                {
                    yield return new WaitForSeconds(0.2f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);

                yield return new WaitForSeconds(0.4f);
                yield return LearnAbility(0.4f);
            }
        }
    }
}
