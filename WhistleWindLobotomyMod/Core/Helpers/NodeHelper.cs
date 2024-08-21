using DiskCardGame;
using InscryptionAPI.Nodes;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class NodeHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static List<Texture2D> GetNodeTextureList(params string[] animationName)
        {
            List<Texture2D> retval = new();
            Assembly asm = Assembly.GetCallingAssembly();
            for (int i = 0; i < 4; i++)
            {
                retval.Add(TextureLoader.LoadTextureFromFile(animationName[i], asm));
            }
            return retval;
        }
        public static NewNodeManager.FullNode CreateNode(
            string name, Type T, List<string> animationFrames,
            GenerationType generationType, GenerationType extraGenType = GenerationType.None)
        {
            List<Texture2D> nodeAnimation = new();
            if (animationFrames.Count != 4)
            {
                Texture2D defaultTexture = TextureLoader.LoadTextureFromFile("sigilAbnormality");
                for (int i = 0; i < 4; i++)
                {
                    nodeAnimation.Add(defaultTexture);
                }
                Log.LogWarning("Node animation doesn't have the correct number of frames, using placeholder texture instead.");
            }
            else
            {
                nodeAnimation = GetNodeTextureList(animationFrames.ToArray());
            }
            if (extraGenType == GenerationType.None)
            {
                return NewNodeManager.New(pluginGuid, name, generationType, T, nodeAnimation);
            }
            else
            {
                // battle node can only appear in the first three regions
                List<NodeData.SelectionCondition> data = new()
                {
                    new NodeData.WithinRegionIndexRange(0, 2)
                };
                return NewNodeManager.New(pluginGuid, name, generationType | extraGenType, T, nodeAnimation).SetGenerationPrerequisites(data);
            }
        }
    }
}
