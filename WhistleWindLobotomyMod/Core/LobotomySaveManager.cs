using InscryptionAPI.Saves;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomySaveManager
    {
        public static bool LearnedAbnormalChoice
        {
            get => GetSaveBool("LearnedAbnormalChoice");
            set => SetSaveBool("LearnedAbnormalChoice", value);
        }
        public static bool LearnedSefirotChoice
        {
            get => GetSaveBool("LearnedSefirotChoice");
            set => SetSaveBool("LearnedSefirotChoice", value);
        }
        public static bool ShownAbnormalEncounters
        {
            get => GetRunBool("ShownAbnormalEncounters");
            set => SetRunBool("ShownAbnormalEncounters", value);
        }
        public static bool UsedBackwardClock
        {
            get => GetRunBool("UsedBackwardClock");
            set => SetRunBool("UsedBackwardClock", value);
        }
        public static bool UsedBackwardClockGBC
        {
            get => GetSaveBool("UsedBackwardClockGBC");
            set => SetSaveBool("UsedBackwardClockGBC", value);
        }
        public static bool TriggeredWhiteNightThisRun
        {
            // Has the Clock struck twelve this run?
            get => GetRunBool("TriggeredWhiteNightThisRun");
            set => SetRunBool("TriggeredWhiteNightThisRun", value);
        }
        public static bool UnlockedWhiteNight
        {
            get => GetSaveBool("HasSeenWhiteNight");
            set => SetSaveBool("HasSeenWhiteNight", value);
        }
        public static bool OwnsApocalypseBird
        {
            get => GetRunBool("OwnsApocalypseBird");
            set => SetRunBool("OwnsApocalypseBird", value);
        }
        public static bool OwnsJesterOfNihil
        {
            get => GetRunBool("OwnsJesterNihil");
            set => SetRunBool("OwnsJesterNihil", value);
        }
        public static bool OwnsLyingAdult
        {
            get => GetRunBool("OwnsLyingAdult");
            set => SetRunBool("OwnsLyingAdult", value);
        }
        public static bool BoardEffectsApocalypse
        {
            get => GetRunBool("BoardEffectsApocalypse");
            set => SetRunBool("BoardEffectsApocalypse", value);
        }
        public static bool BoardEffectsEmerald
        {
            get => GetRunBool("BoardEffectsEmerald");
            set => SetRunBool("BoardEffectsEmerald", value);
        }
        public static bool BoardEffectsEntropy
        {
            get => GetRunBool("BoardEffectsEntropy");
            set => SetRunBool("BoardEffectsEntropy", value);
        }
        public static bool UnlockedApocalypseBird
        {
            get => GetSaveBool("UnlockedApocalypseBird");
            set => SetSaveBool("UnlockedApocalypseBird", value);
        }
        public static bool UnlockedJesterOfNihil
        {
            get => GetSaveBool("UnlockedJesterOfNihil");
            set => SetSaveBool("UnlockedJesterOfNihil", value);
        }
        public static bool UnlockedLyingAdult
        {
            get => GetSaveBool("UnlockedLyingAdult");
            set => SetSaveBool("UnlockedLyingAdult", value);
        }
        public static bool UnlockedAngela
        {
            get => GetSaveBool("UnlockedAngela");
            set => SetSaveBool("UnlockedAngela", value);
        }

        private static bool GetRunBool(string id) => ModdedSaveManager.RunState.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetRunBool(string id, object value) => ModdedSaveManager.RunState.SetValue(LobotomyPlugin.pluginGuid, id, value);
        private static bool GetSaveBool(string id) => ModdedSaveManager.SaveData.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetSaveBool(string id, object value) => ModdedSaveManager.SaveData.SetValue(LobotomyPlugin.pluginGuid, id, value);
    }
}