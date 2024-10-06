using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal static class CustomOpponentPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardDrawPiles), nameof(CardDrawPiles.DrawCardFromDeck))]
        [HarmonyPatch(typeof(CardDrawPiles3D), nameof(CardDrawPiles3D.DrawFromSidePile))]
        private static IEnumerator RefreshDeckBeforeExhaustion(IEnumerator enumerator, CardDrawPiles __instance)
        {
            yield return enumerator;
            if (!CustomOpponentUtils.FightingCustomOpponent(true))
                yield break;

            if (__instance.Exhausted && !PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == "wstl_REFRESH_DECKS"))
            {
                yield return new WaitForSeconds(0.4f);
                ViewManager.Instance.SwitchToView(View.Hand);
                yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_REFRESH_DECKS"));
                yield return new WaitForSeconds(0.4f);
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossExhausted", TextDisplayer.MessageAdvanceMode.Input);
            }

        }

        [HarmonyPrefix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowDamageSequence))]
        private static bool DontChangeViewOnZeroDamage(int damage, int numWeights, ref bool changeView)
        {
            if (CustomOpponentUtils.FightingCustomOpponent(false) && TurnManager.Instance.SpecialSequencer is LobotomyBattleSequencer seq && seq != null)
            {
                if (LifeManager.Instance.DamageUntilPlayerWin == 1 || (damage >= LifeManager.Instance.DamageUntilPlayerWin && Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, numWeights) == 0))
                {
                    changeView = false;
                }
            }
            
            return true;
        }

        #region Custom boss sequences
        [HarmonyPostfix, HarmonyPatch(typeof(Part1GameFlowManager), nameof(Part1GameFlowManager.KillPlayerSequence))]
        private static IEnumerator CustomKillPlayerSequences(IEnumerator enumerator)
        {
            if (TurnManager.Instance.Opponent is IKillPlayerSequence killSeq && killSeq != null && killSeq.RespondsToKillPlayerSequence())
            {
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
                if (SaveFile.IsAscension)
                {
                    AscensionMenuScreens.ReturningFromFailedRun = true;
                    AscensionStatsData.TryIncrementStat(AscensionStat.Type.Losses);
                    SaveManager.SaveToFile();
                    SceneLoader.Load("Ascension_Configure");
                }
                else
                {
                    SceneLoader.Load("Part1_Sanctum");
                }
            }
            else
            {
                yield return enumerator;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardDrawPiles), nameof(CardDrawPiles.ExhaustedSequence))]
        private static IEnumerator CustomBossExhaustionSequence(IEnumerator enumerator, CardDrawPiles __instance)
        {
            if (TurnManager.Instance.Opponent is IExhaustSequence exhaustSeq && exhaustSeq != null)
            {
                CardSlot giantCardSlot = BoardManager.Instance.OpponentSlotsCopy.Find(x => x.Card != null && x.Card.HasTrait(Trait.Giant));
                if (exhaustSeq.RespondsToExhaustSequence(__instance, giantCardSlot.Card))
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.CardPiles, immediate: false, lockAfter: true);
                    yield return new WaitForSeconds(1f);
                    yield return exhaustSeq.ExhaustSequence(__instance, giantCardSlot.Card);
                    yield break;
                }
            }
            yield return enumerator;
        }
        #endregion

        [HarmonyPostfix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowResetSequence))]
        private static IEnumerator CustomOpponentsDontResetScales(IEnumerator enumerator)
        {
            if (CustomOpponentUtils.FightingCustomOpponent(false))
                yield break;

            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static IEnumerator FixGiantCardAnimation(IEnumerator enumerator, CombatPhaseManager3D __instance, CardSlot attackingSlot, CardSlot targetSlot, int damage)
        {
            if (!CustomOpponentUtils.FightingCustomOpponent(true) || !CustomOpponentUtils.IsCustomBoss<ApocalypseBossOpponent>() || attackingSlot.Card.LacksTrait(Trait.Giant))
            {
                yield return enumerator;
                yield break;
            }
            List<Transform> newWeights = new();
            for (int i = 0; i < Mathf.Min(20, damage); i++)
            {
                GameObject gameObject = GameObject.Instantiate(__instance.weightPrefab);
                Vector3 vector = new(0f, 0f, attackingSlot.IsPlayerSlot ? 0.75f : (-0.75f));
                gameObject.transform.position = targetSlot.transform.position + vector + new Vector3((float)i * 0.1f, 0f, (float)i * 0.1f);
                gameObject.transform.eulerAngles = UnityEngine.Random.insideUnitSphere;
                newWeights.Add(gameObject.transform);
            }
            __instance.damageWeights.AddRange(newWeights);
            // the giant card animation breaks if it's not targeting the firstmost slot, so we use the firstmost instead of the actual target slot
            attackingSlot.Card.Anim.PlayAttackAnimation(attackPlayer: true, BoardManager.Instance.PlayerSlotsCopy[0], delegate
            {
                Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * Mathf.Min(10, damage));
                foreach (Transform item in newWeights)
                {
                    if (item != null)
                    {
                        item.gameObject.SetActive(value: true);
                        item.GetComponent<Rigidbody>().AddForce(Vector3.up * 4f, ForceMode.VelocityChange);
                    }
                }
            });
        }

        #region Excess Bones
        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static IEnumerator GiveBonesFromExcessScaleDamage(IEnumerator enumerator, CardSlot attackingSlot, CardSlot targetSlot, int damage)
        {
            if (targetSlot.IsPlayerSlot || TurnManager.Instance.SpecialSequencer is not LobotomyBattleSequencer seq || seq == null || !seq.DirectDamageGivesBones)
            {
                yield return enumerator;
                yield break;
            }

            bool doScaleDamage = LifeManager.Instance.DamageUntilPlayerWin > 1;
            if (doScaleDamage) yield return enumerator;

            // if there's no excess scale damage or we've reched the max allowed amount of excess bones
            int bonesToGive = Mathf.Min(2, (damage - (LifeManager.Instance.DamageUntilPlayerWin - 1) + 1) / 2);
            if (bonesToGive <= 0 || (bonesToGive + seq.currentExcessBones) >= seq.MaxExcessBones)
                yield break;

            if (doScaleDamage)
            {
                DigUpBones(bonesToGive, targetSlot);
            }
            else // if we didn't perform the regular sequence
            {
                attackingSlot.Card.Anim.PlayAttackAnimation(attackPlayer: attackingSlot.IsPlayerSlot, targetSlot, delegate
                {
                    DigUpBones(bonesToGive, targetSlot);
                });
            }

            if (!DialogueEventsData.EventIsPlayed("ApocalypseBossBoneGain"))
            {
                yield return new WaitForSeconds(0.5f);
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBoneGain", TextDisplayer.MessageAdvanceMode.Input);
            }
        }

        private static void DigUpBones(int bonesToGive, CardSlot targetSlot)
        {
            ResourcesManager.Instance.PlayerBones += bonesToGive;
            Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * (float)Mathf.Min(10, bonesToGive));

            for (int i = 0; i < bonesToGive; i++)
            {
                Part1ResourcesManager manager = ResourcesManager.Instance as Part1ResourcesManager;
                GameObject gameObject = GameObject.Instantiate(manager.boneTokenPrefab);
                BoneTokenInteractable component = gameObject.GetComponent<BoneTokenInteractable>();
                Rigidbody tokenRB = gameObject.GetComponent<Rigidbody>();
                Vector3 vector = new(0f, 0f, 0.75f);

                tokenRB.Sleep();
                gameObject.transform.position = targetSlot.transform.position + vector + new Vector3(i * 0.1f, 0f, i * 0.1f);
                gameObject.transform.eulerAngles = UnityEngine.Random.insideUnitSphere;

                Vector3 endValue = manager.GetRandomLandingPosition() + Vector3.up;
                Tween.Position(component.transform, endValue, 0.25f, 0.5f, Tween.EaseInOut, Tween.LoopType.None, null, delegate
                {
                    tokenRB.WakeUp();
                    manager.PushTokenDown(tokenRB);
                });

                manager.boneTokens.Add(component);
                manager.isOrganized = false;
            }
        }
        #endregion

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.CurrentMapRegion), MethodType.Getter)]
        private static void ReplaceFinalWithCustomBossRegion(ref RegionData __result)
        {
            if (RunState.Run.regionTier == RegionProgression.Instance.regions.Count - 1)
            {
                if (SaveFile.IsAscension)
                {
                    if (AscensionSaveData.Data.ChallengeIsActive(FinalApocalypse.Id))
                        __result = CustomOpponentUtils.apocalypseRegion;
                    /*                else if (AscensionSaveData.Data.ChallengeIsActive(FinalComing.Id))
                                        __result = CustomBossUtils.saviourRegion;
                                    else if (AscensionSaveData.Data.ChallengeIsActive(FinalTrick.Id))
                                        __result = CustomBossUtils.adultRegion;
                                    else if (AscensionSaveData.Data.ChallengeIsActive(FinalJester.Id))
                                        __result = CustomBossUtils.jesterRegion;*/
                }
                else if (LobotomyConfigManager.Instance.FinalApocalypse)
                {
                    __result = CustomOpponentUtils.apocalypseRegion;
                }
            }
        }
    }
}
