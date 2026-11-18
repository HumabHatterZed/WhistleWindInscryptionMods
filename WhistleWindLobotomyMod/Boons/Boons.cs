using DiskCardGame;
using InscryptionAPI.Boons;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class Boons {
        public static BoonData.Type RoseCurse { get; private set; }
        internal static void Initialise() {
            RoseCurse = BoonManager.New<StainingRoseBoon>(LobotomyPlugin.pluginGuid,
                "Curse of the Staining Rose", "You will start the battle with a Staining Rose in your hand.",
                TextureLoader.LoadTextureFromFile("boonRoseIcon.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("boonRoseBackground.png", LobotomyPlugin.ModAssembly),
                stackable: false,
                appearInLeshyTrials: false);
        }
    }
}
