using DiskCardGame;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class GiantBlocker : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_GiantBlocker()
        {
            const string rulebookName = "Soulbound Flesh";
            GiantBlocker.ability = AbilityHelper.New<GiantBlocker>(pluginGuid, "sigilGiantBlocker", rulebookName,
                "At the end of the owner's turn, deal direct damage to the owner proportional to how much damage this card received during the turn.",
                -3, true).Id;
        }
    }
}
