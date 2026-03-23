using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents.Angler;
using WhistleWindLobotomyMod.Opponents.Leshy;
using WhistleWindLobotomyMod.Opponents.PirateSkull;
using WhistleWindLobotomyMod.Opponents.Prospector;
using WhistleWindLobotomyMod.Opponents.TrapperTrader;

namespace WhistleWindLobotomyMod.Challenges {
    public static class AbnormalBosses // taken from infiniscryption
    {
        internal const string TITLE = "Abnormal Bosses";
        internal const string DESCRIPTION = "Boss battles will only use Abnormality cards.";

        public static AscensionChallenge ID { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }
        internal static void Register(Harmony harmony) {
            Info = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                15,
                TextureLoader.LoadTextureFromFile("ascensionAbnormalBosses.png"),
                TextureLoader.LoadTextureFromFile("ascensionAbnormalBosses_activated.png")
                );

            ID = Info.Challenge.challengeType;

            harmony.PatchAll(typeof(AbnormalBosses));
        }
        // Array of valid opponent types to replace
        private static readonly Opponent.Type[] SUPPORTED_OPPONENTS = new Opponent.Type[] {
            Opponent.Type.ProspectorBoss,
            Opponent.Type.AnglerBoss,
            Opponent.Type.TrapperTraderBoss,
            Opponent.Type.LeshyBoss,
            Opponent.Type.PirateSkullBoss
        };

