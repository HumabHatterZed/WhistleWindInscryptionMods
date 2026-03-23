using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingApocalypse {
        private const string TITLE = "Start with a Beast";
        private const string DESCRIPTION = "Start your run with Apocalypse Bird in your deck.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                -10,
                TextureLoader.LoadTextureFromFile("ascensionBeastStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionBeastStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
