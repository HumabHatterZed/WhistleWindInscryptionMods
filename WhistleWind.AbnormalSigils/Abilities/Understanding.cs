using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Understanding() {
            const string rulebookName = "Understanding";
            const string rulebookDescription = "If [creature] perishes due to self-inflicted damage, deal 5 damage to opposing creatures and directly to the opponent.";
            const string dialogue = "Too slow.";
            Understanding.ability = AbnormalAbilityHelper.CreateAbility<Understanding>(
                "sigilUnderstanding",
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: false, opponent: false, canStack: false)


.Id;
        }
    }
    /// <summary>
    /// If [creature] perishes due to self-inflicted damage, deal 5 damage to opposing creatures and directly to the opponent.
    /// </summary>
    public class Understanding : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => killer == base.Card;

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            foreach (PlayableCard card in BoardManager.Instance.GetCards(base.Card.OpponentCard)) {
                yield return card.TakeDamage(5, base.Card);
            }
            yield return LifeManager.Instance.ShowDamageSequence(5, 1, base.Card.OpponentCard);
            yield return base.LearnAbility(0.4f);
        }
    }
}
