using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingApocalypse {
        internal const string title = "Start with a Beast";
        internal const string description = "Start your run with Apocalypse Bird in your deck.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register() {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                -10,
                TextureLoader.LoadTextureFromFile("ascensionBeastStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionBeastStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
