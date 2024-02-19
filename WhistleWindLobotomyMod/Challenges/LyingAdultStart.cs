using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class LyingAdultStart
    {
        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Start with a Liar",
                "Start your run with Adult Who Tells Lies in your deck.",
                -15,
                TextureLoader.LoadTextureFromFile("ascensionLiarStart"),
                TextureLoader.LoadTextureFromFile("ascensionLiarStart_activated"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.Instance.EventFlags)
                .Challenge.challengeType;
        }
    }
}
