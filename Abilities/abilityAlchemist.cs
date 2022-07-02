using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Alchemist()
        {
            const string rulebookName = "Alchemist";
            const string rulebookDescription = "Activate: Pay 3 bones to discard your current hand and draw cards equal to the number of cards discarded.";
            const string dialogue = "The unending faith of countless promises.";

            Alchemist.ability = WstlUtils.CreateActivatedAbility<Alchemist>(
                Resources.sigilTheTrain,
                rulebookName, rulebookDescription, dialogue, 2).Id;
        }
    }
    public class Alchemist : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int BonesCost => 0;

        public override bool CanActivate()
        {
            return Singleton<PlayerHand>.Instance.cardsInHand.Count() > 0;
        }

        // Discard all hands in card and draw an equal number
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);

            List<PlayableCard> cardsInHand = new(Singleton<PlayerHand>.Instance.cardsInHand);
            int count = 0;

            Singleton<PlayerHand>.Instance.cardsInHand.Clear();
            foreach (PlayableCard item in cardsInHand)
            {
                if (item != null)
                {
                    count++;
                    yield return Singleton<PlayerHand>.Instance.CleanUpCard(item);
                }
            }
            for (int i = 0; i < count; i++)
            {
                yield return Singleton<CardDrawPiles>.Instance.DrawCardFromDeck();
            }

            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
        }
    }
}
