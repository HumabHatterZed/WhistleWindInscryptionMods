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
                switch (ordealNodeData.ordealType)
                {
                    case OrdealType.Green:
                        sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[0];
                        ordealNodeData.specialBattleId = ordealNodeData.tier switch
                        {
                            1 => OrdealUtils.GreenNoon,
                            2 => OrdealUtils.GreenDusk,
                            3 => OrdealUtils.GreenMidnight,
                            _ => OrdealUtils.GreenDawn
                        };
                        break;
                    case OrdealType.Violet:
                        sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[1];
                        ordealNodeData.specialBattleId = ordealNodeData.tier switch
                        {
                            1 => OrdealUtils.VioletNoon,
                            3 => OrdealUtils.VioletMidnight,
                            _ => OrdealUtils.VioletDawn
                        };
                        break;
                    case OrdealType.Crimson:
                        sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[2];
                        ordealNodeData.specialBattleId = ordealNodeData.tier switch
                        {
                            1 => OrdealUtils.CrimsonNoon,
                            2 => OrdealUtils.CrimsonDusk,
                            _ => OrdealUtils.CrimsonDawn
                        };
                        break;
                    case OrdealType.Amber:
                        sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[3];
                        ordealNodeData.specialBattleId = ordealNodeData.tier switch
                        {
                            2 => OrdealUtils.AmberDusk,
                            3 => OrdealUtils.AmberMidnight,
                            _ => OrdealUtils.AmberDawn
                        };
                        break;
                    case OrdealType.Indigo:
                        sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[4];
                        ordealNodeData.specialBattleId = OrdealUtils.IndigoNoon;
                        break;
                    default:
                        sprite.r.material.mainTexture = OrdealUtils.OrdealNodeMats[5];
                        ordealNodeData.specialBattleId = OrdealUtils.WhiteOrdeal;
                        break;
                }

                sprite.IterateFrame();
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.CreateNode))]
        private static void AddOrdealNode(ref NodeData __result, List<NodeData> previousNodes, int mapLength)
        {
            if (__result is CardBattleNodeData nodeData)
            {
                if (RunState.Run.regionTier == 3 && AscensionSaveData.Data.ChallengeIsActive(FinalOrdeal.Id))
                    return;

                if (__result.gridY + 1 < mapLength && !AscensionSaveData.Data.ChallengeIsActive(BossOrdeals.Id))
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
                    difficulty = (__result as CardBattleNodeData).difficulty,
                    connectedNodes = __result.connectedNodes
                };

                if (__result is BossBattleNodeData)
                    data.totemOpponent = AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems);
                else
                    data.totemOpponent = __result is TotemBattleNodeData;

                if (true) // debug, force ordeal
                {
                    data.tier = 0;
                    data.ordealType = OrdealType.Crimson;
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
