using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public abstract class OpponentDrawCreatedCard : DrawCreatedCard
    {
        protected IEnumerator QueueOrCreateDrawnCard()
        {
            // Queue card if opponent, otherwise draw card to hand
            if (base.Card.OpponentCard)
            {
	    	yield return BaseMethods.QueueCreatedCard(CardToDraw);
	    }
            else
            {
                yield return base.CreateDrawnCard();
            }
        }
    }
}
