using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Conductor1() {
            const string rulebookName = "First Movement: Adagio";
            const string rulebookDescription = "Creatures adjacent to [creature] gain 1 Power. At the start of the owner's next turn, begin the Second Movement: Sostenuto.";
            const string triggerText = "The beasts start to forget everything for the symphony.";
            MovementOne.ID = AbilityHelper.NewFiller<MovementOne>(
                pluginGuid, "sigilMovementOne", rulebookName, rulebookDescription)
                .SetAbilityRedirect("Sostenuto", MovementTwo.ID, Color.red)
                .SetPassive(false)
                .SetPowerlevel(3)
                .ForceAddToRulebook()
                .Info.SetAbilityLearnedDialogue(triggerText)
                .SetGBCTriggerText(triggerText)
                .ability;

            Fervent.data.IconInfo.SetAbilityRedirect("Movement", MovementOne.ID, GameColors.Instance.gray);
        }
    }
    /// <summary>
    /// Creatures adjacent to [creature] gain 1 Power. At the start of the owner's next turn, begin the Second Movement: Sostenuto.
    /// </summary>
    public class MovementOne : ConductorMovementBase {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override Ability NextMovement => MovementTwo.ID;
        public override int GetPassiveAttackBuff(PlayableCard target) {
            if (base.Card.OnBoard && target.OnBoard) {
                return target.Slot.GetAdjacentCards().Count(x => x.HasAbility(this.Ability));
            }
            return 0;
        }
    }
}
