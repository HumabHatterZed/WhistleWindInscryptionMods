using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddDazzling() {
            const string rulebookName = "Dazzling";
            Dazzling.ID = AbilityHelper.New<Dazzling>(LobotomyPlugin.pluginGuid, "sigilDazzling", rulebookName,
                "The turn after this card is played, inflict up to 3 other cards on the board with Enchanted. This card takes no damage from Enchanted cards.",
                0, true, "Like moths to a flame.")
                .SetAbilityRedirect("Enchanted", Enchanted.iconId, GameColors.Instance.gold).Id;
        }
    }
    public class Dazzling : AbilityBehaviour, IPreTakeDamage {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToUpkeep(bool playerUpkeep) {
            if (base.Card.Info.name == Cards.bigBird && TurnManager.Instance.TurnNumber > base.Card.TurnPlayed) {
                return playerUpkeep != base.Card.OpponentCard;
            }
            return false;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            List<PlayableCard> cards = BoardManager.Instance.GetCards(base.Card.OpponentCard);
            if (cards.Count > 0) {
                yield return base.PreSuccessfulTriggerSequence();
                PlayableCard card = cards.GetSeededRandom(base.GetRandomSeed());
                card.Anim.StrongNegationEffect();
                yield return card.AddStatusEffect<Enchanted>(1, modifyTurnGained: (int i) => i + 1);
                yield return new WaitForSeconds(0.4f);
            }
            base.Card.TurnPlayed = TurnManager.Instance.TurnNumber + 2;
        }

        public bool RespondsToPreTakeDamage(PlayableCard source, int damage) {
            return source != null && source.HasStatusEffect<Enchanted>(true);
        }

        public IEnumerator OnPreTakeDamage(PlayableCard source, int damage) {
            base.Card.Anim.StrongNegationEffect();
            yield return source.Die(false, null);
            if (!base.HasLearned) {
                yield return new WaitForSeconds(0.5f);
                yield return base.LearnAbility();
            }
            else
                yield return new WaitForSeconds(0.25f);
        }
    }
}
