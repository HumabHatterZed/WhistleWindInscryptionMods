using DiskCardGame;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddMisdeeds() {
            const string rulebookName = "Misdeeds Not Allowed!";
            Misdeeds.ability = AbilityHelper.New<Misdeeds>(LobotomyPlugin.pluginGuid, "sigilMisdeeds", rulebookName,
                "Whenever [creature] takes damage, gain 1 Power until the end of the owner's turn.",
                0, true).Id;
        }
    }

    public class Misdeeds : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
