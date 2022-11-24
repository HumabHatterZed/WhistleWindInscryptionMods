﻿using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class TextureLoader // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Create Texture2D's from resource files
        public static Texture2D LoadTextureFromBytes(byte[] resourceFile)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(resourceFile);
            texture.filterMode = FilterMode.Point;
            return texture;
        }
        public static Sprite LoadSpriteFromBytes(byte[] resourceFile, bool isStarter = false)
        {
            var texture = LoadTextureFromBytes(resourceFile);
            // Starter Deck icon or Challenge icon
            Rect rect = isStarter ? new Rect(0f, 0f, 35f, 44f) : new Rect(0f, 0f, 49f, 49f);
            Vector2 pivot = new(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot);
        }
    }
}
