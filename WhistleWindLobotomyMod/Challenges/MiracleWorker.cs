using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Challenges
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
                60,
                TextureLoader.LoadTextureFromFile("ascensionMiracleWorker"),
                TextureLoader.LoadTextureFromFile("ascensionMiracleWorker_activated")
                ).Challenge.challengeType;

            harmony.PatchAll(typeof(MiracleWorker));
        }

        private static readonly Opponent.Type[] BLACKLISTED_OPPONENTS = new Opponent.Type[] {

        };

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPostfix]
        private static void AddPlagueDoctor(ref Opponent __result)
        {
            if (SaveFile.IsAscension ? AscensionSaveData.Data.ChallengeIsActive(Id) : LobotomyConfigManager.Instance.MiracleWorker)
            {
                List<List<CardInfo>> turnPlan = new(__result.TurnPlan);
                CardInfo doctorInfo = CardLoader.GetCardByName("wstl_plagueDoctor");
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();

                List<int> validIdxs = new();
                for (int i = 0; i < turnPlan.Count; i++)
                {
                    // if this turn can have plague doctor inserted into it
                    // keep track of list valid indexes, as well as indexes that have occupied cards
                    if (turnPlan[i].Count < 4 && !turnPlan[i].Exists(x => x.HasTrait(Trait.Giant)))
                        validIdxs.Add(i);
                }
                // insert plague doctor into a random turn or make a new turn and insert it randomly into the plan
                if (validIdxs.Count > 0)
                    turnPlan[SeededRandom.Range(0, validIdxs.Count, randomSeed++)].Add(doctorInfo);
                else
                    turnPlan.Insert(SeededRandom.Range(0, turnPlan.Count, randomSeed++), new() { doctorInfo });

                __result.TurnPlan = turnPlan;
            }
        }
    }
}
