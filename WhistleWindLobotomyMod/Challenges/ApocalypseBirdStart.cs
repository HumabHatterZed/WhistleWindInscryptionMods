using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class ApocalypseBirdStart
    {
        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Start with a Beast",
                "Start your run with Apocalypse Bird in your deck.",
                -15,
                TextureLoader.LoadTextureFromFile("ascensionBeastStart"),
                TextureLoader.LoadTextureFromFile("ascensionBeastStart_activated"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomyConfigManager.Instance.EventFlags)
                .Challenge.challengeType;
        }
    }
}
