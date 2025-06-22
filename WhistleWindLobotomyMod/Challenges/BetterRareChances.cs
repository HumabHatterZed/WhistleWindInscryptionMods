using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges {
    public static class BetterRareChances {
        internal const string title = "Better Rare Chances";
        internal const string description = "Abnormal card choices offer rare cards more often.";

        public static AscensionChallenge Id { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }

        public static void Register() {
            Info = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                -5,
                TextureLoader.LoadTextureFromFile("ascensionBetterRareChances.png"),
                TextureLoader.LoadTextureFromFile("ascensionBetterRareChances_activated.png")
                );

            Id = Info.Challenge.challengeType;
        }
    }
}
