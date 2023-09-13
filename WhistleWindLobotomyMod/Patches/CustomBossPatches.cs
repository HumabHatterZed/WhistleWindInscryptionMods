/*using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Challenges;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Leshy;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class CustomBossPatches
    {
        private static readonly Opponent.Type[] CustomBosses = new Opponent.Type[]
        {
            ApocalypseBossOpponent.ID,
        };
        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPrefix]
        public static bool ReplaceBossEncounter(EncounterData encounterData, ref Opponent __result)
        {
            List<Opponent.Type> randomOpponents = new();
            bool apocalypse = LobotomyHelpers.IsChallengeConfigActive(FinalApocalypse.Id, LobotomyConfigManager.Instance.FinalApocalypse);
            bool jester = LobotomyHelpers.IsChallengeConfigActive(FinalApocalypse.Id, LobotomyConfigManager.Instance.FinalJester);
            bool emerald = LobotomyHelpers.IsChallengeConfigActive(FinalApocalypse.Id, LobotomyConfigManager.Instance.FinalEmerald);
            bool rapture = LobotomyHelpers.IsChallengeConfigActive(FinalApocalypse.Id, LobotomyConfigManager.Instance.FinalComing);

            // if there are random custom bosses, add them to the list
            // otherewise
            if (LobotomyConfigManager.Instance.CustomBosses)
                randomOpponents.AddRange(CustomBosses);
            else if (!apocalypse && !jester && !emerald && !rapture)
                return true;

            if (SaveFile.IsAscension)
            {
                if (AscensionSaveData.Data.ChallengeIsActive(FinalApocalypse.Id))
                    randomOpponents.RemoveAt(0);
            }
            else
            {
                if (LobotomyConfigManager.Instance.FinalApocalypse)
                    randomOpponents.RemoveAt(0);
            }
            return true;
        }
    }
}
*/