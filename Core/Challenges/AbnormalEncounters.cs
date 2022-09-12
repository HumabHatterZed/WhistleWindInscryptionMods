using InscryptionAPI;
using InscryptionAPI.Ascension;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;
using static WhistleWindLobotomyMod.AbnormalEncounterData;

namespace WhistleWindLobotomyMod
{
    public static class AbnormalEncounters // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                WstlPlugin.pluginGuid,
                "Abnormal Encounters",
                "All regular battles will only use abnormality cards.",
                20,
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionAbnormalEncounters),
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionAbnormalEncounters_activated)
                ).Challenge.challengeType;

            harmony.PatchAll(typeof(AbnormalEncounters));
        }

        [HarmonyPatch(typeof(RunState), nameof(RunState.Initialize))]
        [HarmonyPostfix]
        public static void ClearVanillaEncounters(ref RunState __instance)
        {
            if (!(__instance != null) && (AscensionSaveData.Data.ChallengeIsActive(Id) || (!SaveFile.IsAscension && ConfigManager.Instance.AbnormalBattles)))
            {
                //ChallengeActivationUI.TryShowActivation(Id);
                RegionProgression.Instance.regions[0].encounters.Clear();
                RegionProgression.Instance.regions[1].encounters.Clear();
                RegionProgression.Instance.regions[2].encounters.Clear();
                RegionProgression.Instance.regions[0].AddEncounters(StrangePack, BitterPack, StrangeFlock, HelperJuggernaut);
                RegionProgression.Instance.regions[1].AddEncounters(StrangeBees, StrangeCreatures1, WormsNest, StrangeCreatures2, StrangeFish);
                RegionProgression.Instance.regions[2].AddEncounters(StrangeHerd, AlriuneJuggernaut, SpidersNest, SwanJuggernaut);
            }
        }
    }
}
