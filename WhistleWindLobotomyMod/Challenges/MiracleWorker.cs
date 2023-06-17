using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Core.Challenges
{
    public static class MiracleWorker
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Miracle Worker",
                "Leshy will play Plague Doctor against you. Beware the Clock.",
                12,
                TextureLoader.LoadTextureFromFile("ascensionMiracleWorker"),
                TextureLoader.LoadTextureFromFile("ascensionMiracleWorker_activated")
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
            if (SaveFile.IsAscension ? AscensionSaveData.Data.ChallengeIsActive(Id) : LobotomyConfigManager.Instance.MiracleWorker)
            {
                if (!SUPPORTED_OPPONENTS.Contains(encounterData.opponentType))
                    ChallengeActivationUI.TryShowActivation(Id);
                List<List<CardInfo>> turnPlan = __result.TurnPlan;
                CardInfo info = CardLoader.GetCardByName("wstl_plagueDoctor");
                for (int i = 0; i < turnPlan.Count(); i++)
                {
                    if (turnPlan[i].Count > 0 && turnPlan[i].Count < 4)
                    {
                        turnPlan[i].Add(info);
                        break;
                    }
                }
                LobotomyPlugin.Log.LogDebug($"start5");
                __result.TurnPlan = turnPlan;
            }
        }
    }
}
