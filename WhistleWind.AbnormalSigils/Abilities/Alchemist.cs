using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Alchemist()
        {
            const string rulebookName = "Alchemist";
            const string rulebookDescription = "Pay 2 Energy to discard your current hand and draw cards equal to the amount discarded. If the main pile is exhausted, draw from the side pile.";
            const string dialogue = "The unending faith of countless promises.";
            const string triggerText = "[creature] replaces your hand with a new one!";
            Alchemist.ability = AbnormalAbilityHelper.CreateActivatedAbility<Alchemist>(
                "sigilAlchemist",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Alchemist : ExtendedActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingEnergyCost => 2;

        public override bool CanActivate()
        {
            if (PlayerHand.Instance.CardsInHand.Count > 0)
            {
                if (CardDrawPiles.Instance.Deck.CardsInDeck > 0)
                    return true;

                if (!SaveManager.SaveFile.IsPart2)
                    return CardDrawPiles3D.Instance.SideDeck.CardsInDeck > 0;
            }
            return false;
        }

        // Discard all hands in card and draw an equal number
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Hand);

            int cardsToDraw = PlayerHand.Instance.CardsInHand.Count;
            yield return PlayerHand.Instance.CleanUp();

            for (int i = 0; i < cardsToDraw; i++)
            {
                if (CardDrawPiles.Instance.Deck.CardsInDeck > 0)
                {
                    if (!SaveManager.SaveFile.IsPart2)
                        CardDrawPiles3D.Instance.Pile.Draw();
                    
                    CardDrawPiles.Instance.DrawCardFromDeck();
                }
                else if (!SaveManager.SaveFile.IsPart2 && CardDrawPiles3D.Instance.SideDeck.CardsInDeck > 0)
                {
                    CardDrawPiles3D.Instance.SidePile.Draw();
                    CardDrawPiles3D.Instance.DrawFromSidePile();
                }
                else
                {
                    break;
                }
            }

            yield return base.LearnAbility(0.2f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }
    }
}
