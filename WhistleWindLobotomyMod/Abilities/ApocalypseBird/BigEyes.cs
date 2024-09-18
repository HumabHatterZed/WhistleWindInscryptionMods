using DiskCardGame;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class BigEyes : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_BigEyes()
        {
            const string rulebookName = "Big Eyes";
            BigEyes.ability = AbilityHelper.New<BigEyes>(pluginGuid, "sigilBigEyes", rulebookName,
                "While [creature] is on the board, all creatures are unaffected by Power-changing effects.",
                0, true).Id;
        }
    }
}
