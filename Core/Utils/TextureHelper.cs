using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using DiskCardGame;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class TextureHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Create Texture2D's from resource files
        public static Texture2D LoadTextureFromResource(byte[] resourceFile)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(resourceFile);
            texture.filterMode = FilterMode.Point;
            return texture;
        }
    }
}
