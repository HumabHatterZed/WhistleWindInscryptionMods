using DiskCardGame;
using GBC;
using HarmonyLib;
using System.Collections;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class OpponentPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(SceneLoader), nameof(SceneLoader.Load))]
        private static void ResetTriggers()
        {
            PreventOpponentDamage = false;
            if (LobotomySaveManager.OpponentBlessings > 11)
                LobotomySaveManager.OpponentBlessings = 11;

            if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                LobotomyConfigManager.Instance.SetBlessings(11);
        }

        // Reset board effects for event cards and the Clock for WhiteNight
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.OutroSequence))]
        private static IEnumerator ResetEffects(IEnumerator enumerator, Opponent __instance)
        {
            PreventOpponentDamage = false;
            if (LobotomySaveManager.TriggeredWhiteNightThisBattle)
            {
                LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");

                if (SaveManager.SaveFile.IsPart1)
                    LeshyAnimationController.Instance?.ResetEyesTexture();

                if (LobotomySaveManager.OpponentBlessings > 11)
                    LobotomySaveManager.OpponentBlessings = 0;

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(0);

                AchievementAPI.Unlock(AchievementAPI.Blessing);
                LobotomySaveManager.TriggeredWhiteNightThisBattle = false;
            }

            LobotomySaveManager.BoardEffectsApocalypse = false;
            LobotomySaveManager.BoardEffectsEmerald = false;
            LobotomySaveManager.BoardEffectsEntropy = false;

            if (__instance is not PixelOpponent)
                Singleton<TableVisualEffectsManager>.Instance?.ResetTableColors();

            if (LobotomySaveManager.UnlockedApocalypseBird)
                AchievementAPI.Unlock(AchievementAPI.TheThreeBirds);

            if (LobotomySaveManager.UnlockedJesterOfNihil)
                AchievementAPI.Unlock(AchievementAPI.MagicalGirls);

            if (LobotomySaveManager.UnlockedLyingAdult)
                AchievementAPI.Unlock(AchievementAPI.YellowBrickRoad);

            yield return enumerator;
        }
    }
}
