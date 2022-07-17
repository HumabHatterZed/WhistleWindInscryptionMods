using InscryptionAPI;
using InscryptionAPI.Nodes;
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
            string name, GenerationType generationType, Type T, List<byte[]> animationFrames)
        {
            List<Texture2D> nodeAnimation = new();
            if (animationFrames.Count != 4)
            {
                WstlPlugin.Log.LogError("Node animation doesn't have the correct number of frame, using placeholder texture instead.");
                Texture2D defaultTexture = WstlTextureHelper.LoadTextureFromResource(Resources.sigilAbnormality);
                for (int i = 0; i < 4; i++)
                {
                    nodeAnimation.Add(defaultTexture);
                }
            }
            else
            {
                nodeAnimation.Add(WstlTextureHelper.LoadTextureFromResource(animationFrames[0]));
                nodeAnimation.Add(WstlTextureHelper.LoadTextureFromResource(animationFrames[1]));
                nodeAnimation.Add(WstlTextureHelper.LoadTextureFromResource(animationFrames[2]));
                nodeAnimation.Add(WstlTextureHelper.LoadTextureFromResource(animationFrames[3]));
            }
            return NewNodeManager.New(WstlPlugin.pluginGuid, name, generationType, T, nodeAnimation);
        }
    }
}
