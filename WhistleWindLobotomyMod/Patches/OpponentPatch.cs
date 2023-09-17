using DiskCardGame;
using GBC;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class OpponentPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(Part1Opponent), nameof(Part1Opponent.ModifyQueuedCard))]
        private static void MarkOpponentDoctor(PlayableCard card)
        {
            // marks the card as using the opponent's blessings count
            if (card.Info.name == "wstl_plagueDoctor")
                card.Info.Mods.Add(new() { singletonId = PlagueDoctorHelpers.ModSingletonId });
        }

        // Reset board effects for event cards and the Clock for WhiteNight
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.OutroSequence))]
        private static IEnumerator ResetEffects(IEnumerator enumerator, Opponent __instance)
        {
            if (LobotomySaveManager.TriggeredWhiteNightThisBattle)
            {
                LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");

                if (SaveManager.SaveFile.IsPart1)
                    LeshyAnimationController.Instance.ResetEyesTexture();
                else if (SaveManager.SaveFile.IsPart3)
                    P03AnimationController.Instance.SwitchToFace(P03AnimationController.Face.Default);

                if (LobotomySaveManager.OpponentBlessings > 11)
                    LobotomySaveManager.OpponentBlessings = 0;

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(0);

                LobotomySaveManager.TriggeredWhiteNightThisBattle = false;
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
