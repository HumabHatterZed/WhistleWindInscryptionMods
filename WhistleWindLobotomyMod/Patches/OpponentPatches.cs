using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.SpecialSequencers;

namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch]
    internal class OpponentPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.QueueCard))]
        private static IEnumerator TransformCowardlyCat(IEnumerator result, Opponent __instance, CardInfo cardInfo, CardSlot slot) {
            yield return result;
            if (cardInfo.HasSpecialAbility(Cowardly.specialAbility)) {
                PlayableCard card = __instance.Queue.Find(x => x.QueuedSlot == slot);
                yield return Cowardly.CheckTransform(card);
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Part1BossOpponent), nameof(Part1BossOpponent.GrizzlyGlitchSequence))]
        private static bool ReplaceGrizzlySequence(ref IEnumerator __result, Part1BossOpponent __instance) {
            bool apostles = LobotomyConfigManager.ChallengeIsActive(ApostleGrizzlies.Id);
            if (apostles || LobotomyConfigManager.ChallengeIsActive(AbnormalBosses.Id)) {
                __result = AbnormalGrizzlySequence.ApostleGlitchSequence(__instance, apostles);
            }
            return false;
        }

        // fixes Trapper-Trader boss fight not using all lobotomy cards
        [HarmonyPrefix, HarmonyPatch(typeof(TradeCardsForPelts), nameof(TradeCardsForPelts.GenerateTradeCardsWithCostTier))]
        private static bool FixTrapperTrapperBoss(int numCards, int tier, int randomSeed, ref List<CardInfo> __result) {
            bool flag = tier > 0;
            int tier2 = Mathf.Max(1, tier);
            List<CardInfo> learnedCards = CardLoader.LearnedCards;
            learnedCards.RemoveAll(x => (!LobotomyCardManager.ObtainableLobotomyCards.Contains(x) && x.temple != 0) || x.CostTier != tier2 || x.Abilities.Exists(a => !AbilitiesUtil.GetInfo(a).opponentUsable));
            if (!ProgressionData.LearnedMechanic(MechanicsConcept.Bones))
                learnedCards.RemoveAll((CardInfo x) => x.BonesCost > 0);

            List<CardInfo> distinctCardsFromPool = CardLoader.GetDistinctCardsFromPool(randomSeed, numCards, learnedCards, flag ? 1 : 0, true);
            while (distinctCardsFromPool.Count < numCards) {
                CardInfo cardByName;
                CardModificationInfo cardModificationInfo;
                if (tier2 == 2) {
                    cardByName = CardLoader.GetCardByName("Snapper");
                    cardModificationInfo = new CardModificationInfo(Ability.Sharp);
                }
                else {
                    cardByName = CardLoader.GetCardByName("Grizzly");
                    cardModificationInfo = new CardModificationInfo(Ability.Reach);
                }
                if (flag) {
                    cardModificationInfo.fromCardMerge = true;
                    cardByName.Mods.Add(cardModificationInfo);
                }
                distinctCardsFromPool.Add(cardByName);
            }
            __result = distinctCardsFromPool;
            return false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.CreateCard))]
        private static void UpdatePlagueDoctorAppearance(PlayableCard __result) {
            if (__result != null && __result.HasSpecialAbility(Bless.specialAbility))
                __result.UpdateAppearanceBehaviours();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SceneLoader), nameof(SceneLoader.Load))]
        private static void ResetTriggersOnSceneLoad() {
            if (LobotomySaveManager.OpponentBlessings > 11)
                LobotomySaveManager.OpponentBlessings = 11;

            if (LobotomyConfigManager.NumOfBlessings > 11)
                LobotomyConfigManager.SetBlessings(11);
        }

        // Reset board effects for event cards and the Clock for WhiteNight
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.CleanupPhase))]
        private static IEnumerator ResetEffectsOnCleanup(IEnumerator enumerator, TurnManager __instance) {
            if (LobotomySaveManager.TriggeredWhiteNightThisBattle) {
                LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");

                if (SaveManager.SaveFile.IsPart1)
                    LeshyAnimationController.Instance?.ResetEyesTexture();

                LobotomySaveManager.OpponentBlessings = 0;
                LobotomyConfigManager.SetBlessings(0);

                AchievementAPI.Unlock(true, AchievementAPI.Blessing);
                LobotomySaveManager.TriggeredWhiteNightThisBattle = false;
            }

            LobotomySaveManager.BoardEffectsApocalypse = false;
            LobotomySaveManager.BoardEffectsEmerald = false;
            LobotomySaveManager.BoardEffectsEntropy = false;

            if (TableVisualEffectsManager.Instance != null)
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();

            AchievementAPI.Unlock(LobotomySaveManager.UnlockedApocalypseBird, AchievementAPI.TheThreeBirds);
            AchievementAPI.Unlock(LobotomySaveManager.UnlockedJesterOfNihil, AchievementAPI.MagicalGirls);
            AchievementAPI.Unlock(LobotomySaveManager.UnlockedLyingAdult, AchievementAPI.YellowBrickRoad);

            yield return enumerator;
        }
    }
}
