using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Ascension;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public static class MiracleWorker
    {
        public static AscensionChallenge Id { get; private set; }

        private static Texture2D portrait = null;
        private static Texture2D emissive = null;

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                WstlPlugin.pluginGuid,
                "Miracle Worker",
                "Leshy will play Plague Doctor during regular battles. Beware the Clock.",
                20,
                WstlTextureHelper.LoadTextureFromResource(Artwork.ascensionMiracleWorker),
                WstlTextureHelper.LoadTextureFromResource(Artwork.ascensionMiracleWorker_activated)
                ).Challenge.challengeType;

            harmony.PatchAll(typeof(MiracleWorker));
        }

        private static readonly Opponent.Type[] SUPPORTED_OPPONENTS = new Opponent.Type[] {
            Opponent.Type.ProspectorBoss,
            Opponent.Type.AnglerBoss,
            Opponent.Type.TrapperTraderBoss,
            Opponent.Type.LeshyBoss,
            Opponent.Type.PirateSkullBoss
        };

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPostfix]
        public static void AddPlagueDoctor(EncounterData encounterData, ref Opponent __result)
        {
            if (AscensionSaveData.Data.ChallengeIsActive(Id) || (!SaveFile.IsAscension && ConfigManager.Instance.MiracleWorker))
            {
                if (!SUPPORTED_OPPONENTS.Contains(encounterData.opponentType))
                    ChallengeActivationUI.TryShowActivation(Id);
                List<List<CardInfo>> turnPlan = __result.TurnPlan;
                CardInfo info = CardLoader.GetCardByName("wstl_plagueDoctor");
                UpdatePortrait();
                info.SetPortrait(portrait, emissive);
                for (int i = 0; i < turnPlan.Count(); i++)
                {
                    if (turnPlan[i].Count() > 0 && turnPlan[i].Count() < 4)
                    {
                        turnPlan[i].Add(info);
                        break;
                    }
                }
                WstlPlugin.Log.LogDebug($"start5");
                __result.TurnPlan = turnPlan;
            }
        }
        private static void UpdatePortrait()
        {
            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor1);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor2);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor3);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor4);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor5);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor6);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor7);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor8);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor9);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor10);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor10_emission);
                    break;
                default:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor11);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor11_emission);
                    break;
            }
        }
    }
}
