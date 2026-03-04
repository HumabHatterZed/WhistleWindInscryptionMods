using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Conductor2() {
            const string rulebookName = "Second Movement: Sostenuto";
            const string rulebookDescription = "Allied creatures gain 1 Power. At the start of the owner's next turn, begin the Third Movement: Accelerando.";
            const string triggerText = "The orchestra gives impetus to the music, bringing the entire world to its demise.";
            MovementTwo.ID = AbilityHelper.NewFiller<MovementTwo>(
                pluginGuid, "sigilMovementTwo", rulebookName, rulebookDescription)
                .SetAbilityRedirect("Accelerando", MovementThree.ID, Color.red)
                .SetPassive(false)
                .SetPowerlevel(5)
                .ForceAddToRulebook()
                .Info.SetAbilityLearnedDialogue(triggerText)
                .SetGBCTriggerText(triggerText)
                .ability;
        }
    }
    /// <summary>
    /// Allied creatures gain 1 Power. At the start of the owner's next turn, begin the Third Movement: Accelerando.
    /// </summary>
    public class MovementTwo : ConductorMovementBase {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override Ability NextMovement => MovementThree.ID;
        public override int GetPassiveAttackBuff(PlayableCard target) {
            if (base.Card.OnBoard && target.OnBoard && target.OpponentCard == base.Card.OpponentCard && target != base.Card) {
                return 1;
            }
            return 0;
        }
    }
}
