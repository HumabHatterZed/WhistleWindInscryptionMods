using DiskCardGame;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSmallBeak() {
            const string rulebookName = "Small Beak";
            SmallBeak.ID = AbilityHelper.New<SmallBeak>(LobotomyPlugin.pluginGuid, "sigilSmallBeak", rulebookName,
                "At the start of the player's turn, target a random lane on the board.  At the start of the player's next turn, kill all cards in the targeted lane, excluding this card.",
                0, true).Id;
        }
    }

    public class SmallBeak : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
}
