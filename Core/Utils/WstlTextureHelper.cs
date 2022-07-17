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
    public static class WstlTextureHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Create Texture2D's from resource files
        public static Texture2D LoadTextureFromResource(byte[] resourceFile)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(resourceFile);
            texture.filterMode = FilterMode.Point;
            return texture;
        }
        public static Sprite LoadSpriteFromResource(byte[] resourceFile, bool starterDeck)
        {
            var texture = LoadTextureFromResource(resourceFile);
            return Sprite.Create(texture, starterDeck ? new Rect(0f, 0f, 35f, 44f) : new Rect(0f, 0f, 49f, 49f), starterDeck ? new Vector2(0.5f, 0.5f) : new Vector2(0.5f, 0.5f));
        }
    }
}
