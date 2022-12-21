using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Alchemist()
        {
            const string rulebookName = "Alchemist";
            const string rulebookDescription = "Pay 3 Bones to discard your current hand and draw cards equal to the amount discarded.";
            const string dialogue = "The unending faith of countless promises.";

            Alchemist.ability = AbnormalAbilityHelper.CreateActivatedAbility<Alchemist>(
                Artwork.sigilAlchemist, Artwork.sigilAlchemist_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    public class Alchemist : BetterActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingBonesCost => 3;

        public override bool CanActivate()
        {
            if (Singleton<PlayerHand>.Instance.CardsInHand.Count() > 0)
            {
                if (Singleton<CardDrawPiles3D>.Instance.Deck.Cards.Count == 0)
                    return Singleton<CardDrawPiles3D>.Instance.SideDeck.Cards.Count > 0;

                return true;
            }
            return false;
        }

        // Discard all hands in card and draw an equal number
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Hand);

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
                    yield return HelperMethods.PlayAlternateDialogue(dialogue: "You've exhausted all of your cards.");
                    break;
                }
            }
            yield return base.LearnAbility(0.2f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }
    }
}
