using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class StartingJester
    {
        internal const string title = "Start with a Fool";
        internal const string description = "Start your run with Jester of Nihil in your deck.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                -10,
                TextureLoader.LoadTextureFromFile("ascensionJesterStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionJesterStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedJesterOfNihil || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
