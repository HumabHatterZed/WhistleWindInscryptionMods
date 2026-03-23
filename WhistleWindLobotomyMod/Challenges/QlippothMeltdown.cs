using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class QlippothMeltdown {
        internal const string TITLE = "Qlippoth Meltdown";
        internal const string DESCRIPTION = "Leshy will occasionally play empowered Abnormalities.";

        public static AscensionChallenge ID { get; private set; }

        public static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                15,
                TextureLoader.LoadTextureFromFile("ascensionMeltdown.png"),
                TextureLoader.LoadTextureFromFile("ascensionMeltdown_activated.png")
                ).Challenge.challengeType;
        }
    }
}
