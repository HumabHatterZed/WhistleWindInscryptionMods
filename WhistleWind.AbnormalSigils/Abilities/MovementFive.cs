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
        private void Ability_Conductor5() {
            const string rulebookName = "Finale";
            const string rulebookDescription = "All other creatures on the board gain 2 Power and Fervent Adoration. At the start of the owner's next turn, remove this card from the board.";
            const string triggerText = "The performance does not end. And Da capo. And Da capo al Fine.";
            MovementFive.ID = AbilityHelper.NewFiller<MovementFive>(
                pluginGuid, "sigilMovementFive", rulebookName, rulebookDescription)
                .SetAbilityRedirect("Fervent Adoration", Fervent.iconId, GameColors.Instance.darkRed)
                .SetPassive(false)
                .SetPowerlevel(3)
                .ForceAddToRulebook()
                .Info.SetAbilityLearnedDialogue(triggerText)
                .SetGBCTriggerText(triggerText)
                .ability;
        }
    }
    /// <summary>
    /// All other creatures on the board gain 3 Power and Fervent Adoration. At the start of the owner's next turn, this card will perish.
    /// </summary>
    public class MovementFive : MovementFour {
        public new static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override Ability NextMovement => Ability.None;
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep) {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.RemoveFromBoard();
            if (!BoardManager.Instance.CardsOnBoard.Exists(x => x.IsConductor())) {
                yield return base.OnDrawn();
            }
            
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }

        public override int GetPassiveAttackBuff(PlayableCard target) {
            if (base.Card.OnBoard && target.OnBoard && target != base.Card) {
                return 2;
            }
            return 0;
        }
    }
}
