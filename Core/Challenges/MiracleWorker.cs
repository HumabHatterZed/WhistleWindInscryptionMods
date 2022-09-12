using InscryptionAPI;
using InscryptionAPI.Ascension;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;
using static WhistleWindLobotomyMod.AbnormalEncounterData;

namespace WhistleWindLobotomyMod
{
    public static class MiracleWorker
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                WstlPlugin.pluginGuid,
                "Miracle Worker",
                "Leshy will occasionally play Plague Doctor during regular battles. Beware the Clock.",
                20,
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionMiracleWorker),
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionMiracleWorker_activated)
                ).Challenge.challengeType;

            harmony.PatchAll(typeof(MiracleWorker));
        }

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPostfix]
        public static void AddPlagueDoctor(ref Opponent __result)
        {
            if (AscensionSaveData.Data.ChallengeIsActive(Id) || (!SaveFile.IsAscension && ConfigManager.Instance.MiracleWorker))
            {
                ChallengeActivationUI.TryShowActivation(Id);
                List<List<CardInfo>> turnPlan = __result.TurnPlan;
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
                if (SeededRandom.Range(0, 12, randomSeed++) == 0 || true)
                {
                    for (int i = 0; i < turnPlan.Count(); i++)
                    {
                        if (turnPlan[i].Count() > 0 && turnPlan[i].Count() < 4)
                        {
                            turnPlan[i].Add(CardLoader.GetCardByName("wstl_plagueDoctor"));
                            break;
                        }
                    }
                }
                WstlPlugin.Log.LogDebug($"start5");
                __result.TurnPlan = turnPlan;
            }
        }
    }
}
