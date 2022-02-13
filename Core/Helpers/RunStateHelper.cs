using DiskCardGame;
using HarmonyLib;
using System;

namespace WhistleWindLobotomyMod
{
    public static class RunStateHelper // Ripped in its entirety from Infiniscryption
    {
        private const string RunStateKey = "RunState";
        private static bool Initialized = false;

        public static void Initialize(Harmony harmony)
        {
            if (!Initialized)
            {
                Initialized = true;
                harmony.PatchAll(typeof(RunStateHelper));
            }
        }

        public static bool GetBool(string key)
        {
            if (!Initialized)
                throw new InvalidOperationException("Cannot use RunStateHelper without initializing it first!");

            return SaveGameHelper.GetBool($"{RunStateKey}.{key}");
        }

        public static int GetInt(string key, int fallback = default(int))
        {
            if (!Initialized)
                throw new InvalidOperationException("Cannot use RunStateHelper without initializing it first!");

            return SaveGameHelper.GetInt($"{RunStateKey}.{key}");
        }

        public static float GetFloat(string key, float fallback = default(float))
        {
            if (!Initialized)
                throw new InvalidOperationException("Cannot use RunStateHelper without initializing it first!");

            return SaveGameHelper.GetFloat($"{RunStateKey}.{key}");
        }

        public static string GetValue(string key)
        {
            if (!Initialized)
                throw new InvalidOperationException("Cannot use RunStateHelper without initializing it first!");

            return SaveGameHelper.GetValue($"{RunStateKey}.{key}");
        }

        public static void SetValue(string key, string value)
        {
            if (!Initialized)
                throw new InvalidOperationException("Cannot use RunStateHelper without initializing it first!");

            SaveGameHelper.SetValue($"{RunStateKey}.{key}", value);
        }

        public static void ClearValue(string key)
        {
            if (!Initialized)
                throw new InvalidOperationException("Cannot use RunStateHelper without initializing it first!");

            SaveGameHelper.ClearValue($"{RunStateKey}.{key}");
        }
        /*
        [HarmonyPatch(typeof(RunState), "Initialize")]
        [HarmonyPostfix]
        public static void ClearAllRunKeys()
        {
            // This removes everything from the save file related to this mod 
            // when the chapter select menu creates a new part 1 run.
            int i = 0;
            while (i < ProgressionData.Data.introducedConsumables.Count)
            {
                if (ProgressionData.Data.introducedConsumables[i].StartsWith($"{SaveGameHelper.SaveKey}.{RunStateKey}"))
                {
                    ProgressionData.Data.introducedConsumables.RemoveAt(i);
                }
                else
                {
                    i += 1;
                }
            }
        }*/
    }
}