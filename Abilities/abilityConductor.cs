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
        private NewAbility Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.";
            const string dialogue = "From break and ruin, the most beautiful performance begins.";

            return WstlUtils.CreateAbility<Conductor>(
                Resources.sigilConductor,
                rulebookName, rulebookDescription, dialogue, 4);
        }
    }
    public class Conductor : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public int count = 0;

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
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_silentEnsemble");
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);

            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.5f);
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return true;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (!playerTurnEnd)
            {
                if (base.Card.Slot.IsPlayerSlot && count < 2) // If base Card is player-owned and ability has activated thrice (including initial play)
                {
                    yield return base.PreSuccessfulTriggerSequence();

                    CardInfo cardInfo = CardLoader.GetCardByName("wstl_silentEnsemble");

                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);

                    if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                    {
                        yield return new WaitForSeconds(0.2f);
                        Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                        yield return new WaitForSeconds(0.2f);
                    }

                    count++;
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
                    yield return new WaitForSeconds(0.25f);
                }
            }
            yield break;
        }
    }
}
