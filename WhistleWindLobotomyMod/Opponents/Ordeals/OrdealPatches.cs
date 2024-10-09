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
                addOrdeal = true; // debug
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
                connectedNodes = __result.connectedNodes,
                totemOpponent = __result is TotemBattleNodeData
            };

            if (true) // debug, force ordeal
            {
                data.tier = 1;
                data.ordealType = OrdealType.Violet;
                __result = data;
                return;
            }

            // determine the type and tier of the Ordeal based on the region
            switch (RunState.Run.regionTier)
            {
                case 0:
                    LobotomyPlugin.Log.LogDebug($"Region 0");
                    AssignOrdealDataToNode(data, 0);
                    break;

                case 1:
                    LobotomyPlugin.Log.LogDebug($"Region 1");
                    AssignOrdealDataToNode(data, 1);
                    break;

                case 2:
                    LobotomyPlugin.Log.LogDebug($"Region 2");
                    AssignOrdealDataToNode(data, 2);
                    break;
            }

            __result = data;
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
