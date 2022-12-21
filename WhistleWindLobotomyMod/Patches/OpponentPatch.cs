using DiskCardGame;
using HarmonyLib;
using System.Collections;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(Opponent))]
    public class OpponentPatch
    {
        // Reset board effects for event cards
        // and the Clock for WhiteNight
        [HarmonyPostfix, HarmonyPatch(nameof(Opponent.OutroSequence))]
        public static IEnumerator ResetEffects(IEnumerator enumerator)
        {
            if (ConfigManager.Instance.NumOfBlessings > 11)
            {
                if (WstlSaveManager.TriggeredWhiteNightThisRun)
                {
                    LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");
                    ConfigManager.Instance.SetBlessings(0);
                    LeshyAnimationController.Instance.ResetEyesTexture();
                }
                else
                    ConfigManager.Instance.SetBlessings(11);
            }
            if (WstlSaveManager.BoardEffectsApocalypse)
            {
                WstlSaveManager.BoardEffectsApocalypse = false;
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
            }
            if (WstlSaveManager.BoardEffectsEntropy)
            {
                WstlSaveManager.BoardEffectsEntropy = false;
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
            }
            if (WstlSaveManager.BoardEffectsEntropy)
            {
                WstlSaveManager.BoardEffectsEntropy = false;
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
            }

            yield return enumerator;
        }
    }
}
