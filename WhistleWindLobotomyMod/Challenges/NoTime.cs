using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class NoTime {
        internal const string TITLE = "No Time Machine";
        internal const string DESCRIPTION = "You cannot obtain or use Backward Clock.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register(Harmony instance) {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                5,
                TextureLoader.LoadTextureFromFile("ascensionNoTime.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoTime_activated.png")
                ).Challenge.challengeType;
        }
    }
}
