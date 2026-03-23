using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingJester {
        internal const string TITLE = "Start with a Fool";
        internal const string DESCRIPTION = "Start your run with Jester of Nihil in your deck.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                -10,
                TextureLoader.LoadTextureFromFile("ascensionJesterStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionJesterStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedJesterOfNihil || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
