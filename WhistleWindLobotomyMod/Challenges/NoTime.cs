using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class NoTime {
        internal const string title = "No Time Machine";
        internal const string description = "You cannot obtain or use Backward Clock.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register(Harmony instance) {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                3,
                TextureLoader.LoadTextureFromFile("ascensionNoTime.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoTime_activated.png")
                ).Challenge.challengeType;
        }
    }
}
