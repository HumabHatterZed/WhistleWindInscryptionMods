using DiskCardGame;
using System.Collections;

namespace WhistleWindLobotomyMod
{
    public abstract class OpponentDrawCreatedCard : DrawCreatedCard
    {
        protected IEnumerator QueueOrCreateDrawnCard()
        {
            // Queue card if opponent, otherwise draw card to hand
            if (base.Card.OpponentCard)
            {
                yield return CustomMethods.QueueCreatedCard(CardToDraw);
            }
            else
            {
                yield return base.CreateDrawnCard();
            }
        }
    }
}
