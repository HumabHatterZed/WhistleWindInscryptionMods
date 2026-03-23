using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges {
    public static class NoRares {
        internal const string TITLE = "No Abnormal Rares";
        internal const string DESCRIPTION = "Abnormal card choices cannot offer rare cards.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                5,
                TextureLoader.LoadTextureFromFile("ascensionNoRares.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoRares_activated.png")
                ).Challenge.challengeType;
        }
    }
}
