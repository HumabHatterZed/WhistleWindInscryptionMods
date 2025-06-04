using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyEncounterManager;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class AbnormalEncounters // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Abnormal Encounters",
                "Regular and totem battles will only use Abnormality cards.",
                10,
                TextureLoader.LoadTextureFromFile("ascensionAbnormalEncounters.png"),
                TextureLoader.LoadTextureFromFile("ascensionAbnormalEncounters_activated.png")
                )
                .Challenge.challengeType;

            harmony.PatchAll(typeof(AbnormalEncounters));
        }

        [HarmonyPatch(typeof(GameFlowManager), nameof(GameFlowManager.Start))]
        [HarmonyPostfix]
        private static void ClearVanillaEncounters(GameFlowManager __instance)
        {
            if (__instance != null)
            {
                if (SaveFile.IsAscension ? AscensionSaveData.Data.ChallengeIsActive(Id) : LobotomyConfigManager.AbnormalBattles)
                {
                    if (!LobotomySaveManager.ShownAbnormalEncounters)
                    {
                        LobotomySaveManager.ShownAbnormalEncounters = true;
                        ChallengeActivationUI.TryShowActivation(Id);
                    }

                    for (int i = 0; i < 3; i++)
                        RegionProgression.Instance.regions[i].encounters = ModEncounters[i];
                }
            }
        }
    }
}
