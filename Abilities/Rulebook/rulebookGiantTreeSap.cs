using DiskCardGame;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_GiantTreeSap()
        {
            const string rulebookName = "Giant Tree Sap";
            const string rulebookDescription = "When sacrificed, has a chance to cause the sacrificing card to explode.";
            const string dialogue = "femboy";
            EntryGiantTreeSap.ability = AbilityHelper.CreateAbility<EntryGiantTreeSap>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryGiantTreeSap : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
