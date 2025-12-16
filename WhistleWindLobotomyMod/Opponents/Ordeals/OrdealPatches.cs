using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Tango;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    [HarmonyPatch]
    internal class OrdealPatches {
        private static readonly OrdealType[] NonAscensionBossOrder = {
            OrdealType.Green,
            OrdealType.Violet,
            OrdealType.Amber,
            OrdealType.Violet
        };



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

        private static OrdealBossBattleNodeData CreateOrdealBossNode(NodeData baseNode, int nodeDifficulty, int regionTier) {
            OrdealBossBattleNodeData retval = new() {
                id = baseNode.id,
                gridX = baseNode.gridX,
                gridY = baseNode.gridY,
                difficulty = nodeDifficulty,
                connectedNodes = baseNode.connectedNodes,
                bossType = OrdealUtils.OpponentID,
                tier = 3,
                ordealType = SaveFile.IsAscension ? (OrdealType)LobotomySaveManager.GetCurrentOrdealBoss(regionTier) : NonAscensionBossOrder[regionTier],
                totemOpponent = SaveFile.IsAscension && AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems)
            };
            retval.specialBattleId = retval.ordealType switch {
                OrdealType.Violet => OrdealUtils.VioletMidnight,
                OrdealType.Amber => OrdealUtils.AmberMidnight,
                _ => OrdealUtils.GreenMidnight
            };
            return retval;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.CreateNode))]
        private static void ConvertBattleIntoOrdeal(ref NodeData __result, ref int y) {
            // only modify card battle nodes
            // if this is the final node, only modify if we have boss ordeals
            if (RunState.CurrentRegionTier > 2 || __result is not CardBattleNodeData nodeData) {
                return;
            }

            if (__result is BossBattleNodeData && LobotomyConfigManager.ChallengeIsActive(BossOrdeals.Id)) {
                // if no boss ordeals or it's the final region and we are overriding with the white ordeals
                OrdealBossBattleNodeData bossData = CreateOrdealBossNode(__result, nodeData.difficulty, RunState.CurrentRegionTier);
                __result = bossData;
                LobotomyPlugin.Log.LogDebug($"[AddOrdeal] Boss: {bossData.ordealType} | regionTier: {RunState.CurrentRegionTier}");
            }
            else if (LobotomyConfigManager.ChallengeIsActive(AllOrdeals.Id) || Random.value <= (0.72f - RunState.Run.DifficultyModifier * 0.043f)) {
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

                // exception for 1st map - no noons as the first couple battles
                // gate values for region tiers
                // 0.60 1.00  0
                // 0.25 0.82  1 
                // -0.1 0.64  1
                if ((RunState.CurrentRegionTier == 0 && y < 7) || randomValue <= 0.6f - RunState.CurrentRegionTier * 0.35f) {
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
            }
        }
        private static void AssignOrdealDataToNode(OrdealBattleNodeData ordealNodeData, int tier) {
            ordealNodeData.tier = tier;
            ordealNodeData.ordealType = tier switch {
                1 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Indigo),
                2 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Amber),
                _ => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Amber),
            };
            LobotomyPlugin.Log.LogDebug($"[AssignOrdealDataToNode] {tier} {ordealNodeData.ordealType}");
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
                type = bossNodeData.ordealType;
                totemOpponent = bossNodeData.totemOpponent;
                if (type != OrdealType.White) {
                    tier = 3;
                }
                else {
                    tier = 0;
                }
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
                Texture2D[] nodeAnimation;
                if (type == OrdealType.White) {
                    nodeAnimation = OrdealUtils.WhiteOrdealAnim;
                }
                else {
                    nodeAnimation = tier switch {
                        1 => totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim,
                        2 => totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim,
                        3 => totemOpponent ? OrdealUtils.MidnightTotemAnim : OrdealUtils.MidnightAnim,
                        _ => totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim,
                    };
                }

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
            //OrdealRegionOrder = null;
            if (LobotomyConfigManager.ChallengeIsActive(BossOrdeals.Id)) {
                List<OrdealType> ordeals = new() { OrdealType.Amber, OrdealType.Violet, OrdealType.Green };
                if (SaveFile.IsAscension) {
                    ordeals = ordeals.Randomize().ToList();
                    ordeals.Add(OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Amber, OrdealType.Violet)); // randomly add 4th ordeal
                }
                else {
                    ordeals.Add(OrdealType.Violet); // for non KCM, leshy is always replaced with Violet
                }

                LobotomySaveManager.OrdealBossOrder1 = (int)ordeals[0];
                LobotomySaveManager.OrdealBossOrder2 = (int)ordeals[1];
                LobotomySaveManager.OrdealBossOrder3 = (int)ordeals[2];
                LobotomySaveManager.OrdealBossOrder4 = (int)ordeals[3];
            }
        }
        //public static OrdealType[] OrdealRegionOrder = null;
    }
}
