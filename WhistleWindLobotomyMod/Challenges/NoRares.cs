using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges {
    public static class NoRares {
        internal const string title = "No Abnormal Rares";
        internal const string description = "Abnormal card choices cannot offer rare cards.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register() {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                5,
                TextureLoader.LoadTextureFromFile("ascensionNoRares.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoRares_activated.png")
                ).Challenge.challengeType;
        }
    }
}
