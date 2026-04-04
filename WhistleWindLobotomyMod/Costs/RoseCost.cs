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
            List<CardInfo> playerDeck = new(RunState.Run.playerDeck.Cards);
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
            CardInfo roseChosenCard = null;
            List<CardInfo> playerDeck = new(RunState.Run.playerDeck.Cards);
            SelectableCard selectedCard = null;

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
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.DeckSelection, immediate: false, lockAfter: true);
            TextDisplayer.Instance.ShowMessage("Choose a creature to resonate with the Rose.");
            
            yield return Singleton<BoardManager>.Instance.CardSelector.SelectCardFrom(playerDeck, (Singleton<CardDrawPiles>.Instance as CardDrawPiles3D).Pile, delegate (SelectableCard x) {
                selectedCard = x;
                roseChosenCard = x.Info;

                if (roseChosenCard.GetAbilityStacks(RoseChosen.ID) > 2) {
                    selectedCard.Anim.PlayDeathAnimation();
                    RunState.Run.playerDeck.RemoveCard(roseChosenCard);
                }
                else {
                    selectedCard.Anim.PlayTransformAnimation();
                    RunState.Run.playerDeck.ModifyCard(roseChosenCard, new CardModificationInfo(RoseChosen.ID) { nonCopyable = true });
                    selectedCard.RenderCard();
                }
                
                TextDisplayer.Instance.Clear();
            });
            
            yield return new WaitForSeconds(0.5f);


            Tween.Position(selectedCard.transform, selectedCard.transform.position + Vector3.back * 4f, 0.1f, 0f, Tween.EaseIn);
            UnityEngine.Object.Destroy(selectedCard.gameObject, 0.1f);

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            ViewManager.Instance.Controller.LockState = ViewLockState.Unlocked;
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
