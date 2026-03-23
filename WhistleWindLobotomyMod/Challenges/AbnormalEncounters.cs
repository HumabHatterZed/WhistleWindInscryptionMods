using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyEncounterManager;

namespace WhistleWindLobotomyMod.Challenges {
    public static class AbnormalEncounters // taken from infiniscryption
    {
        private const string TITLE = "Abnormal Encounters";
        private const string DESCRIPTION = "Regular and totem battles will only use Abnormality cards.";

        public static AscensionChallenge ID { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }
        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony) {
            Info = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                10,
                TextureLoader.LoadTextureFromFile("ascensionAbnormalEncounters.png"),
                TextureLoader.LoadTextureFromFile("ascensionAbnormalEncounters_activated.png")
                );
            ID = Info.Challenge.challengeType;

            harmony.PatchAll(typeof(AbnormalEncounters));
        }

        [HarmonyPatch(typeof(GameFlowManager), nameof(GameFlowManager.Start))]
        [HarmonyPostfix]
        private static void ClearVanillaEncounters(GameFlowManager __instance) {
            if (__instance != null) {
                if (LobotomyConfigManager.ChallengeIsActive(ID)) {
                    if (!LobotomySaveManager.ShownAbnormalEncounters) {
                        LobotomySaveManager.ShownAbnormalEncounters = true;
                        ChallengeActivationUI.TryShowActivation(ID);
                    }

                    for (int i = 0; i < 3; i++)
                        RegionProgression.Instance.regions[i].encounters = ModEncounters[i];
                }
            }
        }
    }
}
