using InscryptionAPI.TalkingCards.Create;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class TextureLoader // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Create Texture2D's from resource files
        public static Texture2D LoadTextureFromBytes(byte[] resourceFile)
        {
            if (resourceFile == null)
                return null;

            var texture = new Texture2D(2, 2);
            texture.LoadImage(resourceFile);
            texture.filterMode = FilterMode.Point;
            return texture;
        }
        public static Sprite LoadSpriteFromBytes(byte[] resourceFile, Vector2? vector = null)
        {
            if (resourceFile == null)
                return null;

            Texture2D texture = LoadTextureFromBytes(resourceFile);

            Rect rect = new(0f, 0f, texture.width, texture.height);
            Vector2 pivot = vector ?? new Vector2(0.5f, 0.5f);

            return Sprite.Create(texture, rect, pivot, 100f);
        }
        public static FaceAnim MakeFaceAnim(byte[] openBytes, byte[] closedBytes = null)
        {
            Texture2D openTex = LoadTextureFromBytes(openBytes);
            Rect rect = new(0f, 0f, openTex.width, openTex.height);
            Sprite openSprite = Sprite.Create(openTex, rect, new(0.5f, 0f), 100f);

            if (closedBytes != null)
            {
                Texture2D closedTex = LoadTextureFromBytes(closedBytes);
                Sprite closedSprite = Sprite.Create(closedTex, rect, new(0.5f, 0f), 100f);
                return new(openSprite, closedSprite);
            }

            return new(openSprite, null);
        }
    }
}
