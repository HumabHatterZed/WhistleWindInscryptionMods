using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Conductor3() {
            const string rulebookName = "Third Movement: Accelerando";
            const string rulebookDescription = "All other creatures on the board gain 1 Power. At the start of the owner's next turn, begin the Fourth Movement: Stringendo.";
            const string triggerText = "The orchestra gives impetus to the music, bringing the entire world to its demise.";
            MovementThree.ID = AbilityHelper.NewFiller<MovementThree>(
                pluginGuid, "sigilMovementThree", rulebookName, rulebookDescription)
                .SetAbilityRedirect("Stringendo", MovementFour.ID, Color.red)
                .SetPassive(false)
                .SetPowerlevel(5)
                .ForceAddToRulebook()
                .Info.SetAbilityLearnedDialogue(triggerText)
                .SetGBCTriggerText(triggerText)
                .ability;
        }
    }
    /// <summary>
    /// All other creatures on the board gain 1 Power. At the start of the owner's next turn, begin the Fourth Movement: Stringendo.
    /// </summary>
    public class MovementThree : ConductorMovementBase {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override Ability NextMovement => MovementFour.ID;

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
