using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.RuleBook;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_FalseThrone() {
            const string rulebookName = "Magic Trick";
            const string rulebookDescription = "Once per battle: Remove cards equal to the number of cards in your hand from both draw piles. All cards in your hand gain the Neutered sigil and become free to play.";
            const string dialogue = "A simple little magic trick to deceive the people.";
            const string triggerText = "[creature] gives a false present to the chosen creature.";
            FalseThrone.ability = AbnormalAbilityHelper.CreateActivatedAbility<FalseThrone>(
                "sigilFalseThrone",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4, special: true)
                .SetAbilityRedirect("Neutered", Neutered.ability, GameColors.Instance.gray)
                .Id;
        }
    }
    /// <summary>
    /// Once per battle: Remove cards equal to the number of cards in your hand from both draw piles. All cards in your hand gain the Neutered sigil and become free to play.
    /// </summary>
    public class FalseThrone : DelayedActivatedAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int TurnDelay => 9999;

        public override bool CanActivate() {
            return base.CanActivate() && PlayerHand.Instance.CardsInHand.Count > 0 && !Singleton<CardDrawPiles>.Instance.Exhausted;
        }
        public override bool CanActivateOpponent() => false;

        public override IEnumerator Activate() {
            bool hasSideDeck = Singleton<CardDrawPiles3D>.Instance != null;
            
            ViewManager.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.2f);
            foreach (PlayableCard card in PlayerHand.Instance.CardsInHand) {
                card.AddTemporaryMod(new(Neutered.ability) {
                    bloodCostAdjustment = -999,
                    bonesCostAdjustment = -999,
                    energyCostAdjustment = -999,
                    nullifyGemsCost = true
                });
                card.Anim.StrongNegationEffect();

                if (hasSideDeck) {
                    if (Singleton<CardDrawPiles>.Instance.Deck.CardsInDeck > 0) {
                        Singleton<CardDrawPiles3D>.Instance.Pile.Draw();
                        Singleton<CardDrawPiles>.Instance.Deck.Draw();
                    }
                    if (Singleton<CardDrawPiles3D>.Instance.SideDeck.CardsInDeck > 0) {
                        Singleton<CardDrawPiles3D>.Instance.SidePile.Draw();
                        Singleton<CardDrawPiles3D>.Instance.SideDeck.Draw();
                    }
                }
                else if (Singleton<CardDrawPiles>.Instance.Deck.CardsInDeck > 0) {
                    Singleton<CardDrawPiles>.Instance.Deck.Draw();
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
            this.currentTurnDelay = TurnDelay;
            base.Card.AddTemporaryMod(new() { negateAbilities = new() { this.Ability } });
        }

        // prevent turn counter from decrementing
        public override bool RespondsToUpkeep(bool playerUpkeep) => false;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            yield break;
        }
    }
}
