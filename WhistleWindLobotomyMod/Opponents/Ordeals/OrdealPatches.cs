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
                yield return TurnManager.Instance.SpecialSequencer.OnOtherCardDie(target.Card, target, false, null);
            }
        }

        /// <remarks>
        /// Using a patch to account for turn skipping and other potential shenanigans.
        /// Guarantee amountKilledThisTurn is reset to 0.
        /// </remarks>
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.PlayerTurn))]
        private static IEnumerator UpdateOrdealBattleVariables(IEnumerator enumerator, TurnManager __instance) {
            yield return enumerator;
            if (OrdealUtils.OpponentIsOrdeal()) {
                yield return (__instance.SpecialSequencer as OrdealBattleSequencer).OnOpponentTurnEnd(true);
            }
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
            if (__result is not CardBattleNodeData nodeData) {
                return;
            }
            bool bossNode = __result is BossBattleNodeData;
            if (bossNode && OrdealRegionOrder != null) {
                if (RunState.CurrentRegionTier == 3 && LobotomyConfigManager.ChallengeIsActive(FinalOrdeal.Id)) {
                    return;
                }
                OrdealBossBattleNodeData bossData = new() {
                    id = __result.id,
                    gridX = __result.gridX,
                    gridY = __result.gridY,
                    difficulty = nodeData.difficulty,
                    connectedNodes = __result.connectedNodes,
                    bossType = OrdealUtils.OpponentID,
                    tier = 3,
                    ordealType = RunState.CurrentRegionTier < OrdealRegionOrder.Length ? OrdealRegionOrder[RunState.CurrentRegionTier] : OrdealType.Green,
                    totemOpponent = AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems)
                };
                bossData.specialBattleId = bossData.ordealType switch { 
                    OrdealType.Violet => OrdealUtils.VioletMidnight,
                    OrdealType.Amber => OrdealUtils.AmberMidnight,
                    _ => OrdealUtils.GreenMidnight
                };
                __result = bossData;
                LobotomyPlugin.Log.LogDebug($"[AddOrdeal] Boss {RunState.CurrentRegionTier} {bossData.ordealType}");
                return;
            }

            if (!LobotomyConfigManager.ChallengeIsActive(AllOrdeals.Id) && UnityEngine.Random.value <= (0.8f - RunState.Run.DifficultyModifier * 0.023f)) {
                return;
            }

            int tier;
            float randomValue = UnityEngine.Random.value;
            OrdealBattleNodeData data = new() {
                id = __result.id,
                gridX = __result.gridX,
                gridY = __result.gridY,
                difficulty = nodeData.difficulty,
                connectedNodes = __result.connectedNodes,
                totemOpponent = __result is TotemBattleNodeData
            };

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

            AssignOrdealDataToNode(data, tier);
            __result = data;

            LobotomyPlugin.Log.LogDebug($"[AddOrdeal] Region {RunState.CurrentRegionTier} {tier}");
        }
        private static void AssignOrdealDataToNode(OrdealBattleNodeData ordealNodeData, int tier) {
            ordealNodeData.tier = tier;
            ordealNodeData.ordealType = tier switch {
                1 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Indigo),
                2 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Amber),
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
            int tier = -1;
            bool totemOpponent = false;
            OrdealType type = OrdealType.Green;
            if (data is OrdealBossBattleNodeData bossNodeData) {
                tier = 3;
                type = bossNodeData.ordealType;
                totemOpponent = bossNodeData.totemOpponent;
            }
            else if (data is OrdealBattleNodeData ordealNodeData) {
                tier = ordealNodeData.tier;
                type = ordealNodeData.ordealType;
                totemOpponent = ordealNodeData.totemOpponent;
                
                ordealNodeData.specialBattleId = type switch {
                    OrdealType.Green => tier switch {
                        1 => OrdealUtils.GreenNoon,
                        2 => OrdealUtils.GreenDusk,
                        _ => OrdealUtils.GreenDawn
                    },
                    OrdealType.Violet => tier switch {
                        1 => OrdealUtils.VioletNoon,
                        _ => OrdealUtils.VioletDawn
                    },
                    OrdealType.Crimson => tier switch {
                        1 => OrdealUtils.CrimsonNoon,
                        2 => OrdealUtils.CrimsonDusk,
                        _ => OrdealUtils.CrimsonDawn
                    },
                    OrdealType.Amber => tier switch {
                        2 => OrdealUtils.AmberDusk,
                        _ => OrdealUtils.AmberDawn
                    },
                    _ => OrdealUtils.IndigoNoon
                };
            }

            if (tier != -1) {
                AnimatingSprite sprite = __result.GetComponentInChildren<AnimatingSprite>();
                Texture2D[] nodeAnimation = tier switch {
                    1 => totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim,
                    2 => totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim,
                    3 => totemOpponent ? OrdealUtils.MidnightTotemAnim : OrdealUtils.MidnightAnim,
                    _ => totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim,
                };

                for (int i = 0; i < sprite.textureFrames.Count; i++) {
                    sprite.textureFrames[i] = nodeAnimation[i];
                }

                if (type != OrdealType.White) {
                    sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[(int)type];
                }
                sprite.IterateFrame();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AscensionSaveData), nameof(AscensionSaveData.RollCurrentRunRegionOrder))]
        private static void DetermineMidnightOrder(AscensionSaveData __instance) {
            if (LobotomyConfigManager.ChallengeIsActive(BossOrdeals.Id)) {
                List<OrdealType> ordeals = new() { OrdealType.Amber, OrdealType.Violet, OrdealType.Green };
                if (SaveFile.IsAscension) {
                    ordeals.Randomize();
                    ordeals.Add(OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Amber, OrdealType.Violet));
                }
                else {
                    ordeals.Add(OrdealType.Violet);
                }

                OrdealRegionOrder = ordeals.ToArray();
            }
            else {
                OrdealRegionOrder = null;
            }
        }
        public static OrdealType[] OrdealRegionOrder = null;
    }
}
