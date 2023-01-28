using DiskCardGame;
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
            const string rulebookDescription = "Pay 3 bones to discard your current hand and draw cards equal to the number of cards discarded.";
            const string dialogue = "The unending faith of countless promises.";

            Alchemist.ability = AbilityHelper.CreateActivatedAbility<Alchemist>(
                Resources.sigilAlchemist, Resources.sigilAlchemist_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    public class Alchemist : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int BonesCost => 3;

        public override bool CanActivate()
        {
            if (Singleton<PlayerHand>.Instance.CardsInHand.Count() > 0)
            {
                if (Singleton<CardDrawPiles3D>.Instance.Deck.Cards.Count == 0)
                {
                    return Singleton<CardDrawPiles3D>.Instance.SideDeck.Cards.Count > 0;
                }
                return true;
            }
            return false;
        }

        // Discard all hands in card and draw an equal number
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);

            List<PlayableCard> cardsInHand = new(Singleton<PlayerHand>.Instance.CardsInHand);
            int count = 0;

            Singleton<PlayerHand>.Instance.CardsInHand.Clear();
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
                if (Singleton<CardDrawPiles3D>.Instance.Deck.Cards.Count > 0)
                {
                    Singleton<CardDrawPiles3D>.Instance.pile.Draw();
                    yield return Singleton<CardDrawPiles3D>.Instance.DrawCardFromDeck();
                }
                else if (Singleton<CardDrawPiles3D>.Instance.SideDeck.Cards.Count > 0)
                {
                    Singleton<CardDrawPiles3D>.Instance.sidePile.Draw();
                    yield return Singleton<CardDrawPiles3D>.Instance.DrawFromSidePile();
                }
                else
                {
                    yield return new WaitForSeconds(0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("You've exhausted your available cards.", -0.65f, 0.4f);
                    break;
                }
            }
            yield return base.LearnAbility(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
        }
    }
}
