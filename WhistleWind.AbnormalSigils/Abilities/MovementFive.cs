using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Rulebook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Conductor5()
        {
            const string rulebookName = "Finale";
            const string rulebookDescription = "All other creatures on the board gain 3 Power and Fervent Adoration. At the start of the owner's next turn, this card will perish.";
            const string triggerText = "The performance does not end. And Da capo. And Da capo al Fine.";
            MovementFive.ability = AbilityHelper.NewFiller<MovementFive>(
                pluginGuid, "sigilMovementFive", rulebookName, rulebookDescription)
                .Info.SetAbilityLearnedDialogue(triggerText).SetGBCTriggerText(triggerText).SetPassive(false)
                .SetAbilityRedirect("Fervent Adoration", Fervent.iconId, GameColors.Instance.darkRed).SetPowerlevel(5).ability;
        }
    }
    public class MovementFive : ConductorMovementBase
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability NextMovement => Ability.None;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            return otherCard != null && otherCard.Slot != base.Card.Slot && !otherCard.HasStatusEffect<Fervent>();
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            yield return otherCard.AddStatusEffect<Fervent>(1);
        }
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy)
            {
                if (slot.Card != null && !slot.Card.HasStatusEffect<Fervent>() && slot.Card != base.Card)
                {
                    yield return slot.Card.AddStatusEffect<Fervent>(1);
                }
            }
        }
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep)
        {
            yield return new WaitForSeconds(0.5f);
            yield return base.Card.Die(false, null);
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility();
        }

        public override int GetPassiveAttackBuff(PlayableCard target)
        {
            if (base.Card.OnBoard && target.OnBoard && target != base.Card)
            {
                return 3;
            }
            return 0;
        }
    }
}
