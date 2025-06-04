using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class StartingLiar
    {
        internal const string title = "Start with a Liar";
        internal const string description = "Start your run with Adult Who Tells Lies in your deck.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title   ,
                description,
                -10,
                TextureLoader.LoadTextureFromFile("ascensionLiarStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionLiarStart_activated.png"), 0)
                .SetCustomUnlock(dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.EventFlags)
                .Challenge.challengeType;
        }
    }
}
