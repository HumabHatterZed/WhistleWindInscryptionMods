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
        private void Ability_QueenNest()
        {
            const string rulebookName = "Queen Nest";
            const string rulebookDescription = "When a card bearing this sigil is played, create a Worker Bee in your hand. Create an additional Worker Bee whenever another card dies.";
            const string dialogue = "For the hive.";
            QueenNest.ability = WstlUtils.CreateAbility<QueenNest>(
                Resources.sigilQueenNest,
                rulebookName, rulebookDescription, dialogue, 3).Id;
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

            CardInfo cardInfo = CardLoader.GetCardByName("wstl_queenBeeWorker");
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.5f);
        }
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && base.Card.Slot.IsPlayerSlot && killer != null;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_queenBeeWorker");
            if (card != null)
            {
                if (!card.Info.name.ToLowerInvariant().Contains("queenbeeworker") && card != base.Card)
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
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo);

                    yield return new WaitForSeconds(0.4f);
                    yield return LearnAbility(0.4f);
                }
            }
        }
    }
}
