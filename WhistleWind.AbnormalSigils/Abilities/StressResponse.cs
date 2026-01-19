using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_StressResponse() {
            const string rulebookName = "Stress Response";
            const string rulebookDescription = "When this card is struck by an ally or while at or below half Health, its next attack gains 1 Power. These effects stack with each other.";
            const string dialogue = "A final show of force.";
            StressResponse.ability = AbnormalAbilityHelper.CreateAbility<StressResponse>(
                "sigilStressResponse",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// When this card is struck by an ally or while at or below half Health, its next attack gains 1 Power. These effects stack with each other.
    /// </summary>
    public class StressResponse : AbilityBehaviour, IPassiveAttackBuff {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool empowerNextAttack;
        private bool attackedByAlly;

        public override bool RespondsToTakeDamage(PlayableCard source) => source != null && base.Card.Health > 0 && !base.Card.Dead;
        public override IEnumerator OnTakeDamage(PlayableCard source) {
            if (source.OpponentCard == base.Card.OpponentCard) {
                attackedByAlly = true;
            }
            bool halfHealth = (float)base.Card.Health / base.Card.MaxHealth <= 0.5f;

            yield return base.PreSuccessfulTriggerSequence();

            if ((float)base.Card.Health / base.Card.MaxHealth <= 0.5f) {
                if (base.Card.Info.name == "wstlWonder_reddenedBuddy") {
                    base.Card.Anim.StrongNegationEffect();
                    base.Card.SwitchToAlternatePortrait();
                }
                yield return base.LearnAbility(0.3f);
            }
        }

        public override bool RespondsToAttackEnded() => true;
        public override IEnumerator OnAttackEnded() {
            attackedByAlly = false;
            yield break;
        }

        public int GetPassiveAttackBuff(PlayableCard target) {
            if (target == base.Card) {
                int retval = attackedByAlly ? 1 : 0;
                if ((float)base.Card.Health / base.Card.MaxHealth <= 0.5f) {
                    retval++;
                }
                return retval;
            }
            return 0;
        }
    }
}