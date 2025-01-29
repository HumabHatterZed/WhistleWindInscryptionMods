using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Conductor2()
        {
            const string rulebookName = "Second Movement: Sostenuto";
            const string rulebookDescription = "Allied creatures gain 1 Power. At the start of the owner's next turn, begin the Third Movement: Accelerando.";
            const string triggerText = "The orchestra gives impetus to the music, bringing the entire world to its demise.";
            MovementTwo.ability = AbilityHelper.NewFiller<MovementTwo>(
                pluginGuid, "sigilMovementTwo", rulebookName, rulebookDescription)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook()
                .Info.SetAbilityLearnedDialogue(triggerText).SetGBCTriggerText(triggerText).SetPassive(false).SetPowerlevel(5).ability;
        }
    }
    public class MovementTwo : ConductorMovementBase
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability NextMovement => MovementThree.ability;
        public override int GetPassiveAttackBuff(PlayableCard target)
        {
            if (base.Card.OnBoard && target.OnBoard && target.OpponentCard == base.Card.OpponentCard && target != base.Card)
            {
                return 1;
            }
            return 0;
        }
    }
}
