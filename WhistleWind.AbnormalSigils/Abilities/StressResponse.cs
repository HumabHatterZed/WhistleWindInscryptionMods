using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_StressResponse() {
            const string rulebookName = "Stress Response";
            const string rulebookDescription = "If this card is at or below half Health after being struck, its next attack gains 1 Power. Also, gain 1 Power when struck by an ally.";
            const string dialogue = "A final show of force.";
            StressResponse.ability = AbnormalAbilityHelper.CreateAbility<StressResponse>(
                "sigilStressResponse",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// If this card is at or below half Health after being struck, its next attack gains 1 Power. Also, gain 1 Power when struck by an ally.
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
            if (attackedByAlly || base.Card.Health <= base.Card.MaxHealth / 2) {
                empowerNextAttack = true;
                if (base.Card.Info.name == "wstlWonder_reddenedBuddy") {
                    base.Card.Anim.StrongNegationEffect();
                    base.Card.SwitchToAlternatePortrait();
                }
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.LearnAbility(0.3f);
            }
        }

        public override bool RespondsToAttackEnded() => true;
        public override IEnumerator OnAttackEnded() {
            empowerNextAttack = attackedByAlly = false;
            yield break;
        }

        public int GetPassiveAttackBuff(PlayableCard target) {
            if (target == base.Card && empowerNextAttack) {
                return attackedByAlly ? 2 : 1;
            }
            return 0;
        }
    }
}