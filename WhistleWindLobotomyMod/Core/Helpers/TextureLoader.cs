using InscryptionAPI.Helpers;
using UnityEngine;

namespace WhistleWindLobotomyMod.Core.Helpers
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
        public static Sprite LoadSpriteFromBytes(byte[] resourceFile, Vector2? vector = null)
        {
            Texture2D texture = LoadTextureFromBytes(resourceFile);

            Rect rect = new(0f, 0f, texture.width, texture.height);
            Vector2 pivot = vector ?? new Vector2(0.5f, 0.5f);

            return Sprite.Create(texture, rect, pivot, 100f);
        }
    }
}
