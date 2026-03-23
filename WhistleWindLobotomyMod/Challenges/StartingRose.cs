using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingRose {
        internal const string TITLE = "Staining Rose";
        internal const string DESCRIPTION = "You start with the Staining Rose item.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                20,
                TextureLoader.LoadTextureFromFile("ascensionLiarStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionLiarStart_activated.png"), 0)
                .Challenge.challengeType;
        }
    }
}
