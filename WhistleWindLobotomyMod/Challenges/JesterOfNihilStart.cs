using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class JesterOfNihilStart
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Start with a Fool",
                "Start your run with Jester of Nihil in your deck.",
                -15,
                TextureLoader.LoadTextureFromFile("ascensionJesterStart"),
                TextureLoader.LoadTextureFromFile("ascensionJesterStart_activated"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedJesterOfNihil || LobotomyConfigManager.Instance.EventFlags)
                .Challenge.challengeType;
        }
    }
}
