using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards.Create;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class TextureLoader // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        /// <summary>
        /// Variant of TextureHelper.GetImageAsTexture that returns null instead of throwing errors.
        /// </summary>
        /// <param name="fileName">The file name, case insensitive. Must end with .png or another file extension.</param>
        /// <param name="target">The assembly the file is an embedded resource for. Uses the calling assembly if null is given.</param>
        /// <returns>The file as a Texture2D object, or null if the file does not exist.</returns>
        public static Texture2D LoadTextureFromFile(string fileName, Assembly target = null)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            Assembly targetAssembly = target ?? Assembly.GetCallingAssembly();
            byte[] resourceFile = GetResourceBytes(fileName, targetAssembly);
            if (resourceFile == null)
            {
                return null;
            }

            Texture2D texture2D = new(2, 2);
            texture2D.LoadImage(resourceFile);
            texture2D.filterMode = FilterMode.Point;
            texture2D.name = Path.GetFileNameWithoutExtension(fileName);
            return texture2D;
        }

        public static Sprite LoadSpriteFromFile(string fileName, Vector2? vector = null, Assembly asm = null)
        {
            Texture2D texture = LoadTextureFromFile(fileName, asm);
            if (texture == null)
                return null;

            Rect rect = new(0f, 0f, texture.width, texture.height);
            Vector2 pivot = vector ?? new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot, 100f);
        }

        private static byte[] GetResourceBytes(string fileName, Assembly targetAssembly)
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

        public static FaceAnim MakeFaceAnim(string openName, string closedName = null, Assembly targetAsm = null)
        {
            Texture2D openTex = LoadTextureFromFile(openName, targetAsm);
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
    }
}
