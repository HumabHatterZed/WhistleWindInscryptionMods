using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingLiar {
        private const string TITLE = "Start with a Liar";
        private const string DESCRIPTION = "Start your run with Adult Who Tells Lies in your deck.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                -10,
                TextureLoader.LoadTextureFromFile("ascensionLiarStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionLiarStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
