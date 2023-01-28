using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

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
                "Leshy will play Plague Doctor against you. Beware the Clock.",
                20,
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionMiracleWorker),
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionMiracleWorker_activated)
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
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor1);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor2);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor3);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor4);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor5);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor6);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor7);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor8);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor9);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor10);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor10_emission);
                    break;
                default:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor11);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor11_emission);
                    break;
            }
        }
    }
}
