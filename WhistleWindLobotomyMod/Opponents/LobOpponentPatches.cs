using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch]
    internal static class LobOpponentPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(ConsumableItem), nameof(ConsumableItem.CanActivate))]
        private static void PreventHourglassItem(ConsumableItem __instance, ref bool __result) {
            if (!__result)
                return;

            if (__instance is HourglassItem) {
                if (LobOpponentUtils.IsCustomBoss(out ApocalypseBossOpponent boss) && !boss.BattleSequencer.DisabledEggEffects.Contains(ActiveEggEffect.LongArms))
                    __result = false;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Part1GameFlowManager), nameof(Part1GameFlowManager.KillPlayerSequence))]
        private static IEnumerator CustomKillPlayerSequences(IEnumerator enumerator) {
            if (TurnManager.Instance.Opponent is IKillPlayerSequence killSeq && killSeq != null && killSeq.RespondsToKillPlayerSequence()) {
                Singleton<PlayerHand>.Instance.SetShown(shown: false);
                AudioSource reachSound = AudioController.Instance.PlaySound2D("eyes_opening", MixerGroup.TableObjectsSFX, 0.75f);

                yield return new WaitForSeconds(0.5f);
                yield return killSeq.KillPlayerSequence();
                yield return new WaitForSeconds(2.75f);

                Singleton<TextDisplayer>.Instance.Clear();
                Singleton<InteractionCursor>.Instance.SetHidden(hidden: true);
                GameObject.DontDestroyOnLoad(AudioController.Instance.PlaySound2D("candle_loseLife", MixerGroup.TableObjectsSFX).gameObject);
                GameObject.Destroy(reachSound.gameObject);
                AudioController.Instance.StopAllLoops();
                yield return new WaitForSeconds(0.15f);
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearBlack);
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
                yield return new WaitForSeconds(2f);
                if (SaveFile.IsAscension) {
                    AscensionMenuScreens.ReturningFromFailedRun = true;
                    AscensionStatsData.TryIncrementStat(AscensionStat.Type.Losses);
                    SaveManager.SaveToFile();
                    SceneLoader.Load("Ascension_Configure");
                }
                else {
                    SceneLoader.Load("Part1_Sanctum");
                }
            }
            else {
                yield return enumerator;
            }
        }

        /// <summary>
        /// During custom boss fights, give the player a card to refresh their decks.
        /// Allows for the fight to continue without dealing with Starvation.
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardDrawPiles), nameof(CardDrawPiles.DrawCardFromDeck))]
        [HarmonyPatch(typeof(CardDrawPiles3D), nameof(CardDrawPiles3D.DrawFromSidePile))]
        private static IEnumerator RefreshDeckBeforeExhaustion(IEnumerator enumerator, CardDrawPiles __instance) {
            yield return enumerator;
            if (!LobOpponentUtils.FightingCustomBoss())
                yield break;

            if (__instance.Exhausted && !PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == "wstl_REFRESH_DECKS")) {
                yield return new WaitForSeconds(0.4f);
                ViewManager.Instance.SwitchToView(View.Hand);
                yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_REFRESH_DECKS"));
                yield return new WaitForSeconds(0.4f);
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossExhausted", TextDisplayer.MessageAdvanceMode.Input);
            }
        }

        /// <summary>
        /// Prevents the camera from panning to the scales if direct damage has been modified to be 0.
        /// </summary>
        [HarmonyPrefix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowDamageSequence))]
        private static bool DontChangeViewOnZeroDamage(int damage, int numWeights, bool toPlayer, ref bool changeView) {
            if (!toPlayer && TurnManager.Instance?.SpecialSequencer is LobotomyBattleSequencer seq
                    && seq.HighestPositiveScaleBalance == LifeManager.Instance.Balance) {
                changeView = false;
            }

            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowResetSequence))]
        private static IEnumerator CustomOpponentsDontResetScales(IEnumerator enumerator) {
            if (LobOpponentUtils.FightingCustomBoss())
                yield break;

            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static void RemoveExcessTeethOnBoard(CardSlot attackingSlot, CardSlot targetSlot, ref int damage) {
            if (targetSlot.IsPlayerSlot == attackingSlot.IsPlayerSlot || damage < 0) {
                if (TurnManager.Instance?.SpecialSequencer is LobotomyBattleSequencer seq) {
                    int maxNumOfTeeth = seq.HighestPositiveScaleBalance - LifeManager.Instance.Balance;
                    int bonesToGive = Mathf.Min(seq.MaxExcessBones - seq.currentExcessBones, damage) - maxNumOfTeeth;
                    if (bonesToGive > 0) {
                        maxNumOfTeeth -= bonesToGive;
                    }
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static IEnumerator FixGiantCardAnimation(IEnumerator enumerator, CombatPhaseManager3D __instance, CardSlot attackingSlot, CardSlot targetSlot, int damage) {
            if (attackingSlot.Card.LacksTrait(Trait.Giant) || !LobOpponentUtils.FightingCustomBoss()) {
                yield return enumerator;
                yield break;
            }

            List<Transform> newWeights = new();
            for (int i = 0; i < Mathf.Min(20, damage); i++) {
                GameObject gameObject = GameObject.Instantiate(__instance.weightPrefab);
                Vector3 vector = new(0f, 0f, attackingSlot.IsPlayerSlot ? 0.75f : (-0.75f));
                gameObject.transform.position = targetSlot.transform.position + vector + new Vector3(i * 0.1f, 0f, i * 0.1f);
                gameObject.transform.eulerAngles = UnityEngine.Random.insideUnitSphere;
                newWeights.Add(gameObject.transform);
            }
            __instance.damageWeights.AddRange(newWeights);
            // the giant card animation breaks if it's not targeting the firstmost slot, so we use the firstmost instead of the actual target slot
            attackingSlot.Card.Anim.PlayAttackAnimation(attackPlayer: true, BoardManager.Instance.PlayerSlotsCopy[0], delegate {
                Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * Mathf.Min(10, damage));
                foreach (Transform item in newWeights) {
                    if (item != null) {
                        item.gameObject.SetActive(value: true);
                        item.GetComponent<Rigidbody>().AddForce(Vector3.up * 4f, ForceMode.VelocityChange);
                    }
                }
            });
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.CurrentMapRegion), MethodType.Getter)]
        private static void ReplaceFinalWithCustomBossRegion(ref RegionData __result) {
            if (RunState.Run.regionTier == RegionProgression.Instance.regions.Count - 1) {
                if (LobotomyConfigManager.ChallengeIsActive(FinalApocalypse.Id)) {
                    __result = LobOpponentUtils.apocalypseRegion;
                }
            }
        }
    }
}
