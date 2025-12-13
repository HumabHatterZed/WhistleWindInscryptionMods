using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core;

namespace WhistleWind.AbnormalSigils.Patches {
    [HarmonyPatch]
    internal class InterfacePatches {
        [HarmonyPostfix, HarmonyPatch(typeof(GlobalTriggerHandler), nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
        private static IEnumerator CallPreTurnEnd(IEnumerator result, Trigger trigger, object[] otherArgs) {
            if (trigger == Trigger.TurnEnd) {
                bool playerTurn = (bool)otherArgs[0];
                List<IPreTurnEnd> onTurnEnd = CustomTriggerFinder.FindGlobalTriggers<IPreTurnEnd>(true).ToList();
                foreach (IPreTurnEnd turnEnd in onTurnEnd) {
                    if (turnEnd.RespondsToPreTurnEnd(playerTurn)) {
                        yield return turnEnd.OnPreTurnEnd(playerTurn);
                    }
                }
            }
            yield return result;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.PlayerTurn))]
        private static IEnumerator TriggerOnTurnEndPlayer(IEnumerator result, TurnManager __instance) {
            yield return result;

            List<IPlayerTurnEnd> onTurnEnd = CustomTriggerFinder.FindGlobalTriggers<IPlayerTurnEnd>(true).ToList();
            onTurnEnd.Sort((a, b) => b.PlayerTurnEndPriority() - a.PlayerTurnEndPriority());

            foreach (IPlayerTurnEnd trigger in onTurnEnd) {
                if (trigger.RespondsToPlayerTurnEnd())
                    yield return trigger.OnPlayerTurnEnd();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.OpponentTurn))]
        private static IEnumerator TriggerOnTurnEndOpponent(IEnumerator result, TurnManager __instance) {
            bool skipTurn = __instance.Opponent.SkipNextTurn;
            yield return result;

            List<IOpponentTurnEnd> onOpponentTurnEnd = CustomTriggerFinder.FindGlobalTriggers<IOpponentTurnEnd>(true).ToList();
            onOpponentTurnEnd.Sort((a, b) => b.OpponentTurnEndPriority(skipTurn) - a.OpponentTurnEndPriority(skipTurn));

            foreach (IOpponentTurnEnd trigger in onOpponentTurnEnd) {
                if (trigger.RespondsToOpponentTurnEnd(skipTurn))
                    yield return trigger.OnOpponentTurnEnd(skipTurn);
            }
        }
    }
}
