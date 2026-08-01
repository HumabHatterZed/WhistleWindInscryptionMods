using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
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
    public partial class Abilities {
        private static void AddOrigamiGarden() {
            OrigamiGarden.ID = AbilityHelper.New<OrigamiGarden>(LobotomyPlugin.pluginGuid,
                "sigilOrigamiGarden",
                "Origami Garden",
                "At the end of your turn, this card will inflict 1 Paper Rose on a card you own in hand or on the board. This effect becomes stronger the longer this card remains in your hand.",
                0, true)
                .SetAbilityRedirect("Paper Rose", PaperRose.iconId, Color.red)
                .Id;
        }
    }

    /// <summary>
    /// At the end of your turn, this card will inflict 1 Paper Rose on one of your cards, or deal 1 damage directly to you if no legible cards exist. This effect becomes stronger the longer this card remains in your hand.
    /// </summary>
    public class OrigamiGarden : AbilityBehaviour, IOnTurnEndInHand {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        // prevent triggering when battles have ended
        public bool RespondsToTurnEndInHand(bool playerTurn) {
            if (base.Card.InHand && playerTurn) {
                // GameIsOver doesn't trigger before TurnEnd is called
                return TurnManager.Instance.Opponent.NumLives > 1 || LifeManager.Instance.Balance < 5;
            }
            return false;
        }
        public IEnumerator OnTurnEndInHand(bool playerTurn) {
            PlayableCard target;
            int randSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            int numInstances = 1 + TurnManager.Instance.TurnNumber / 3;

            LobotomyPlugin.Log.LogDebug($"[OrigamiGarden] Num Targets: {numInstances}");

            // get all cards in hoof and on the board then apply paper rose to one of them
            List<PlayableCard> allPlayerCards = BoardManager.Instance.GetPlayerCards();
            allPlayerCards.AddRange(PlayerHand.Instance.CardsInHand);
            allPlayerCards.Remove(this.Card);
            // dont apply rose to spells or the resonating card
            allPlayerCards.RemoveAll(x => x.Info.IsSpell() || x.HasAbility(RoseChosen.ID));

            yield return HelperMethods.ChangeCurrentView(View.Hand);
            Singleton<PlayerHand>.Instance.OnCardInspected(this.Card);
            yield return new WaitForSeconds(0.5f);

            // if there are no eligible cards, deal damage to the player instead
            if (allPlayerCards.Count == 0) {
                yield return DialogueHelper.PlayDialogueEvent("StainingRoseDamagePlayer");

                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(numInstances, numInstances, toPlayer: true);
            }
            else {
                List<PlayableCard> nonPaperRoseCards = allPlayerCards.FindAll(x => !x.HasStatusEffect<PaperRose>());

                for (int i = 0; i < numInstances; i++) {
                    // if there are valid cards without rose status, prioritise applying rose to them
                    // otherwise, apply extra rose to cards with the status
                    if (nonPaperRoseCards.Count > 0) {
                        target = nonPaperRoseCards[SeededRandom.Range(0, nonPaperRoseCards.Count, randSeed++)];
                        nonPaperRoseCards.Remove(target);
                    }
                    else {
                        target = allPlayerCards[SeededRandom.Range(0, allPlayerCards.Count, randSeed++)];
                    }

                    if (target.InHand) {
                        Singleton<PlayerHand>.Instance.OnCardInspected(target);
                    }
                    yield return target.AddStatusEffect<PaperRose>(1);
                    target.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.5f);
                }
            }

            yield return base.LearnAbility(0.5f);
        }
    }
}
