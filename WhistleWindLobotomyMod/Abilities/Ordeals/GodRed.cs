using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using Microsoft.Win32.SafeHandles;
using Pixelplacement;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodRed() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God Red";
            info.rulebookDescription = "Activate: Crush one of the player's draw piles with the Red Hand. At half Health (once): Crush both of the player's draw piles with the Red Hand.";
            info.powerLevel = 5;

            GodRed.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodRed), TextureLoader.LoadTextureFromFile("sigilGodRed.png"))
                .SetUniqueRedirect("Red Hand", "wstl:Ordeals_Red Hand", GameColors.Instance.glowRed)
                .Id;
        }
    }

    public class GodRed : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private Animator handMainDeck;
        private Animator handSideDeck;

        private bool attackMainDeck = false;

        protected override IEnumerator PreActivate(bool halfHealth) {
            ViewManager.Instance.SwitchToView(View.CardPiles);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound2D("Violet_portal_on", MixerGroup.TableObjectsSFX);
            if (halfHealth) {
                handMainDeck.SetTrigger("Show");
                handSideDeck.SetTrigger("Show");
                handMainDeck.SetLayerWeight(1, 1f);
                handSideDeck.SetLayerWeight(1, 1f);
            }
            else {
                attackMainDeck = AttackMainDeck();
                if (attackMainDeck) {
                    handMainDeck.SetTrigger("Show");
                    handMainDeck.SetLayerWeight(1, 1f);
                }
                else {
                    handSideDeck.SetTrigger("Show");
                    handSideDeck.SetLayerWeight(1, 1f);
                }
            }

        }
        protected override IEnumerator Activate(bool halfHealth) {
            int numToRemove = 2 + Mathf.Min(2, RunState.CurrentRegionTier + Mathf.Max(0, RunState.Run.DifficultyModifier - 1));

            ViewManager.Instance.SwitchToView(View.CardPiles);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound2D("Violet_attack", MixerGroup.TableObjectsSFX);

            if (halfHealth) {
                handMainDeck.SetTrigger("Extend");
                handSideDeck.SetTrigger("Extend");

                yield return new WaitForSeconds(0.5f);

                base.StartCoroutine(AttackMainDeck(numToRemove));
                yield return AttackSideDeck(numToRemove);

                yield return new WaitForSeconds(1f);
                handMainDeck.SetTrigger("Hide");
                handSideDeck.SetTrigger("Hide");
            }
            else if (attackMainDeck) {
                handMainDeck.SetTrigger("Extend");
                yield return new WaitForSeconds(0.5f);
                yield return AttackMainDeck(numToRemove);
                yield return new WaitForSeconds(1f);
                handMainDeck.SetTrigger("Hide");
            }
            else {
                handSideDeck.SetTrigger("Extend");
                yield return new WaitForSeconds(0.5f);
                yield return AttackSideDeck(numToRemove);
                yield return new WaitForSeconds(1f);
                handSideDeck.SetTrigger("Hide");
            }
            handMainDeck.SetLayerWeight(1, 0f);
            handSideDeck.SetLayerWeight(1, 0f);
            AudioController.Instance.PlaySound2D("Violet_portal_off", MixerGroup.TableObjectsSFX);
        }

        private IEnumerator AttackMainDeck(int cardsToDestroy) {
            if (CardDrawPiles3D.Instance.Deck.CardsInDeck == 0) {
                yield break;
            }
            yield return new WaitUntil(() => !CardDrawPiles3D.Instance.Pile.DoingCardOperation);
            CardDrawPiles3D.Instance.Pile.DoingCardOperation = true;
            AudioController.Instance.PlaySound3D("card_death", MixerGroup.TableObjectsSFX, CardDrawPiles3D.Instance.Pile.transform.position, 1f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.VerySmall), new AudioParams.Repetition(0.05f));
            for (int i = 0; i < cardsToDestroy; i++) {
                if (CardDrawPiles3D.Instance.Deck.CardsInDeck == 0) {
                    yield break;
                }
                Transform topCard = CardDrawPiles3D.Instance.Pile.cards[CardDrawPiles3D.Instance.Pile.NumCards - 1];
                CardDrawPiles3D.Instance.Pile.cards.Remove(topCard);
                Tween.LocalScale(topCard, new Vector3(1.5f, 0f, 1.5f), 0.1f, 0f, completeCallback: delegate {
                    Destroy(topCard.gameObject);
                });

                CardDrawPiles3D.Instance.Deck.Draw(null); // mull the actual deck of CardInfos

                // disable the pile's shadow if we've removed the last card from it
                if (CardDrawPiles3D.Instance.Pile.cards.IndexOf(topCard) == CardDrawPiles3D.Instance.Pile.cards.Count - 1) {
                    CardDrawPiles3D.Instance.Pile.shadow.SetActive(false);
                }
            }

            CardDrawPiles3D.Instance.Pile.DoingCardOperation = false;
        }
        private IEnumerator AttackSideDeck(int cardsToDestroy) {
            if (CardDrawPiles3D.Instance.SideDeck.CardsInDeck == 0) {
                yield break;
            }
            yield return new WaitUntil(() => !CardDrawPiles3D.Instance.SidePile.DoingCardOperation);
            CardDrawPiles3D.Instance.SidePile.DoingCardOperation = true;
            AudioController.Instance.PlaySound3D("card_death", MixerGroup.TableObjectsSFX, CardDrawPiles3D.Instance.SidePile.transform.position, 1f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.VerySmall), new AudioParams.Repetition(0.05f));
            for (int i = 0; i < cardsToDestroy; i++) {
                if (CardDrawPiles3D.Instance.SideDeck.CardsInDeck == 0) {
                    yield break;
                }
                Transform topCard = CardDrawPiles3D.Instance.SidePile.cards[CardDrawPiles3D.Instance.SidePile.NumCards - 1];
                CardDrawPiles3D.Instance.SidePile.cards.Remove(topCard);
                Tween.LocalScale(topCard, new Vector3(1.5f, 0f, 1.5f), 0.1f, 0f, completeCallback: delegate {
                    Destroy(topCard.gameObject);
                });

                CardDrawPiles3D.Instance.SideDeck.Draw(null); // mull the actual deck of CardInfos

                // disable the pile's shadow if we've removed the last card from it
                if (CardDrawPiles3D.Instance.SidePile.cards.IndexOf(topCard) == CardDrawPiles3D.Instance.SidePile.cards.Count - 1) {
                    CardDrawPiles3D.Instance.SidePile.shadow.SetActive(false);
                }
            }

            CardDrawPiles3D.Instance.SidePile.DoingCardOperation = false;
        }

        public static int RedHoofCards() {
            return 2 + Mathf.Min(2, RunState.CurrentRegionTier + Mathf.Max(0, RunState.Run.DifficultyModifier - 1));
        }
        private bool AttackMainDeck() {
            // if both decks can be drawn from or we have exhausted both decks,
            // choose randomly
            if (CardDrawPiles3D.Instance.Deck.CardsInDeck > 0 && CardDrawPiles3D.Instance.SideDeck.CardsInDeck > 0 ||
                (CardDrawPiles3D.Instance.Deck.CardsInDeck == 0 && CardDrawPiles3D.Instance.SideDeck.CardsInDeck == 0)) {
                return SeededRandom.Bool(base.GetRandomSeed() + TurnManager.Instance.TurnNumber);
            }

            // if only one deck can be drawn from, return which one can be drawn from
            return CardDrawPiles3D.Instance.Deck.CardsInDeck > 0;
        }

        public override void SetUpVisualGameObject() {
            activateVisualGameObject = new("RedHand_pool");
            GameObject obj = Instantiate(LobOpponentUtils.ShrineBossRedPrefab, activateVisualGameObject.transform);
            obj.transform.position = CardDrawPiles3D.Instance.Pile.transform.position + 3 * Vector3.up + 3 * Vector3.forward;
            handMainDeck = obj.GetComponent<Animator>();
            
            GameObject obj2 = Instantiate(LobOpponentUtils.ShrineBossRedPrefab, activateVisualGameObject.transform);
            obj2.transform.position = CardDrawPiles3D.Instance.SidePile.transform.position + 3 * Vector3.up + 3 * Vector3.forward;
            handSideDeck = obj2.GetComponent<Animator>();
        }

        protected override IEnumerator CleanUpVisuals() {
            if (preActivated) {
                handMainDeck.Play("Hide");
                handSideDeck.Play("Hide");
                handMainDeck.SetLayerWeight(1, 0f);
                handSideDeck.SetLayerWeight(1, 0f);
            }

            yield return new WaitForSeconds(0.5f);
            Destroy(activateVisualGameObject);
        }
    }
}
