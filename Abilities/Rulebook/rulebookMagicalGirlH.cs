using DiskCardGame;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_MagicalGirlH()
        {
            const string rulebookName = "Magical Girl Heart";
            const string rulebookDescription = "Transforms when the 2 more allied cards than opponent cards have died, or vice versa. Transforms on upkeep.";
            const string dialogue = "femboy";
            EntryMagicalGirlH.ability = AbilityHelper.CreateAbility<EntryMagicalGirlH>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryMagicalGirlH : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
