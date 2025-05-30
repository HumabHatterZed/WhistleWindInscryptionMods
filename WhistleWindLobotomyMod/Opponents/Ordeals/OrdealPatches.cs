using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;

namespace WhistleWindLobotomyMod.Opponents
{
    [HarmonyPatch]
    internal class OrdealPatches
    {
        /// <remarks>
        /// Use to guarantee the sequence works correctly (account for turn skipping)
        /// </remarks>
        [HarmonyPrefix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.PlayerTurn))]
        private static bool ResetOrdealKillCountEachTurn(TurnManager __instance)
        {
            if (OrdealUtils.OpponentIsOrdeal())
                (__instance.SpecialSequencer as OrdealBattleSequencer).amountKilledThisTurn = 0;

            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.OpponentTurn))]
        private static IEnumerator UpdateOrdealAmountLeft(IEnumerator enumerator, TurnManager __instance)
        {
            yield return enumerator;

            if (OrdealUtils.OpponentIsOrdeal())
            {
                OrdealBattleSequencer sequencer = __instance.SpecialSequencer as OrdealBattleSequencer;
                bool leftoverCardsLeft = OrdealCounterManager.Instance.amountLeft < 1 && !sequencer.PlayerHasDefeatedOrdeal();
                if (sequencer.amountKilledThisTurn > 0 || leftoverCardsLeft)
                {
                    yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, 0.5f);
                    yield return OrdealCounterManager.Instance.UpdateAmountLeft(sequencer.amountKilledThisTurn, 0.25f);
                    if (leftoverCardsLeft)
                    {
                        yield return TextDisplayer.Instance.PlayDialogueEvent("OrdealDefeatedCardsLeft", TextDisplayer.MessageAdvanceMode.Input);
                    }
                    yield return 0.5f;
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ViewController), nameof(ViewController.SwitchToControlMode))]
        private static void AllowMoveToCounterView(ViewController __instance, ViewController.ControlMode mode)
        {
            if (!OrdealUtils.OpponentIsOrdeal())
                return;

            switch (mode)
            {
                case ViewController.ControlMode.CardGameDefault:
                    AddOrdealViewControls(__instance, true);
                    break;
                case ViewController.ControlMode.CardGameChoosingSlot:
                    AddOrdealViewControls(__instance, false);
                    break;
                case ViewController.ControlMode.CardGameChooseDraw:
                    AddOrdealViewControls(__instance, false);
                    break;
            }
        }
        private static void AddOrdealViewControls(ViewController instance, bool addSideControls)
        {
            if (!OrdealUtils.OpponentIsOrdeal())
                return;

            if (!instance.allowedViews.Contains(OrdealUtils.ViewCounter))
                instance.allowedViews.Add(OrdealUtils.ViewCounter);

            if (!instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Exists(x => x.from == OrdealUtils.ViewCounter))
            {
                instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(
                    new ViewController.ViewTransitionInput(View.OpponentQueue, OrdealUtils.ViewCounter, Button.LookUp));

                instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(
                    new ViewController.ViewTransitionInput(OrdealUtils.ViewCounter, View.OpponentQueue, Button.LookDown));

                if (addSideControls)
                {
                    instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(
                        new ViewController.ViewTransitionInput(OrdealUtils.ViewCounter, View.Consumables, Button.LookRight));

                    instance.CARDBATTLE_ALT_TRANSITION_INPUTS.Add(
                        new ViewController.ViewTransitionInput(OrdealUtils.ViewCounter, View.Scales, Button.LookLeft));
                }
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.ScalesTippedToOpponent))]
        private static void ValidateOrdealCompletion(TurnManager __instance, ref bool __result)
        {
            if (!OrdealUtils.OpponentIsOrdeal())
                return;

            if ((__instance.SpecialSequencer as OrdealBattleSequencer).PlayerHasDefeatedOrdeal())
                __result = true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ViewManager), nameof(ViewManager.GetViewInfo))]
        private static void CustomViewForCounter(ref ViewInfo __result, View view)
        {
            if (view == OrdealUtils.ViewCounter)
            {
                __result = new()
                {
                    camPosition = new Vector3(0f, 7.65f, -5.15f),
                    fov = 35f
                };
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnAndPlaceElement))]
        private static void ConstructOrdealNode(ref GameObject __result, MapElementData data)
        {
            if (data is OrdealBattleNodeData ordealNodeData)
            {
                Texture2D[] nodeAnimation = null;
                float randomValue = UnityEngine.Random.value;
                AnimatingSprite sprite = __result.GetComponentInChildren<AnimatingSprite>();

                // region 1: dawn
                // region 2: noon
                // region 3: dusk
                nodeAnimation = ordealNodeData.tier switch
                {
                    1 => ordealNodeData.totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim,
                    2 => ordealNodeData.totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim,
                    3 => ordealNodeData.totemOpponent ? OrdealUtils.MidnightTotemAnim : OrdealUtils.MidnightAnim,
                    _ => ordealNodeData.totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim,
                };

                for (int i = 0; i < sprite.textureFrames.Count; i++)
                {
                    sprite.textureFrames[i] = nodeAnimation[i];
                }

                // recolour the sprite's mask based on the ordeal colour - also assign the correct battle id for the given the colour and tier
                sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[(int)ordealNodeData.ordealType];
                ordealNodeData.specialBattleId = ordealNodeData.ordealType switch
                {
                    OrdealType.Green => ordealNodeData.tier switch
                    {
                        1 => OrdealUtils.GreenNoon,
                        2 => OrdealUtils.GreenDusk,
                        3 => OrdealUtils.GreenMidnight,
                        _ => OrdealUtils.GreenDawn
                    },
                    OrdealType.Violet => ordealNodeData.tier switch
                    {
                        1 => OrdealUtils.VioletNoon,
                        3 => OrdealUtils.VioletMidnight,
                        _ => OrdealUtils.VioletDawn
                    },
                    OrdealType.Crimson => ordealNodeData.tier switch
                    {
                        1 => OrdealUtils.CrimsonNoon,
                        2 => OrdealUtils.CrimsonDusk,
                        _ => OrdealUtils.CrimsonDawn
                    },
                    OrdealType.Amber => ordealNodeData.tier switch
                    {
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

        [HarmonyPostfix, HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.CreateNode))]
        private static void AddOrdealNode(ref NodeData __result, List<NodeData> previousNodes, int mapLength)
        {
            if (__result is CardBattleNodeData nodeData)
            {
                // if this is the last node and we aren't overriding boss nodes
                if (nodeData.gridY + 1 >= mapLength && !AscensionSaveData.Data.ChallengeIsActive(BossOrdeals.Id))
                    return;

                bool addOrdeal = AscensionSaveData.Data.ChallengeIsActive(AllOrdeals.Id);
                addOrdeal = true; // debug
                if (!addOrdeal)
                {
                    int numOfPreviousOrdeals = previousNodes.Count(x => x is OrdealBattleNodeData);
                    addOrdeal = UnityEngine.Random.value > 0.75f + numOfPreviousOrdeals * 0.01f - RunState.Run.DifficultyModifier * 0.023f;
                }

                if (!addOrdeal) return;

                // if the check passes, turn this battle node into an Ordeal node
                float randomValue = UnityEngine.Random.value;
                OrdealBattleNodeData data = new()
                {
                    id = __result.id,
                    gridX = __result.gridX,
                    gridY = __result.gridY,
                    difficulty = nodeData.difficulty,
                    connectedNodes = __result.connectedNodes
                };

                if (__result is BossBattleNodeData boss && boss.bossType == Opponent.Type.LeshyBoss)
                    data.totemOpponent = AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems);
                else
                    data.totemOpponent = __result is TotemBattleNodeData;

                // DEBUG DEBUG
                // REMOVE ON RELEASE
                // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
                if (true)
                {
                    data.tier = 0;
                    data.ordealType = OrdealType.Green;
                    __result = data;
                    return;
                }

                // determine the type and tier of the Ordeal based on the region
                switch (RunState.Run.regionTier)
                {
                    case 3:
                        LobotomyPlugin.Log.LogDebug($"Region 3");
                        AssignOrdealDataToNode(data, 3);
                        break;
                    case 2:
                        LobotomyPlugin.Log.LogDebug($"Region 2");
                        AssignOrdealDataToNode(data, 2);
                        break;
                    case 1:
                        LobotomyPlugin.Log.LogDebug($"Region 1");
                        AssignOrdealDataToNode(data, 1);
                        break;
                    default:
                        LobotomyPlugin.Log.LogDebug($"Region 0");
                        AssignOrdealDataToNode(data, 0);
                        break;
                }

                __result = data;
            }
        }
        private static void AssignOrdealDataToNode(OrdealBattleNodeData ordealNodeData, int tier)
        {
            ordealNodeData.tier = tier;
            ordealNodeData.ordealType = tier switch
            {
                1 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Indigo),
                2 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Amber),
                3 => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Violet, OrdealType.Amber),
                _ => OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Amber),
            };
        }
    }
}
