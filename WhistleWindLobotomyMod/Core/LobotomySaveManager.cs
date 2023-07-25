using InscryptionAPI.Saves;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomySaveManager
    {
        public static bool LearnedAbnormalChoice
        {
            get => GetSaveBool("LearnedAbnormalChoice");
            set => SetSaveValue("LearnedAbnormalChoice", value);
        }
        public static bool LearnedSefirotChoice
        {
            get => GetSaveBool("LearnedSefirotChoice");
            set => SetSaveValue("LearnedSefirotChoice", value);
        }
        public static bool ShownAbnormalEncounters
        {
            get => GetRunBool("ShownAbnormalEncounters");
            set => SetRunValue("ShownAbnormalEncounters", value);
        }
        public static bool UsedBackwardClock
        {
            get => GetRunBool("UsedBackwardClock");
            set => SetRunValue("UsedBackwardClock", value);
        }
        public static bool UsedBackwardClockGBC
        {
            get => GetSaveBool("UsedBackwardClockGBC");
            set => SetSaveValue("UsedBackwardClockGBC", value);
        }
        public static bool TriggeredWhiteNightThisRun
        {
            // Has the Clock struck twelve this run?
            get => GetRunBool("TriggeredWhiteNightThisRun");
            set => SetRunValue("TriggeredWhiteNightThisRun", value);
        }
        public static bool TriggeredWhiteNightThisBattle
        {
            // Has the Clock struck twelve this run?
            get => GetRunBool("TriggeredWhiteNightThisBattle");
            set => SetRunValue("TriggeredWhiteNightThisBattle", value);
        }
        public static int OpponentBlessings
        {
            get => GetRunInt("OpponentBlessings");
            set => SetRunValue("OpponentBlessings", value);
        }
        public static bool OwnsApocalypseBird
        {
            get => GetRunBool("OwnsApocalypseBird");
            set => SetRunValue("OwnsApocalypseBird", value);
        }
        public static bool OwnsJesterOfNihil
        {
            get => GetRunBool("OwnsJesterNihil");
            set => SetRunValue("OwnsJesterNihil", value);
        }
        public static bool OwnsLyingAdult
        {
            get => GetRunBool("OwnsLyingAdult");
            set => SetRunValue("OwnsLyingAdult", value);
        }
        public static bool BoardEffectsApocalypse
        {
            get => GetRunBool("BoardEffectsApocalypse");
            set => SetRunValue("BoardEffectsApocalypse", value);
        }
        public static bool BoardEffectsEmerald
        {
            get => GetRunBool("BoardEffectsEmerald");
            set => SetRunValue("BoardEffectsEmerald", value);
        }
        public static bool BoardEffectsEntropy
        {
            get => GetRunBool("BoardEffectsEntropy");
            set => SetRunValue("BoardEffectsEntropy", value);
        }
        public static bool UnlockedApocalypseBird
        {
            get => GetSaveBool("UnlockedApocalypseBird");
            set => SetSaveValue("UnlockedApocalypseBird", value);
        }
        public static bool UnlockedJesterOfNihil
        {
            get => GetSaveBool("UnlockedJesterOfNihil");
            set => SetSaveValue("UnlockedJesterOfNihil", value);
        }
        public static bool UnlockedLyingAdult
        {
            get => GetSaveBool("UnlockedLyingAdult");
            set => SetSaveValue("UnlockedLyingAdult", value);
        }
        public static bool UnlockedAngela
        {
            get => GetSaveBool("UnlockedAngela");
            set => SetSaveValue("UnlockedAngela", value);
        }

        private static int GetRunInt(string id) => ModdedSaveManager.RunState.GetValueAsInt(LobotomyPlugin.pluginGuid, id);
        private static bool GetRunBool(string id) => ModdedSaveManager.RunState.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetRunValue(string id, object value) => ModdedSaveManager.RunState.SetValue(LobotomyPlugin.pluginGuid, id, value);
        private static bool GetSaveBool(string id) => ModdedSaveManager.SaveData.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetSaveValue(string id, object value) => ModdedSaveManager.SaveData.SetValue(LobotomyPlugin.pluginGuid, id, value);
    }
}