        private static readonly string[] OPPONENT_IDS = new string[] {
            BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.ProspectorBoss),
            BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.AnglerBoss),
            BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.TrapperTraderBoss),
            BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.LeshyBoss),
            BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.PirateSkullBoss)
        };

        // Replaces boss encounters with custom ones
        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPrefix]
        private static bool ReplaceBossEncounter(EncounterData encounterData, ref Opponent __result) {
            // breaks if challenge is not active or if opponent is not supported
            if (!LobotomyConfigManager.ChallengeIsActive(ID) || !SUPPORTED_OPPONENTS.Contains(encounterData.opponentType))
                return true;

            GameObject gameObject = new() {
                name = "Opponent"
            };
            Opponent.Type opponentType = encounterData.opponentType;
            LobotomyPlugin.Log.LogDebug($"Replacing opponent: {opponentType}");
            Opponent opponent = opponentType switch {
                Opponent.Type.ProspectorBoss => gameObject.AddComponent<ProspectorAbnormalBossOpponent>(),
                Opponent.Type.AnglerBoss => gameObject.AddComponent<AnglerAbnormalBossOpponent>(),
                Opponent.Type.TrapperTraderBoss => gameObject.AddComponent<TrapperTraderAbnormalBossOpponent>(),
                Opponent.Type.LeshyBoss => gameObject.AddComponent<LeshyAbnormalBossOpponent>(),
                Opponent.Type.PirateSkullBoss => gameObject.AddComponent<PirateSkullAbnormalBossOpponent>(),
                _ => null
            };
            if (opponent == null)
                return true;

            string text = encounterData.aiId;
            if (string.IsNullOrEmpty(text)) {
                text = "AI";
            }
            opponent.AI = Activator.CreateInstance(CustomType.GetType("DiskCardGame", text)) as AI;
            opponent.NumLives = opponent.StartingLives;
            opponent.OpponentType = opponentType;
            opponent.TurnPlan = opponent.ModifyTurnPlan(encounterData.opponentTurnPlan);
            opponent.Blueprint = encounterData.Blueprint;
            opponent.Difficulty = encounterData.Difficulty;
            opponent.ExtraTurnsToSurrender = SeededRandom.Range(0, 3, SaveManager.SaveFile.GetCurrentRandomSeed());
            __result = opponent;
            return false;
        }

        private static void AddBossSequencer<T>(TurnManager manager) where T : SpecialBattleSequencer {
            UnityEngine.Object.Destroy(manager.SpecialSequencer);
            SpecialBattleSequencer sequencer = manager.gameObject.AddComponent<T>();
            Traverse trav = Traverse.Create(manager);
            trav.Property("SpecialSequencer").SetValue(sequencer);
        }

        // Replaces special sequencers with custom ones
        [HarmonyPatch(typeof(TurnManager), nameof(TurnManager.UpdateSpecialSequencer))]
        [HarmonyPrefix]
        private static bool ReplaceSequencers(string specialBattleId, ref TurnManager __instance) {
            if (!LobotomyConfigManager.ChallengeIsActive(ID) || !OPPONENT_IDS.Contains(specialBattleId))
                return true;

            LobotomyPlugin.Log.LogDebug($"Replacing special ID: {specialBattleId}");
            if (specialBattleId == BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.ProspectorBoss)) {
                ChallengeActivationUI.TryShowActivation(ID);
                AddBossSequencer<ProspectorAbnormalBattleSequencer>(__instance);
                return false;
            }
            if (specialBattleId == BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.AnglerBoss)) {
                ChallengeActivationUI.TryShowActivation(ID);
                AddBossSequencer<AnglerAbnormalBattleSequencer>(__instance);
                return false;
            }
            if (specialBattleId == BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.TrapperTraderBoss)) {
                ChallengeActivationUI.TryShowActivation(ID);
                AddBossSequencer<TrapperTraderAbnormalBattleSequencer>(__instance);
                return false;
            }
            if (specialBattleId == BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.LeshyBoss)) {
                ChallengeActivationUI.TryShowActivation(ID);
                AddBossSequencer<LeshyAbnormalBattleSequencer>(__instance);
                return false;
            }
            if (specialBattleId == BossBattleSequencer.GetSequencerIdForBoss(Opponent.Type.PirateSkullBoss)) {
                ChallengeActivationUI.TryShowActivation(ID);
                AddBossSequencer<PirateSkullAbnormalBattleSequencer>(__instance);
                return false;
            }
            return true;
        }
        // Replaces special sequencers with custom ones
        [HarmonyPatch(typeof(GiantShip), nameof(GiantShip.MutinySequence))]
        [HarmonyPostfix]
        private static IEnumerator ReplaceSequencers(IEnumerator enumerator, GiantShip __instance) {
            // if this challenge and Final Boss are active at once
            if (AscensionSaveData.Data.ChallengeIsActive(ID) && AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.FinalBoss)) {
                int numSkeles = (__instance.nextHealthThreshold - __instance.PlayableCard.Health) / 5 + 1;
                for (int i = 0; i < numSkeles; i++) {
                    List<CardSlot> validSlots = Singleton<BoardManager>.Instance.PlayerSlotsCopy;
                    validSlots.RemoveAll((x) => x.Card != null);
                    if (validSlots.Count > 0) {
                        Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue, immediate: false, lockAfter: true);
                        yield return new WaitForSeconds(0.5f);
                        Singleton<CardRenderCamera>.Instance.GetLiveRenderCamera(__instance.Card.StatsLayer as RenderLiveStatsLayer).GetComponentInChildren<PirateShipAnimatedPortrait>().NextSkeletonJumpOverboard();
                        yield return new WaitForSeconds(1f);
                        Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                        CardSlot slot = validSlots[UnityEngine.Random.Range(0, validSlots.Count)];
                        yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.skeletonShrimp), slot);
                        yield return new WaitForSeconds(0.2f);
                        __instance.skelesSpawned++;
                    }
                }
                if (__instance.mutineesSinceDialogue > 1) {
                    yield return new WaitForSeconds(0.3f);
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("PirateSkullShipMutinee", TextDisplayer.MessageAdvanceMode.Input);
                    __instance.mutineesSinceDialogue = 0;
                }
                __instance.mutineesSinceDialogue++;
                yield break;
            }
            yield return enumerator;
        }
    }
}
