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

        private static readonly Opponent.Type[] BLACKLISTED_OPPONENTS = new Opponent.Type[] {
            
        };

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPostfix]
        private static void AddPlagueDoctor(EncounterData encounterData, ref Opponent __result)
        {
            if (SaveFile.IsAscension ? AscensionSaveData.Data.ChallengeIsActive(Id) : LobotomyConfigManager.Instance.MiracleWorker)
            {
                List<List<CardInfo>> turnPlan = new(__result.TurnPlan);
                CardInfo info = CardLoader.GetCardByName("wstl_plagueDoctor");
                
                // if there are no turns that have 0 < cards < 4, insert a new turn at the start with just Plague Doctor
                if (!turnPlan.Exists(turn => turn.Count > 0 && turn.Count < 4))
                {
                    turnPlan.Insert(0, new() { info });
                }
                else
                {
                    for (int i = 0; i < turnPlan.Count; i++)
                    {
                        // add Plague Doctor to the first possible turn plan
                        if (turnPlan[i].Count > 0 && turnPlan[i].Count < 4 && !turnPlan[i].Exists(x => x.HasTrait(Trait.Giant)))
                        {
                            turnPlan[i].Add(info);
                            break;
                        }
                    }
                }
                __result.TurnPlan = turnPlan;
            }
        }
    }
}
