using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Opponents
{
    // 4 sequencers that control the blueprint
    [HarmonyPatch]
    internal class OrdealPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnAndPlaceElement))]
        private static void SetUpOrdealNode(ref GameObject __result, MapElementData data)
        {
            if (data is OrdealBattleNodeData ordealNodeData)
            {
                Texture2D[] nodeAnimation = null;
                AnimatingSprite sprite = __result.GetComponentInChildren<AnimatingSprite>();

                float randomValue = UnityEngine.Random.value;
                switch (RunState.Run.regionTier)
                {
                    case 0: // 79/21 | dawn and noon
                        if (randomValue <= 0.79f - RunState.Run.DifficultyModifier * 0.02f)
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 0: Dawn");
                            ordealNodeData.specialBattleId = OrdealDawn.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Violet, OrdealColour.Amber);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim;
                        }
                        else
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 0: Noon");
                            ordealNodeData.specialBattleId = OrdealNoon.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Violet, OrdealColour.Indigo);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim;
                        }
                        break;

                    case 1: // 59/30/11 | dawn, noon, dusk
                        if (randomValue <= 0.59f - RunState.Run.DifficultyModifier * 0.024f)
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 1: Dawn");
                            ordealNodeData.specialBattleId = OrdealDawn.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Violet, OrdealColour.Amber);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim;
                        }
                        else if (randomValue <= 0.89f - RunState.Run.DifficultyModifier * 0.0247f)
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 1: Noon");
                            ordealNodeData.specialBattleId = OrdealNoon.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Violet, OrdealColour.Indigo);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim;
                        }
                        else
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 1: Dusk");
                            ordealNodeData.specialBattleId = OrdealDusk.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Amber);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim;
                        }
                        break;

                    case 2: // 55/39/6 | noon, dusk, midnight
                        if (randomValue <= 0.55f - RunState.Run.DifficultyModifier * 0.026f)
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 2: Noon");
                            ordealNodeData.specialBattleId = OrdealNoon.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Violet, OrdealColour.Indigo);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim;
                        }
                        else if (randomValue <= 0.94f - RunState.Run.DifficultyModifier * 0.029f)
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 2: Dusk");
                            ordealNodeData.specialBattleId = OrdealDusk.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Crimson, OrdealColour.Amber);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim;
                        }
                        else
                        {
                            LobotomyPlugin.Log.LogDebug($"Region 2: Midnight");
                            ordealNodeData.specialBattleId = OrdealMidnight.ID;
                            ordealNodeData.ordealColour = OrdealUtils.ChooseOrdealColourFromPossibilities(
                                OrdealColour.Green, OrdealColour.Violet, OrdealColour.Amber);

                            nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.MidnightTotemAnim : OrdealUtils.MidnightAnim;
                        }
                        break;
                }

                if (nodeAnimation == null)
                    return;

                for (int i = 0; i < sprite.textureFrames.Count; i++)
                {
                    sprite.textureFrames[i] = nodeAnimation[i];
                }
                sprite.IterateFrame();
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.CreateNode))]
        private static void AddOrdealNode(ref NodeData __result, List<NodeData> nodesInRow, int mapLength)
        {
            // region 1: dawn and noon
            // region 2: noon and dusk
            // region 3: noon, dusk, midnight

            if ((__result is not CardBattleNodeData && __result is not TotemBattleNodeData) || __result.gridY + 1 == mapLength)
                return;

            if (!nodesInRow.Exists(x => x is OrdealBattleNodeData) || AscensionSaveData.Data.ChallengeIsActive(AllOrdeals.Id))
            {
                // if the check passes, turn one of the battle nodes into an Ordeal
                if (false && UnityEngine.Random.value <= 0.81f - RunState.Run.DifficultyModifier * 0.061f)
                    return;

                __result = new OrdealBattleNodeData()
                {
                    id = __result.id,
                    gridX = __result.gridX,
                    gridY = __result.gridY,
                    connectedNodes = __result.connectedNodes,
                    totemOpponent = __result is TotemBattleNodeData
                };
            }
        }
    }
}
