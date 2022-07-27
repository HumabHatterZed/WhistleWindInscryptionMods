using InscryptionAPI;
using InscryptionAPI.Nodes;
using InscryptionAPI.Encounters;
using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
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
                Texture2D defaultTexture = WstlTextureHelper.LoadTextureFromResource(Resources.sigilAbnormality);
                for (int i = 0; i < 4; i++)
                {
                    nodeAnimation.Add(defaultTexture);
                }
                WstlPlugin.Log.LogError("Node animation doesn't have the correct number of frames, using placeholder texture instead.");
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    nodeAnimation.Add(WstlTextureHelper.LoadTextureFromResource(animationFrames[i]));
                }
            }
            if (extraGenType == GenerationType.None)
            {
                return NewNodeManager.New(WstlPlugin.pluginGuid, name, generationType, T, nodeAnimation);
            }
            else
            {
                List<NodeData.SelectionCondition> data = new()
                {
                    new NodeData.WithinRegionIndexRange(0, 1)
                };
                return NewNodeManager.New(WstlPlugin.pluginGuid, name, generationType | extraGenType, T, nodeAnimation).SetGenerationPrerequisites(data);
            }
        }
    }
}
