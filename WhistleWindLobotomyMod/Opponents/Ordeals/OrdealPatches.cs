using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    [HarmonyPatch]
    internal class OrdealPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(ScissorsItem), nameof(ScissorsItem.OnValidTargetSelected))]
        private static IEnumerator CountScissoredOrdeals(IEnumerator enumerator, CardSlot target) {
            bool isOrdeal = target.Card.HasTrait(LobotomyCardManager.Ordeal);
            yield return enumerator;
            if (isOrdeal && OrdealUtils.OpponentIsOrdeal()) {
                yield return TurnManager.Instance.SpecialSequencer.OnOtherCardDie(null, target, false, null);
            }
        }

        /// <remarks>
        /// Using a patch to account for turn skipping and other potential shenanigans.
        /// Guarantee amountKilledThisTurn is reset to 0.
        /// </remarks>
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.PlayerTurn))]
        private static IEnumerator UpdateOrdealBattleVariables(IEnumerator enumerator, TurnManager __instance) {
            yield return enumerator;
            if (OrdealUtils.OpponentIsOrdeal())
                yield return (__instance.SpecialSequencer as OrdealBattleSequencer).OnOpponentTurnEnd(true);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TurnManager), nameof(TurnManager.ScalesTippedToOpponent))]
        [HarmonyPatch(typeof(TurnManager), nameof(TurnManager.LifeLossConditionsMet))]
        private static void OrdealCompleted(TurnManager __instance, ref bool __result) {
            if (__result || !OrdealUtils.OpponentIsOrdeal())
                return;

            if ((__instance.SpecialSequencer as OrdealBattleSequencer).PlayerHasDefeatedOrdeal())
                __result = true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.CreateNode))]
        private static void ConvertBattleIntoOrdeal(ref NodeData __result, List<NodeData> previousNodes, int mapLength) {
            // only modify card battle nodes
            // if this is the final node, only modify if we have boss ordeals
            bool bossNode = __result is BossBattleNodeData;
            if ((bossNode && !AscensionSaveData.Data.ChallengeIsActive(BossOrdeals.Id)) || __result is not CardBattleNodeData nodeData) {
                return;
            }

            bool addOrdeal = false;
            if (AscensionSaveData.Data.ChallengeIsActive(AllOrdeals.Id) ||
                UnityEngine.Random.value > (0.75f + previousNodes.Count(x => x is OrdealBattleNodeData) * 0.01f - RunState.Run.DifficultyModifier * 0.023f)) {
                addOrdeal = true;
            }

            if (!addOrdeal) return;

            int tier;
            OrdealBattleNodeData data = new() {
                id = __result.id,
                gridX = __result.gridX,
                gridY = __result.gridY,
                difficulty = nodeData.difficulty,
                connectedNodes = __result.connectedNodes
            };

            if (bossNode) {
                data.totemOpponent = AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems);
                tier = 3;
            }
            else {
                float randomValue = UnityEngine.Random.value;
                data.totemOpponent = __result is TotemBattleNodeData;

                // gate values for region tiers
                // 0.60 1.00  0
                // 0.25 0.82  1 
                // -0.1 0.64  1
                if (randomValue <= 0.6f - RunState.CurrentRegionTier * 0.35f) {
                    tier = 0;
                }
                else if (randomValue <= 1f - RunState.CurrentRegionTier * 0.18f) {
                    tier = 1;
                }
                else {
                    tier = 2;
                }
            }

            // DEBUG DEBUG
            // REMOVE ON RELEASE
            // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            if (true) {
                data.tier = tier;
                data.ordealType = OrdealType.Green;
            }
            //AssignOrdealDataToNode(data, tier);
            __result = data;

            LobotomyPlugin.Log.LogDebug($"[AddOrdeal] Region {RunState.CurrentRegionTier} {tier}");
        }
        private static void AssignOrdealDataToNode(OrdealBattleNodeData ordealNodeData, int tier) {
            ordealNodeData.tier = tier;
            ordealNodeData.ordealType = tier switch {
                1 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Indigo),
                2 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Amber),
                3 => AscensionSaveData.Data.ChallengeIsActive(FinalOrdeal.Id) ? OrdealType.White : OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Violet, OrdealType.Amber),
                _ => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Amber),
            };
        }

        #region Counter View
        [HarmonyPostfix, HarmonyPatch(typeof(ViewManager), nameof(ViewManager.GetViewInfo))]
        private static void OrdealCounterViewInfo(ref ViewInfo __result, View view) {
            if (view != OrdealUtils.ViewCounter) {
                return;
            }

            __result = OrdealUtils.OrdealViewInfo;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ViewController), nameof(ViewController.SwitchToControlMode))]
        internal static void AllowMoveToCounterView(ViewController __instance, ViewController.ControlMode mode) {
            if (!OrdealUtils.OpponentIsOrdeal())
                return;

            switch (mode) {
                case ViewController.ControlMode.CardGameDefault:
                    AddOrdealViewControls(__instance, true);
                    break;
                case ViewController.ControlMode.CardGameChoosingSlot:
                case ViewController.ControlMode.CardGameChooseDraw:
                    AddOrdealViewControls(__instance, false);
                    break;
            }
        }
        /// <summary>
        /// Adds functionality for player to look at the Ordeal counter
        /// </summary>
        private static void AddOrdealViewControls(ViewController instance, bool addSideControls) {
            if (!instance.allowedViews.Contains(OrdealUtils.ViewCounter))
                instance.allowedViews.Add(OrdealUtils.ViewCounter);

            if (!instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Exists(x => x.from == OrdealUtils.ViewCounter)) {
                instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(OrdealUtils.AcceptableViewTransitions[0]);
                instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(OrdealUtils.AcceptableViewTransitions[1]);

                if (addSideControls) {
                    instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(OrdealUtils.AcceptableViewTransitions[2]);
                    instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(OrdealUtils.AcceptableViewTransitions[3]);
                }
            }
        }
        #endregion

        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnAndPlaceElement))]
        private static void ConstructOrdealMapNode(ref GameObject __result, MapElementData data) {
            if (data is OrdealBattleNodeData ordealNodeData) {
                Texture2D[] nodeAnimation = null;
                float randomValue = UnityEngine.Random.value;
                AnimatingSprite sprite = __result.GetComponentInChildren<AnimatingSprite>();

                // region 1: dawn
                // region 2: noon
                // region 3: dusk
                nodeAnimation = ordealNodeData.tier switch {
                    1 => ordealNodeData.totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim,
                    2 => ordealNodeData.totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim,
                    3 => ordealNodeData.totemOpponent ? OrdealUtils.MidnightTotemAnim : OrdealUtils.MidnightAnim,
                    _ => ordealNodeData.totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim,
                };

                for (int i = 0; i < sprite.textureFrames.Count; i++) {
                    sprite.textureFrames[i] = nodeAnimation[i];
                }

                // recolour the sprite's mask based on the ordeal colour - also assign the correct battle id for the given the colour and tier
                sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[(int)ordealNodeData.ordealType];
                ordealNodeData.specialBattleId = ordealNodeData.ordealType switch {
                    OrdealType.Green => ordealNodeData.tier switch {
                        1 => OrdealUtils.GreenNoon,
                        2 => OrdealUtils.GreenDusk,
                        3 => OrdealUtils.GreenMidnight,
                        _ => OrdealUtils.GreenDawn
                    },
                    OrdealType.Violet => ordealNodeData.tier switch {
                        1 => OrdealUtils.VioletNoon,
                        3 => OrdealUtils.VioletMidnight,
                        _ => OrdealUtils.VioletDawn
                    },
                    OrdealType.Crimson => ordealNodeData.tier switch {
                        1 => OrdealUtils.CrimsonNoon,
                        2 => OrdealUtils.CrimsonDusk,
                        _ => OrdealUtils.CrimsonDawn
                    },
                    OrdealType.Amber => ordealNodeData.tier switch {
                        2 => OrdealUtils.AmberDusk,
                        3 => OrdealUtils.AmberMidnight,
                        _ => OrdealUtils.AmberDawn
                    },
                    OrdealType.Indigo => OrdealUtils.IndigoNoon,
                    _ => OrdealUtils.WhiteOrdeal
                };
                sprite.IterateFrame();
            }
        }
    }
}
