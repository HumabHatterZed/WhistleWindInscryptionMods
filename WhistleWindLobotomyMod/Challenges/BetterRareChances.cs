using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges {
    public static class BetterRareChances {
        private const string TITLE = "Better Rare Chances";
        private const string DESCRIPTION = "Abnormal card choices offer rare cards more often.";

        public static AscensionChallenge ID { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }

        public static void Register() {
            Info = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                -5,
                TextureLoader.LoadTextureFromFile("ascensionBetterRareChances.png"),
                TextureLoader.LoadTextureFromFile("ascensionBetterRareChances_activated.png")
                );

            ID = Info.Challenge.challengeType;
        }
    }
}
