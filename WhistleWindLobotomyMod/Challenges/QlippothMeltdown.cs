using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class QlippothMeltdown {
        internal const string title = "Qlippoth Meltdown";
        internal const string description = "Leshy will occasionally play empowered Abnormalities.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register() {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                15,
                TextureLoader.LoadTextureFromFile("ascensionMeltdown.png"),
                TextureLoader.LoadTextureFromFile("ascensionMeltdown_activated.png")
                ).Challenge.challengeType;
        }
    }
}
