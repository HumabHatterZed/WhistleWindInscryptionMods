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
            const string rulebookDescription = "When a card dies while a card bearing this sigil is on the board, a Worker Bee is created in your hand. A Worker Bee is defined as: 1 Power, 1 Health.";
            const string dialogue = "For the hive.";
            QueenNest.ability = AbilityHelper.CreateAbility<QueenNest>(
                Resources.sigilQueenNest, Resources.sigilQueenNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true).Id;
        }
    }
    public class QueenNest : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return base.Card.OnBoard && !base.Card.OpponentCard && card != base.Card && killer != null;
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
