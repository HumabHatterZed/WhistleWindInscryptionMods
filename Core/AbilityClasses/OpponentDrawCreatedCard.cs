using DiskCardGame;
using System.Collections;
using WhistleWind.Core.Helpers;

namespace WhistleWind.Core.AbilityClasses
{
    public abstract class OpponentDrawCreatedCard : DrawCreatedCard
    {
        protected IEnumerator QueueOrCreateDrawnCard()
        {
            // Queue card if opponent, otherwise draw card to hand
            if (base.Card.OpponentCard)
                yield return HelperMethods.QueueCreatedCard(CardToDraw);
            else
                yield return base.CreateDrawnCard();
        }
    }
}
