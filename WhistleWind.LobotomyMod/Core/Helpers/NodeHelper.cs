using DiskCardGame;
using InscryptionAPI.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;
using static WhistleWind.LobotomyMod.LobotomyPlugin;

namespace WhistleWind.LobotomyMod.Core.Helpers
{
    public static class NodeHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static NewNodeManager.FullNode CreateNode(
            string name, Type T, List<byte[]> animationFrames,
            GenerationType generationType, GenerationType extraGenType = GenerationType.None)
        {
            List<Texture2D> nodeAnimation = new();
            if (animationFrames.Count != 4)
            {
                Texture2D defaultTexture = TextureLoader.LoadTextureFromBytes(Artwork.sigilAbnormality);
                for (int i = 0; i < 4; i++)
                {
                    nodeAnimation.Add(defaultTexture);
                }
                Log.LogWarning("Node animation doesn't have the correct number of frames, using placeholder texture instead.");
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    nodeAnimation.Add(TextureLoader.LoadTextureFromBytes(animationFrames[i]));
                }
            }
            if (extraGenType == GenerationType.None)
            {
                return NewNodeManager.New(pluginGuid, name, generationType, T, nodeAnimation);
            }
            else
            {
                List<NodeData.SelectionCondition> data = new()
                {
                    new NodeData.WithinRegionIndexRange(0, 2)
                };
                return NewNodeManager.New(pluginGuid, name, generationType | extraGenType, T, nodeAnimation).SetGenerationPrerequisites(data);
            }
        }
    }
}
