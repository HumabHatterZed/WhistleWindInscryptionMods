using InscryptionAPI.TalkingCards.Create;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class TextureLoader // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static Texture2D LoadTextureFromBytes(byte[] resourceFile)
        {
            Texture2D texture2D = new(2, 2, TextureFormat.RGBA32, false);
            texture2D.LoadImage(resourceFile);
            texture2D.filterMode = FilterMode.Point;
            return texture2D;
        }
        public static Sprite LoadSpriteFromBytes(byte[] resourceFile, Vector2? vector = null)
        {
            Texture2D texture = LoadTextureFromBytes(resourceFile);
            if (texture == null)
                return null;

            Rect rect = new(0f, 0f, texture.width, texture.height);
            Vector2 pivot = vector ?? new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot, 100f);
        }

        public static Texture2D LoadTextureFromFile(string fileName, Assembly target = null)
        {
            if (fileName == null)
                return null;

            Assembly targetAssembly = target ?? Assembly.GetCallingAssembly();
            byte[] resourceFile = GetResource(fileName.EndsWith(".png") ? fileName : fileName + ".png", targetAssembly);
            if (resourceFile == null)
            {
                //Debug.Log($"Could not get file: [{fileName}]");
                return null;
            }

            Texture2D texture2D = new(2, 2);
            texture2D.LoadImage(resourceFile);
            texture2D.filterMode = FilterMode.Point;
            return texture2D;
        }

        public static Sprite LoadSpriteFromFile(string fileName, Vector2? vector = null)
        {
            Texture2D texture = LoadTextureFromFile(fileName);
            if (texture == null)
                return null;

            Rect rect = new(0f, 0f, texture.width, texture.height);
            Vector2 pivot = vector ?? new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot, 100f);
        }

        public static FaceAnim MakeFaceAnim(string openName, string closedName = null)
        {
            Texture2D openTex = LoadTextureFromFile(openName);
            Rect rect = new(0f, 0f, openTex.width, openTex.height);
            Sprite openSprite = Sprite.Create(openTex, rect, new(0.5f, 0f), 100f);

            if (closedName != null)
            {
                Texture2D closedTex = LoadTextureFromFile(closedName);
                Sprite closedSprite = Sprite.Create(closedTex, rect, new(0.5f, 0f), 100f);
                return new(openSprite, closedSprite);
            }

            return new(openSprite, null);
        }

        private static byte[] GetResource(string fileName, Assembly targetAssembly)
        {
            string lowerKey = "." + fileName.ToLowerInvariant();
            string text = targetAssembly.GetManifestResourceNames().FirstOrDefault((string r) => r.ToLowerInvariant().EndsWith(lowerKey));
            if (string.IsNullOrEmpty(text))
                return null;

            using Stream stream = targetAssembly.GetManifestResourceStream(text);
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
