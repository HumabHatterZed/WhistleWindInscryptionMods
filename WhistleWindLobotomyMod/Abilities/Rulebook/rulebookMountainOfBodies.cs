using DiskCardGame;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_MountainOfBodies()
        {
            const string rulebookName = "Mountain of Smiling Bodies";
            const string rulebookDescription = "Transforms whenever it kills a card, up to two times. When killed after transforming, returns to a previous forme.";
            const string dialogue = "femboy";
            EntryMountainOfBodies.ability = AbilityHelper.CreateAbility<EntryMountainOfBodies>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryMountainOfBodies : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
