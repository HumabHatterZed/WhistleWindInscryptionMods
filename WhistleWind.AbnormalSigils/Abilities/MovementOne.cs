using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Rulebook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Conductor1()
        {
            const string rulebookName = "First Movement: Adagio";
            const string rulebookDescription = "Creatures adjacent to [creature] gain 1 Power. At the start of the owner's next turn, begin the Second Movement: Sostenuto.";
            const string triggerText = "The beasts start to forget everything for the symphony.";
            MovementOne.ability = AbilityHelper.NewFiller<MovementOne>(
                pluginGuid, "sigilMovementOne", rulebookName, rulebookDescription)
                .Info.SetAbilityLearnedDialogue(triggerText).SetGBCTriggerText(triggerText).SetPassive(false).SetPowerlevel(3).ability;

            Fervent.data.IconInfo.SetAbilityRedirect("Movement", MovementOne.ability, GameColors.Instance.gray);
        }
    }
    public class MovementOne : ConductorMovementBase
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability NextMovement => MovementTwo.ability;
        public override int GetPassiveAttackBuff(PlayableCard target)
        {
            if (base.Card.OnBoard && target.OnBoard)
            {
                return target.Slot.GetAdjacentCards().Count(x => x.HasAbility(this.Ability));
            }
            return 0;
        }
    }
}
