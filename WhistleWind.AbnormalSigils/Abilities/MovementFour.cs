using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
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
        private void Ability_Conductor4()
        {
            const string rulebookName = "Fourth Movement: Stringendo";
            const string rulebookDescription = "All other creatures on the board gain 2 Power and Fervent Adoration. At the start of the owner's next turn, begin the Finale.";
            const string triggerText = "The music shall perforate your entire being.";
            MovementFour.ability = AbilityHelper.NewFiller<MovementFour>(
                pluginGuid, "sigilMovementFour", rulebookName, rulebookDescription)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook()
                .Info.SetAbilityLearnedDialogue(triggerText).SetGBCTriggerText(triggerText).SetPassive(false).SetPowerlevel(5)
                .SetAbilityRedirect("Fervent Adoration", Fervent.iconId, GameColors.Instance.darkRed).ability;
        }
    }
    public class MovementFour : ConductorMovementBase
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability NextMovement => MovementFive.ability;

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
            yield return base.OnUpkeep(onPlayerUpkeep);
            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy)
            {
                if (slot.Card != null && !slot.Card.HasStatusEffect<Fervent>() && slot.Card != base.Card)
                {
                    yield return slot.Card.AddStatusEffect<Fervent>(1);
                }
            }
        }
        public override int GetPassiveAttackBuff(PlayableCard target)
        {
            if (base.Card.OnBoard && target.OnBoard && target != base.Card)
            {
                /*if (!target.HasStatusEffect<Fervent>())
                {
                    target.AddStatusEffect<Fervent>(1);
                }*/
                return 2;
            }
            return 0;
        }
    }
}
