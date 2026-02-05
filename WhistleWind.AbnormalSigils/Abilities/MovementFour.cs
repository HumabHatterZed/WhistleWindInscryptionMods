using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Conductor4() {
            const string rulebookName = "Fourth Movement: Stringendo";
            const string rulebookDescription = "All other creatures on the board gain 1 Power and Fervent Adoration. At the start of the owner's next turn, begin the Finale.";
            const string triggerText = "The music shall perforate your entire being.";
            MovementFour.ability = AbilityHelper.NewFiller<MovementFour>(
                pluginGuid, "sigilMovementFour", rulebookName, rulebookDescription)
                .SetPart3Rulebook()
                .SetMagnificusRulebook()
                .SetGrimoraRulebook()
                .SetAbilityRedirect("Finale", MovementFive.ability, Color.red)
                .Info.SetAbilityLearnedDialogue(triggerText)
                .SetGBCTriggerText(triggerText)
                .SetPassive(false)
                .SetPowerlevel(5)
                .SetAbilityRedirect("Fervent Adoration", Fervent.iconId, GameColors.Instance.darkRed)
                .ability;
        }
    }
    /// <summary>
    /// All other creatures on the board gain 2 Power and Fervent Adoration. At the start of the owner's next turn, begin the Finale.
    /// </summary>
    public class MovementFour : ConductorMovementBase {
        public static Ability ability;
        public override Ability Ability => MovementFour.ability;
        public override Ability NextMovement => MovementFive.ability;

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn() {
            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard) {
                if (card != null && card.HasStatusEffect<Fervent>()) {
                    yield return card.RemoveStatusEffect<Fervent>();
                }
            }
        }
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) {
            return otherCard != null && otherCard.Slot != base.Card.Slot && !otherCard.HasStatusEffect<Fervent>();
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            yield return otherCard.AddStatusEffect<Fervent>(1);
        }
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard) {
                if (card != null && card != base.Card && !card.HasStatusEffect<Fervent>()) {
                    yield return card.AddStatusEffect<Fervent>(1);
                }
            }
        }

        public override IEnumerator OnUpkeep(bool onPlayerUpkeep) {
            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard) {
                if (card != null && card != base.Card && !card.HasStatusEffect<Fervent>()) {
                    yield return card.AddStatusEffect<Fervent>(1);
                }
            }
            yield return base.OnUpkeep(onPlayerUpkeep);
        }
        public override int GetPassiveAttackBuff(PlayableCard target) {
            if (base.Card.OnBoard && target.OnBoard && target != base.Card) {
                return 1;
            }
            return 0;
        }
    }
}
