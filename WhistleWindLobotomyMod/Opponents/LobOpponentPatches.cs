using DiskCardGame;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch]
    internal static class LobOpponentPatches {
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

            if (__instance is not CardDrawPiles3D pile || (pile.Deck.CardsInDeck + pile.SideDeck.CardsInDeck) > 1) {
                yield break;
            }

            LobotomyPlugin.Log.LogDebug("[RefreshDeck] check for exhaustion");
            if (!PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == "wstl_REFRESH_DECKS")) {
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
            if (LobOpponentUtils.FightingCustomOpponent())
                yield break;

            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.CurrentMapRegion), MethodType.Getter)]
        private static void ReplaceFinalWithCustomBossRegion(ref RegionData __result) {
            if (RunState.CurrentRegionTier == RegionProgression.Instance.regions.Count - 1) {
                if (LobotomyConfigManager.ChallengeIsActive(FinalApocalypse.Id)) {
                    __result = LobOpponentUtils.apocalypseRegion;
                }
                else if (LobotomyConfigManager.ChallengeIsActive(FinalOrdeal.Id)) {
                    __result = LobOpponentUtils.whiteOrdealRegion;
                }
            }
        }
    }
}
