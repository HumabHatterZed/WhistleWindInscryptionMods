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
                "While in your hand: at the end of your turn, this card will inflict 1 Paper Rose on of your other cards. The longer this card is in your hand, the stronger this effect becomes.",
                0, true)
                .SetAbilityRedirect("Paper Rose", PaperRose.iconId, Color.red)
                .Id;
        }
    }

    /// <summary>
    /// While in your hand: at the end of your turn, this card will inflict 1 Paper Rose on of your other cards. The longer this card is in your hand, the stronger this effect becomes.
    /// </summary>
    public class OrigamiGarden : AbilityBehaviour, IOnTurnEndInHand {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public bool RespondsToTurnEndInHand(bool playerTurn) => base.Card.InHand && playerTurn;
        public IEnumerator OnTurnEndInHand(bool playerTurn) {
            PlayableCard target;
            int randSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            List<PlayableCard> nonPaperRoseCards;
            // get all cards in hoof and on the board then apply paper rose to one of them
            List<PlayableCard> allPlayerCards = BoardManager.Instance.GetPlayerCards();
            allPlayerCards.AddRange(PlayerHand.Instance.CardsInHand);
            allPlayerCards.RemoveAll(x => x.Info.IsSpell() || x.HasAbility(RoseChosen.ID));
            allPlayerCards.Remove(this.Card);
            
            nonPaperRoseCards = allPlayerCards.FindAll(x => !x.HasStatusEffect<PaperRose>());

            for (int i = 0; i < 1 + (TurnManager.Instance.TurnNumber - 1) / 3; i++) {
                if (nonPaperRoseCards.Count > 0) {
                    target = nonPaperRoseCards[SeededRandom.Range(0, nonPaperRoseCards.Count, randSeed++)];
                    nonPaperRoseCards.Remove(target);
                }
                else {
                    target = allPlayerCards[SeededRandom.Range(0, allPlayerCards.Count, randSeed++)];
                }
                yield return target.AddStatusEffect<PaperRose>(1);
            }

            yield return base.LearnAbility(0.5f);
        }
    }
}
