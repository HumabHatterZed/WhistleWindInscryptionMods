using DiskCardGame;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class SmallBeak : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_SmallBeak()
        {
            const string rulebookName = "Small Beak";
            SmallBeak.ability = AbilityHelper.New<SmallBeak>(pluginGuid, "sigilSmallBeak", rulebookName,
                "At the start of the player's turn, target a random lane on the board.  At the start of the player's next turn, kill all cards in the targeted lane, excluding this card.",
                0, true).Id;
        }
    }
}
