using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddBigEyes() {
            const string rulebookName = "Big Eyes";
            BigEyes.ID = AbilityHelper.New<BigEyes>(LobotomyPlugin.pluginGuid, "sigilBigEyes", rulebookName,
                "While [creature] is on the board, all creatures are unaffected by Power-changing effects.",
                0, true).Id;
        }
    }

    public class BigEyes : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
}
