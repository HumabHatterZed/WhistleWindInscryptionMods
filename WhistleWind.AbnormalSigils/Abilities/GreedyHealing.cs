using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_GreedyHealing() {
            const string rulebookName = "Malignant Regeneration";
            const string rulebookDescription = "At the end of the owner's turn, this card gains 1 Health. If its Health exceeds its maximum by 3, it will perish.";
            const string dialogue = "Your beast has Health in excess.";
            const string triggerText = "[creature] gives itself more Health!";
            GreedyHealing.ID = AbnormalAbilityHelper.CreateAbility<GreedyHealing>(
                "sigilGreedyHealing",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, this card gains 1 Health. If its Health exceeds its maximum by 3, it will perish.
    /// </summary>
    public class GreedyHealing : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            yield return PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);

            bool faceDown = base.Card.FaceDown;
            yield return base.Card.FlipFaceUp(faceDown);
            base.Card.Anim.LightNegationEffect();
            base.Card.HealDamage(1);
            yield return new WaitForSeconds(0.3f);

            if (base.Card.Health < base.Card.MaxHealth + 3) {
                yield return base.LearnAbility(0.3f);
                yield return base.Card.FlipFaceDown(faceDown);
            }
            else {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.55f);
                for (int i = 0; i < 3; i++) {
                    base.Card.HealDamage(Mathf.Max(1, base.Card.Health) * (i + 1));
                    base.Card.Anim.PlayHitAnimation();
                    yield return new WaitForSeconds(0.2f);
                }
                yield return base.Card.Die(false, null);
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayDialogueEvent("RegeneratorOverheal");
            }
        }
    }
}
