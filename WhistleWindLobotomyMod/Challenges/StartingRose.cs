using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingRose {
        private const string TITLE = "Staining Rose";
        private const string DESCRIPTION = "The Adult boss will be stronger and guaranteed to appear.";

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
