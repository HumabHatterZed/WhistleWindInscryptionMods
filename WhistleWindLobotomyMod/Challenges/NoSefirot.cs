using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class NoSefirot {
        internal const string TITLE = "No Sefirot";
        internal const string DESCRIPTION = "Sefirot card choices will not appear.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                5,
                TextureLoader.LoadTextureFromFile("ascensionNoSefirot.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoSefirot_activated.png"),
                -12
                ).Challenge.challengeType;
        }
    }
}
