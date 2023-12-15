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
    internal static class CustomBossPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardDrawPiles), nameof(CardDrawPiles.DrawCardFromDeck))]
        [HarmonyPatch(typeof(CardDrawPiles3D), nameof(CardDrawPiles3D.DrawFromSidePile))]
        private static IEnumerator RefreshDecksForCustomBosses(IEnumerator enumerator, CardDrawPiles __instance)
        {
            yield return enumerator;
            if (!CustomBossUtils.FightingCustomBoss())
                yield break;

            if (__instance.Exhausted && !PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == "wstl_REFRESH_DECKS"))
            {
                CardInfo refreshDecks = CardLoader.GetCardByName("wstl_REFRESH_DECKS");

                yield return new WaitForSeconds(0.4f);
                ViewManager.Instance.SwitchToView(View.Hand);
                yield return CardSpawner.Instance.SpawnCardToHand(refreshDecks);
                yield return new WaitForSeconds(0.4f);
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossExhausted", TextDisplayer.MessageAdvanceMode.Input);
            }

        }
        [HarmonyPostfix, HarmonyPatch(typeof(Part1GameFlowManager), nameof(Part1GameFlowManager.KillPlayerSequence))]
        private static IEnumerator CustomKillPlayerSequences(IEnumerator enumerator)
        {
            if (!CustomBossUtils.FightingCustomBoss())
            {
                yield return enumerator;
                yield break;
            }
            Singleton<PlayerHand>.Instance.SetShown(shown: false);
            AudioSource reachSound = AudioController.Instance.PlaySound2D("eyes_opening", MixerGroup.TableObjectsSFX, 0.75f);
            yield return new WaitForSeconds(0.5f);
            if (CustomBossUtils.IsCustomBoss<ApocalypseBossOpponent>())
                ApocalypseBossUtils.KillPlayerSequence();

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

        [HarmonyPostfix, HarmonyPatch(typeof(CardDrawPiles), nameof(CardDrawPiles.ExhaustedSequence))]
        private static IEnumerator GiantCardsGainStarvation(IEnumerator enumerator, CardDrawPiles __instance)
        {
            if (!CustomBossUtils.FightingCustomBoss() || TurnManager.Instance.Opponent.NumLives > 1)
            {
                yield return enumerator;
                yield break;
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.CardPiles, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(1f);

            CardSlot giantCardSlot = BoardManager.Instance.OpponentSlotsCopy.Find(x => x.Card != null && x.Card.HasTrait(Trait.Giant));
            if (giantCardSlot != null)
            {
                if (CustomBossUtils.IsCustomBoss<ApocalypseBossOpponent>())
                    yield return ApocalypseBossUtils.ExhaustedSequence(__instance, giantCardSlot);
                // other bosses
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowResetSequence))]
        private static IEnumerator DontResetScales(IEnumerator enumerator)
        {
            if (CustomBossUtils.FightingCustomBoss())
                yield break;

            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static IEnumerator FixGiantCardAnimation(IEnumerator enumerator, CombatPhaseManager3D __instance, CardSlot attackingSlot, CardSlot targetSlot, int damage)
        {
            if (!CustomBossUtils.FightingCustomBoss() || !CustomBossUtils.IsCustomBoss<ApocalypseBossOpponent>() || attackingSlot.Card.LacksTrait(Trait.Giant))
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

        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static IEnumerator GainBonesFromDirectDamage(IEnumerator enumerator, CardSlot attackingSlot, CardSlot targetSlot, int damage)
        {
            if (!CustomBossUtils.FightingCustomBoss() || !attackingSlot.IsPlayerSlot)
            {
                yield return enumerator;
                yield break;
            }

            bool doScaleDamage = LifeManager.Instance.DamageUntilPlayerWin > 1;
            int bonesToGive = damage - (LifeManager.Instance.DamageUntilPlayerWin - 1);

            // deal scale damage
            if (doScaleDamage)
                yield return enumerator;

            // if there's no excess scale damage, break
            if (bonesToGive <= 0)
                yield break;

            bonesToGive = Mathf.Min(3, bonesToGive);

            if (doScaleDamage)
                DigUpBones(bonesToGive, targetSlot);
            else
            {
                attackingSlot.Card.Anim.PlayAttackAnimation(attackPlayer: true, targetSlot, delegate
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

        [HarmonyPostfix, HarmonyPatch(typeof(BleachPotItem), nameof(BleachPotItem.GetValidOpponentSlots))]
        private static void CannotBleachCustomBosses(ref List<CardSlot> __result)
        {
            if (!CustomBossUtils.FightingCustomBoss())
                return;

            __result.RemoveAll(x => x.Card.HasTrait(Trait.Uncuttable));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.CurrentMapRegion), MethodType.Getter)]
        private static void ReplaceFinalWithCustomBossRegion(ref RegionData __result)
        {
            if (RunState.Run.regionTier == RegionProgression.Instance.regions.Count - 1)
            {
                if (SaveFile.IsAscension)
                {
                    if (AscensionSaveData.Data.ChallengeIsActive(FinalApocalypse.Id))
                        __result = CustomBossUtils.apocalypseRegion;
                    /*                else if (AscensionSaveData.Data.ChallengeIsActive(FinalComing.Id))
                                        __result = CustomBossUtils.saviourRegion;
                                    else if (AscensionSaveData.Data.ChallengeIsActive(FinalTrick.Id))
                                        __result = CustomBossUtils.adultRegion;
                                    else if (AscensionSaveData.Data.ChallengeIsActive(FinalJester.Id))
                                        __result = CustomBossUtils.jesterRegion;*/
                }
                else if (LobotomyConfigManager.Instance.FinalApocalypse)
                {
                    __result = CustomBossUtils.apocalypseRegion;
                }
            }
        }
    }
}
