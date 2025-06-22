using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class ApostleGrizzlies {
        internal const string title = "Apostle Grizzlies";
        internal const string description = "Apostles appear during the first 3 boss battles instead.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register() {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                10,
                TextureLoader.LoadTextureFromFile("ascensionApostleBear.png"),
                TextureLoader.LoadTextureFromFile("ascensionApostleBear_activated.png")
                )
                .SetDependantChallengeGetterStatic(AscensionChallenge.GrizzlyMode)
                .Challenge.challengeType;
        }
    }
}
