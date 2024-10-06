using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;

namespace WhistleWindLobotomyMod.Opponents
{
    [HarmonyPatch]
    internal class OrdealPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnAndPlaceElement))]
        private static void ConstructOrdealNode(ref GameObject __result, MapElementData data)
        {
            if (data is OrdealBattleNodeData ordealNodeData)
            {
                Texture2D[] nodeAnimation = null;
                float randomValue = UnityEngine.Random.value;
                AnimatingSprite sprite = __result.GetComponentInChildren<AnimatingSprite>();

                // region 1: dawn, noon -> 79/21
                // region 2: dawn, noon, dusk -> 55/33/12
                // region 3: noon, dusk, midnight -> 53/40/7

                /*switch (RunState.Run.regionTier)
                {
                    case 0:
                        LobotomyPlugin.Log.LogDebug($"Region 0");
                        if (randomValue <= 0.79f - RunState.Run.DifficultyModifier * 0.02f)
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 0);
                        }
                        else
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 1, true);
                        }
                        break;

                    case 1:
                        LobotomyPlugin.Log.LogDebug($"Region 1");
                        if (randomValue <= 0.55f - RunState.Run.DifficultyModifier * 0.024f)
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 0);
                        }
                        else if (randomValue <= 0.88f - RunState.Run.DifficultyModifier * 0.0247f)
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 1);
                        }
                        else
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 2);
                        }
                        break;

                    case 2:
                        LobotomyPlugin.Log.LogDebug($"Region 2");
                        if (randomValue <= 0.53f - RunState.Run.DifficultyModifier * 0.026f)
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 1);
                        }
                        else if (randomValue <= 0.93f - RunState.Run.DifficultyModifier * 0.029f)
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 2);
                        }
                        else
                        {
                            nodeAnimation = AssignOrdealDataToNode(ordealNodeData, 3);
                        }
                        break;
                }*/

                switch (ordealNodeData.tier)
                {
                    case 1:
                        ordealNodeData.specialBattleId = OrdealNoon.ID;
                        nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.NoonTotemAnim : OrdealUtils.NoonAnim;
                        break;
                    case 2:
                        ordealNodeData.specialBattleId = OrdealDusk.ID;
                        nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.DuskTotemAnim : OrdealUtils.DuskAnim;
                        break;
                    case 3:
                        ordealNodeData.specialBattleId = OrdealMidnight.ID;
                        nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.MidnightTotemAnim : OrdealUtils.MidnightAnim;
                        break;
                    default:
                        ordealNodeData.specialBattleId = OrdealDawn.ID;
                        nodeAnimation = ordealNodeData.totemOpponent ? OrdealUtils.DawnTotemAnim : OrdealUtils.DawnAnim;
                        break;
                }

                for (int i = 0; i < sprite.textureFrames.Count; i++)
                {
                    sprite.textureFrames[i] = nodeAnimation[i];
                }

                sprite.r.material.mainTexture = ordealNodeData.ordealType switch
                {
                    OrdealType.Green => OrdealUtils.OrdealNodeMats[0],
                    OrdealType.Violet => OrdealUtils.OrdealNodeMats[1],
                    OrdealType.Crimson => OrdealUtils.OrdealNodeMats[2],
                    OrdealType.Amber => OrdealUtils.OrdealNodeMats[3],
                    OrdealType.Indigo => OrdealUtils.OrdealNodeMats[4],
                    _ => OrdealUtils.OrdealNodeMats[5]
                };

                sprite.IterateFrame();
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.CreateNode))]
        private static void AddOrdealNode(ref NodeData __result, List<NodeData> previousNodes, int mapLength)
        {
            if (__result is not CardBattleNodeData || __result.gridY + 1 >= mapLength)
                return;

            bool addOrdeal = AscensionSaveData.Data.ChallengeIsActive(AllOrdeals.Id);
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
                connectedNodes = __result.connectedNodes,
                totemOpponent = __result is TotemBattleNodeData
            };
            // determine the type and tier of the Ordeal based on the region
            switch (RunState.Run.regionTier)
            {
                case 0:
                    LobotomyPlugin.Log.LogDebug($"Region 0");
                    if (randomValue <= 0.79f - RunState.Run.DifficultyModifier * 0.02f)
                    {
                        AssignOrdealDataToNode(data, 0); // dawn
                    }
                    else
                    {
                        AssignOrdealDataToNode(data, 1, true); // noon, no indigo
                    }
                    break;

                case 1:
                    LobotomyPlugin.Log.LogDebug($"Region 1");
                    if (randomValue <= 0.55f - RunState.Run.DifficultyModifier * 0.024f)
                    {
                        AssignOrdealDataToNode(data, 0); // dawn
                    }
                    else if (randomValue <= 0.88f - RunState.Run.DifficultyModifier * 0.0247f)
                    {
                        AssignOrdealDataToNode(data, 1); // noon
                    }
                    else
                    {
                        AssignOrdealDataToNode(data, 2); // dusk
                    }
                    break;

                case 2:
                    LobotomyPlugin.Log.LogDebug($"Region 2");
                    if (randomValue <= 0.53f - RunState.Run.DifficultyModifier * 0.026f)
                    {
                        AssignOrdealDataToNode(data, 1); // noon
                    }
                    else if (randomValue <= 0.93f - RunState.Run.DifficultyModifier * 0.029f)
                    {
                        AssignOrdealDataToNode(data, 2); // dusk
                    }
                    else
                    {
                        AssignOrdealDataToNode(data, 3); // midnight
                    }
                    break;
            }

            __result = data;
        }
        private static void AssignOrdealDataToNode(OrdealBattleNodeData ordealNodeData, int tier, bool removeIndigo = false)
        {
            ordealNodeData.tier = tier;
            switch (tier)
            {
                case 1:
                    LobotomyPlugin.Log.LogDebug($"Noon");
                    if (removeIndigo)
                        ordealNodeData.ordealType = OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet);
                    else
                        ordealNodeData.ordealType = OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Indigo);
                    break;
                case 2:
                    LobotomyPlugin.Log.LogDebug($"Dusk");
                    ordealNodeData.ordealType = OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Amber);
                    break;
                case 3:
                    LobotomyPlugin.Log.LogDebug($"Midnight");
                    ordealNodeData.ordealType = OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Violet, OrdealType.Amber);
                    break;
                default:
                    LobotomyPlugin.Log.LogDebug($"Dawn");
                    ordealNodeData.ordealType = OrdealUtils.ChooseRandomOrdealType(OrdealType.Green, OrdealType.Crimson, OrdealType.Violet, OrdealType.Amber);
                    break;
            }
        }
    }
}
