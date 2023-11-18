using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class RefreshDecks : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();

        public override IEnumerator OnResolveOnBoard()
        {
            List<CardInfo> deck = new();
            CardDrawPiles3D.Instance.DeckData.ForEach((CardInfo x) => deck.Add(x.Clone() as CardInfo));
            List<CardInfo> sideDeck = new();
            CardDrawPiles3D.Instance.SideDeckData.ForEach((CardInfo x) => deck.Add(x.Clone() as CardInfo));

            yield return HelperMethods.ChangeCurrentView(View.CardPiles, 0.2f, 0.4f);

            Singleton<CardDrawPiles>.Instance.CleanUp();

            // remove any remaining cards then create new piles

            yield return new WaitForSeconds(0.4f);
            yield return Singleton<CardDrawPiles>.Instance.Initialize();

            /*yield return CardDrawPiles3D.Instance.pile.SpawnCards(CardDrawPiles3D.Instance.Deck.CardsInDeck, 0.5f);
            yield return new WaitForSeconds(0.4f);
            yield return CardDrawPiles3D.Instance.sidePile.SpawnCards(CardDrawPiles3D.Instance.SideDeck.CardsInDeck, 0.5f);
*/
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_RefreshDecks()
        {
            const string rulebookName = "Refresh Decks";
            RefreshDecks.ability = LobotomyAbilityHelper.CreateAbility<RefreshDecks>(
                "sigilRefreshDecks", rulebookName,
                "Refreshes and reshuffles both draw piles.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
