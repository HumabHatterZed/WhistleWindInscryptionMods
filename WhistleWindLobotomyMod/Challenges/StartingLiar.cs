using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class StartingLiar
    {
        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Start with a Liar",
                "Start your run with Adult Who Tells Lies in your deck.",
                -10,
                TextureLoader.LoadTextureFromFile("ascensionLiarStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionLiarStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
