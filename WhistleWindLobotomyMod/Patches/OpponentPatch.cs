using DiskCardGame;
using GBC;
using HarmonyLib;
using System.Collections;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(Opponent))]
    public class OpponentPatch
    {
        // Reset board effects for event cards and the Clock for WhiteNight
        [HarmonyPostfix, HarmonyPatch(nameof(Opponent.OutroSequence))]
        public static IEnumerator ResetEffects(IEnumerator enumerator, Opponent __instance)
        {
            if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
            {
                if (LobotomySaveManager.TriggeredWhiteNightThisRun)
                {
                    LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");
                    LobotomyConfigManager.Instance.SetBlessings(0);
                    LeshyAnimationController.Instance.ResetEyesTexture();
                }
                else
                    LobotomyConfigManager.Instance.SetBlessings(11);
            }

            LobotomySaveManager.BoardEffectsApocalypse = false;
            LobotomySaveManager.BoardEffectsEmerald = false;
            LobotomySaveManager.BoardEffectsEntropy = false;

            if (__instance is not PixelOpponent)
                Singleton<TableVisualEffectsManager>.Instance?.ResetTableColors();

            yield return enumerator;
        }
    }
}
