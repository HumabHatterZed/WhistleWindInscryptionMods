using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
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
        private void Ability_Conductor3()
        {
            const string rulebookName = "Third Movement: Accelerando";
            const string rulebookDescription = "All other creatures on the board gain 1 Power. At the start of the owner's next turn, begin the Fourth Movement: Stringendo.";
            const string triggerText = "The orchestra gives impetus to the music, bringing the entire world to its demise.";
            MovementThree.ability = AbilityHelper.NewFiller<MovementThree>(
                pluginGuid, "sigilMovementThree", rulebookName, rulebookDescription)
                .Info.SetAbilityLearnedDialogue(triggerText).SetGBCTriggerText(triggerText).SetPassive(false).SetPowerlevel(5).ability;
        }
    }
    public class MovementThree : ConductorMovementBase
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability NextMovement => MovementFour.ability;

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
                return 1;
            }
            return 0;
        }
    }
}
