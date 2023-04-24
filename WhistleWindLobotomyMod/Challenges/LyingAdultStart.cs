using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod.Core.Challenges
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
                -20,
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionLiarStart),
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionLiarStart_activated), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.Instance.EventFlags)
                .Challenge.challengeType;
        }
    }
}
