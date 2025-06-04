using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class StartingApocalypse
    {
        public static AscensionChallenge Id { get; private set; }
        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Start with a Beast",
                "Start your run with Apocalypse Bird in your deck.",
                -10,
                TextureLoader.LoadTextureFromFile("ascensionBeastStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionBeastStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
