using DiskCardGame;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Misdeeds : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Misdeeds()
        {
            const string rulebookName = "Misdeeds Not Allowed!";
            Misdeeds.ability = AbilityHelper.New<Misdeeds>(pluginGuid, "sigilMisdeeds", rulebookName,
                "Whenever [creature] takes damage, gain 1 Power until the end of the owner's turn.",
                0, true).Id;
        }
    }
}
