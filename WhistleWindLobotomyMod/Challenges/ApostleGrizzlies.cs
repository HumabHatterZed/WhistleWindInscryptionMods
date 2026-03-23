using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class ApostleGrizzlies {
        internal const string TITLE = "Apostle Grizzlies";
        internal const string DESCRIPTION = "Apostles appear during the first 3 boss battles instead.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                10,
                TextureLoader.LoadTextureFromFile("ascensionApostleBear.png"),
                TextureLoader.LoadTextureFromFile("ascensionApostleBear_activated.png")
                )
                .SetDependantChallengeGetterStatic(AscensionChallenge.GrizzlyMode)
                .Challenge.challengeType;
        }
    }
}
