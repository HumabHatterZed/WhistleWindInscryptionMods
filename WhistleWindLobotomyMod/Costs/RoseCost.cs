using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.CardCosts;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    /// <summary>
    /// When this card is played, it will present 3 cards in your deck you must choose from. The selected card will gain Chosen by the Rose.
    /// </summary>
    public class RoseCost : CustomCardCost {
        public const string STAINING_ROSE_COST = "StainingRoseCost";
        public const int MAX_STACK = 3;
        private static Texture2D roseTex = null;
        internal static void Init() {
            CardCostManager.Register(LobotomyPlugin.pluginGuid, STAINING_ROSE_COST, typeof(RoseCost), GetCostTexture, null);
        }

        public static Texture2D GetCostTexture(int amt, CardInfo info, PlayableCard card) {
            roseTex ??= TextureLoader.LoadTextureFromFile("costRose.png");
            return roseTex;
        }
        public override string CostName => STAINING_ROSE_COST;
        public override bool CostSatisfied(int cardCost, PlayableCard playableCard) {
            CardInfo roseChosenCard = null;
            List<CardInfo> playerDeck = new(SaveManager.SaveFile.CurrentDeck.Cards);
            roseChosenCard = playerDeck.Find(x => x.HasAbility(RoseChosen.ID));
            if (roseChosenCard != null) {
                // if the rose has already chosen a card
                LobotomyPlugin.Log.LogDebug("Has chosen already");
                return true;
            }
            else {
                // otherwise at least one card must be chooseable by the rose
                LobotomyPlugin.Log.LogDebug("Check can choose");
                return playerDeck.Exists(RoseChosen.CanBeRoseChosen);
            }
        }
        public override IEnumerator OnPlayed(int cardCost, PlayableCard playableCard) {
            yield return ResonateSequence();
        }
        public IEnumerator ResonateSequence() {
            CardInfo roseChosenCard = null;
            List<CardInfo> playerDeck = new(SaveManager.SaveFile.CurrentDeck.Cards);
            SelectableCard selectedCard = null;
            bool hasAlreadyChosen = false;
            roseChosenCard = playerDeck.Find(x => x.HasAbility(RoseChosen.ID));
            // if we haven't already chosen a card, get a selection of cards to select from
            if (roseChosenCard == null) {
                int removeIdx;
                playerDeck.RemoveAll(x => !RoseChosen.CanBeRoseChosen(x));
                removeIdx = Mathf.Min(3, playerDeck.Count / 2);

                // sort by powerlevel (high -> low)
                playerDeck.Sort((CardInfo a, CardInfo b) => b.PowerLevel - a.PowerLevel);
                playerDeck.RemoveRange(removeIdx, playerDeck.Count - removeIdx); // show up to 3 options
            }
            else {
                playerDeck = new() { roseChosenCard };
                hasAlreadyChosen = true;
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.DeckSelection, immediate: false, lockAfter: true);
            if (hasAlreadyChosen) {
                TextDisplayer.Instance.ShowMessage("Resonate with Staining Rose.");
            }
            else {
                TextDisplayer.Instance.ShowMessage("Choose a creature to resonate with Staining Rose.");
            }

            // SelectCardFrom uses the passed-in List<CardInfo> to determine how many card objects to readd to the pile
            // this is because it assumes the length of the list is equal to the num of cards in the deck (Tutor)
            // since we aren't doing that here, we need to keep track of that count ourselves so we can re-set the pile's cards correctly
            int cardsInPile = CardDrawPiles3D.Instance.Pile.cards.Count;
            yield return Singleton<BoardManager>.Instance.CardSelector.SelectCardFrom(playerDeck, CardDrawPiles3D.Instance.Pile, delegate (SelectableCard x) {
                selectedCard = x;
                roseChosenCard = x.Info;

                if (roseChosenCard.GetAbilityStacks(RoseChosen.ID) >= MAX_STACK) {
                    selectedCard.Anim.PlayDeathAnimation();
                    SaveManager.SaveFile.CurrentDeck.RemoveCard(roseChosenCard);
                }
                else {
                    selectedCard.Anim.PlayTransformAnimation();
                    SaveManager.SaveFile.CurrentDeck.ModifyCard(roseChosenCard, new CardModificationInfo(RoseChosen.ID) { nonCopyable = true });
                    selectedCard.RenderCard();
                    RenderChosenCardInField(roseChosenCard);
                }

                TextDisplayer.Instance.Clear();
            });
            base.StartCoroutine(CardDrawPiles3D.Instance.Pile.SpawnCards(cardsInPile));

            yield return new WaitForSeconds(0.5f);


            Tween.Position(selectedCard.transform, selectedCard.transform.position + Vector3.back * 4f, 0.1f, 0f, Tween.EaseIn);
            UnityEngine.Object.Destroy(selectedCard.gameObject, 0.1f);

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            // if we removed the chosen card, remove it from the battle
            if (!SaveManager.SaveFile.CurrentDeck.Cards.Contains(roseChosenCard)) {
                yield return KillChosenCardInField(roseChosenCard);
            }
            ViewManager.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private IEnumerator KillChosenCardInField(CardInfo info) {
            List<PlayableCard> cardsInPlay = BoardManager.Instance.GetPlayerCards();
            cardsInPlay.AddRange(PlayerHand.Instance.CardsInHand);

            if (cardsInPlay.Exists(x => x.Info == info)) {
                LobotomyPlugin.Log.LogInfo("Card in hoof/on board");
                PlayableCard card = cardsInPlay.Find(x => x.Info == info);
                if (card.OnBoard) {
                    yield return card.Die(true);
                }
                else {
                    PlayerHand.Instance.RemoveCardFromHand(card);
                    card.gameObject.SetActive(false);
                    CustomCoroutine.Instance.StartCoroutine(card.DestroyWhenStackIsClear());
                }
            }
            else if (CardDrawPiles3D.Instance.Deck.Cards.Contains(info)) {
                CardDrawPiles3D.Instance.Deck.Cards.Remove(info);
                Transform cardObj = CardDrawPiles3D.Instance.Pile.cards[CardDrawPiles3D.Instance.Pile.cards.Count - 1];
                CardDrawPiles3D.Instance.Pile.cards.Remove(cardObj);
                Destroy(cardObj);
            }
        }

        private void RenderChosenCardInField(CardInfo info) {
            List<PlayableCard> cardsInPlay = BoardManager.Instance.GetPlayerCards();
            cardsInPlay.AddRange(PlayerHand.Instance.CardsInHand);

            if (cardsInPlay.Exists(x => x.Info == info)) {
                LobotomyPlugin.Log.LogInfo("Card in hoof/on board");
                cardsInPlay.Find(x => x.Info == info).RenderCard();
            }
        }

        public override string CostUnsatisfiedHint(int cardCost, PlayableCard playableCard) {
            return UnityEngine.Random.Range(0, 3) switch {
                0 => $"{playableCard.Info.DisplayedNameLocalized} desires a suitably strong creature to resonate with.",
                1 => "You have no creatures in your caravan that meet the right condition.",
                _ => $"None of your creatures meet the condition for resonance."
            };
        }
    }
}
