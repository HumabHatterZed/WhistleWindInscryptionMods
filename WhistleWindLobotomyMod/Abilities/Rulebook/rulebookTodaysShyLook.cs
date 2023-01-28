using DiskCardGame;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_TodaysShyLook()
        {
            const string rulebookName = "Today's Shy Look";
            const string rulebookDescription = "Transforms when drawn.";
            const string dialogue = "femboy";
            EntryTodaysShyLook.ability = AbilityHelper.CreateAbility<EntryTodaysShyLook>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryTodaysShyLook : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
