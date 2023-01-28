using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace Infiniscryption.Core.Helpers
{
    public static class AssetHelper
    {
        public static string FindResourceName(string key, string type, Assembly target)
        {
            string lowerKey = $".{key.ToLowerInvariant()}.{type.ToLowerInvariant()}";
            foreach (string resourceName in target.GetManifestResourceNames())
                if (resourceName.ToLowerInvariant().EndsWith(lowerKey))
                    return resourceName;

            return default(string);
        }

        private static byte[] GetResourceBytes(string key, string type, Assembly target)
        {
            string resourceName = FindResourceName(key, type, target);

            if (string.IsNullOrEmpty(resourceName))
            {
                string errorHelp = "";
                foreach (string testName in target.GetManifestResourceNames())
                    errorHelp += "," + testName;
                throw new InvalidDataException($"Could not find resource matching {key}. This is what I have: {errorHelp}");
            }

            using (Stream resourceStream = target.GetManifestResourceStream(resourceName))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    resourceStream.CopyTo(memStream);
                    return memStream.ToArray();
                }
            }
        }

        public static string GetResourceString(string key, string type)
        {
            Assembly target = Assembly.GetExecutingAssembly();
            string resourceName = FindResourceName(key, type, target);

            if (string.IsNullOrEmpty(resourceName))
            {
                string errorHelp = "";
                foreach (string testName in target.GetManifestResourceNames())
                    errorHelp += "," + testName;
                throw new InvalidDataException($"Could not find resource matching {key}. This is what I have: {errorHelp}");
            }

            using (Stream resourceStream = target.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static Texture2D LoadTexture(string texture, FilterMode filterMode = FilterMode.Point)
        {
            Texture2D retval = new(2, 2);
            byte[] imgBytes = GetResourceBytes(texture, "png", Assembly.GetExecutingAssembly());
            retval.LoadImage(imgBytes);
            retval.name = $"Infiniscryption_{texture}";
            retval.filterMode = filterMode;
            return retval;
        }

        private static string WriteWavToFile(string wavname)
        {
            byte[] wavBytes = GetResourceBytes(wavname, "wav", Assembly.GetExecutingAssembly());
            string tempPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{wavname}.wav");
            File.WriteAllBytes(tempPath, wavBytes);
            return tempPath;
        }

        public static void LoadAudioClip(string clipname, ManualLogSource log = null, string group = "Loops")
        {
            // Is this a hack?
            // Hell yes, this is a hack.

            Traverse audioController = Traverse.Create(AudioController.Instance);
            List<AudioClip> clips = audioController.Field(group).GetValue<List<AudioClip>>();

            if (clips.Find(clip => clip.name.Equals(clipname)) != null)
                return;

            string manualPath = WriteWavToFile(clipname);

            try
            {
                if (log != null)
                    log.LogInfo($"About to get audio clip at file://{manualPath}");

                using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip($"file://{manualPath}", AudioType.WAV))
                {
                    request.SendWebRequest();
                    while (request.IsExecuting()) ; // Wait for this thing to finish

                    if (request.isHttpError)
                    {
                        throw new InvalidOperationException($"Bad request getting audio clip {request.error}");
                    }
                    else
                    {
                        AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                        clip.name = clipname;

                        clips.Add(clip);
                    }
                }
            }
            finally
            {
                if (File.Exists(manualPath))
                    File.Delete(manualPath);
            }
        }
    }
}