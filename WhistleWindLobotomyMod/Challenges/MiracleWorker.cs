using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod.Challenges {
    public static class MiracleWorker {
        internal const string TITLE = "Miracle Worker";
        internal const string DESCRIPTION = "Leshy may play Plague Doctor in battle. Beware the Clock.";

        public static AscensionChallenge ID { get; private set; }

        // Creates the challenge then calls the relevant patches
        internal static void Register(Harmony harmony) {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                12,
                TextureLoader.LoadTextureFromFile("ascensionMiracleWorker.png"),
                TextureLoader.LoadTextureFromFile("ascensionMiracleWorker_activated.png")
                ).Challenge.challengeType;

            harmony.PatchAll(typeof(MiracleWorker));
        }

        private static readonly Opponent.Type[] BLACKLISTED_OPPONENTS = new Opponent.Type[] {
            //LobOpponentUtils.SaviourBossID,
            LobOpponentUtils.ApocalypseBossID,
            OrdealUtils.OpponentID,
            OrdealUtils.SweeperOpponentID
        };

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPostfix]
        private static void AddPlagueDoctor(ref Opponent __result) {
            if (!LobotomyConfigManager.ChallengeIsActive(ID) || BLACKLISTED_OPPONENTS.Contains(__result.OpponentType)) {
                return;
            }

            List<List<CardInfo>> turnPlan = new(__result.TurnPlan);
            CardInfo doctorInfo = CardLoader.GetCardByName(Cards.plagueDoctor);
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();

            List<int> validIdxs = new();
            for (int i = 0; i < turnPlan.Count; i++) {
                // if this turn can have plague doctor inserted into it
                // keep track of list valid indexes, as well as indexes that have occupied cards
                if (turnPlan[i].Count < 4 && !turnPlan[i].Exists(x => x.HasTrait(Trait.Giant)))
                    validIdxs.Add(i);
            }
            // insert plague doctor into a random turn or make a new turn and insert it randomly into the plan
            if (validIdxs.Count > 0)
                turnPlan[validIdxs[SeededRandom.Range(0, validIdxs.Count, randomSeed++)]].Add(doctorInfo);
            else
                turnPlan.Insert(SeededRandom.Range(0, turnPlan.Count, randomSeed++), new() { doctorInfo });

            __result.TurnPlan = turnPlan;
        }
    }
}
