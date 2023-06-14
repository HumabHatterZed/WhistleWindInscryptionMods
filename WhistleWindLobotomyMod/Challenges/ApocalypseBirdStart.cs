using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Core.Challenges
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
                -20,
                TextureLoader.LoadTextureFromFile("ascensionBeastStart"),
                TextureLoader.LoadTextureFromFile("ascensionBeastStart_activated"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomyConfigManager.Instance.EventFlags)
                .Challenge.challengeType;
        }
    }
}
