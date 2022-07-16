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
        private void Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.";
            const string dialogue = "From break and ruin, the most beautiful performance begins.";

            Conductor.ability = AbilityHelper.CreateAbility<Conductor>(
                Resources.sigilConductor,
                rulebookName, rulebookDescription, dialogue, 4).Id;
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

        public override bool RespondsToUpkeep(bool onPlayerUpkeep)
        {
            if (!base.Card.Slot.IsPlayerSlot)
            {
                return false;
            }
            return count < 2;
        }
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep)
        {
            count++;
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

            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
