using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using System.Collections;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingRose {
        internal const string TITLE = "Painted Red";
        internal const string DESCRIPTION = "You start the run with the Curse of the Staining Rose.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register(Harmony harmonyInstance) {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                20,
                TextureLoader.LoadTextureFromFile("ascensionRose.png"),
                TextureLoader.LoadTextureFromFile("ascensionRose_activated.png"), 0)
                .Challenge.challengeType;

            harmonyInstance.PatchAll(typeof(StartingRose));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunIntroSequencer), nameof(RunIntroSequencer.RunIntroSequence))]
        private static IEnumerator AddStainingRoseBoon(IEnumerator result) {
            if (LobotomyConfigManager.ChallengeIsActive(StartingRose.ID)) {
                RunState.Run.playerDeck.AddBoon(Boons.RoseCurse);
            }
            yield return result;
        }
    }
}
