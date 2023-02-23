using InscryptionAPI.Saves;

namespace WhistleWindLobotomyMod.Core
{
    public static partial class LobotomySaveManager
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
        public static bool TriggeredWhiteNightThisRun
        {
            // Has the Clock struck twelve this run?
            get => GetBool("TriggeredWhiteNightThisRun");
            set => SetBool("TriggeredWhiteNightThisRun", value);
        }
        public static bool UsedBackwardClock
        {
            get => GetBool("UsedBackwardClock");
            set => SetBool("UsedBackwardClock", value);
        }
        public static bool OwnsApocalypseBird
        {
            get => GetBool("OwnsApocalypseBird");
            set => SetBool("OwnsApocalypseBird", value);
        }
        public static bool OwnsJesterOfNihil
        {
            get => GetBool("OwnsJesterNihil");
            set => SetBool("OwnsJesterNihil", value);
        }
        public static bool OwnsLyingAdult
        {
            get => GetBool("OwnsLyingAdult");
            set => SetBool("OwnsLyingAdult", value);
        }
        public static bool BoardEffectsApocalypse
        {
            get => GetBool("BoardEffectsApocalypse");
            set => SetBool("BoardEffectsApocalypse", value);
        }
        public static bool BoardEffectsEmerald
        {
            get => GetBool("BoardEffectsEmerald");
            set => SetBool("BoardEffectsEmerald", value);
        }
        public static bool BoardEffectsEntropy
        {
            get => GetBool("BoardEffectsEntropy");
            set => SetBool("BoardEffectsEntropy", value);
        }
        public static bool HasGottenAngelaOnce
        {
            get => GetSaveBool("HasGottenAngelaOnce");
            set => SetSaveBool("HasGottenAngelaOnce", value);
        }

        private static bool GetBool(string id) => ModdedSaveManager.RunState.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetBool(string id, object value) => ModdedSaveManager.RunState.SetValue(LobotomyPlugin.pluginGuid, id, value);
        private static bool GetSaveBool(string id) => ModdedSaveManager.SaveData.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetSaveBool(string id, object value) => ModdedSaveManager.SaveData.SetValue(LobotomyPlugin.pluginGuid, id, value);
    }
}