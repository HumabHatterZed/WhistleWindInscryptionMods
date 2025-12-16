using DiskCardGame;
using InscryptionAPI.Saves;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod.Core {
    public static class LobotomySaveManager {

        public static int GetCurrentOrdealBoss(int regionTier) {
            return regionTier switch {
                0 => OrdealBossOrder1,
                1 => OrdealBossOrder2,
                2 => OrdealBossOrder3,
                3 => OrdealBossOrder4,
                _ => 0
            };
        }
        public static int OrdealBossOrder1 {
            get => GetRunInt("OrdealBossOrder1");
            set => SetRunValue("OrdealBossOrder1", value);
        }
        public static int OrdealBossOrder2 {
            get => GetRunInt("OrdealBossOrder2");
            set => SetRunValue("OrdealBossOrder2", value);
        }
        public static int OrdealBossOrder3 {
            get => GetRunInt("OrdealBossOrder3");
            set => SetRunValue("OrdealBossOrder3", value);
        }
        public static int OrdealBossOrder4 {
            get => GetRunInt("OrdealBossOrder4");
            set => SetRunValue("OrdealBossOrder4", value);
        }

        public static bool LearnedOrdeals {
            get => GetSaveBool("LearnedOrdeals");
            set => SetSaveValue("LearnedOrdeals", value);
        }
        public static bool LearnedAbnormalChoice {
            get => GetSaveBool("LearnedAbnormalChoice");
            set => SetSaveValue("LearnedAbnormalChoice", value);
        }
        public static bool LearnedSefirotChoice {
            get => GetRunBool("LearnedSefirotChoice");
            set => SetRunValue("LearnedSefirotChoice", value);
        }
        public static bool ShownAbnormalEncounters {
            get => GetSaveBool("ShownAbnormalEncounters");
            set => SetSaveValue("ShownAbnormalEncounters", value);
        }
        public static bool UsedBackwardClock {
            get => GetRunBool("UsedBackwardClock");
            set => SetRunValue("UsedBackwardClock", value);
        }
        public static bool UsedBackwardClockGBC {
            get => GetSaveBool("UsedBackwardClockGBC");
            set => SetSaveValue("UsedBackwardClockGBC", value);
        }
        public static bool TriggeredWhiteNightThisRun {
            // Has the Clock struck twelve this run?
            get => GetRunBool("TriggeredWhiteNightThisRun");
            set => SetRunValue("TriggeredWhiteNightThisRun", value);
        }
        public static bool TriggeredWhiteNightThisBattle {
            // Has the Clock struck twelve this run?
            get => GetRunBool("TriggeredWhiteNightThisBattle");
            set => SetRunValue("TriggeredWhiteNightThisBattle", value);
        }
        public static int OpponentBlessings {
            get => GetRunInt("OpponentBlessings");
            set => SetRunValue("OpponentBlessings", value);
        }
        public static bool OwnsApocalypseBird {
            get => GetRunBool("OwnsApocalypseBird");
            set => SetRunValue("OwnsApocalypseBird", value);
        }
        public static bool OwnsJesterOfNihil {
            get => GetRunBool("OwnsJesterNihil");
            set => SetRunValue("OwnsJesterNihil", value);
        }
        public static bool OwnsLyingAdult {
            get => GetRunBool("OwnsLyingAdult");
            set => SetRunValue("OwnsLyingAdult", value);
        }
        public static bool BoardEffectsApocalypse {
            get => GetRunBool("BoardEffectsApocalypse");
            set => SetRunValue("BoardEffectsApocalypse", value);
        }
        public static bool BoardEffectsEmerald {
            get => GetRunBool("BoardEffectsEmerald");
            set => SetRunValue("BoardEffectsEmerald", value);
        }
        public static bool BoardEffectsEntropy {
            get => GetRunBool("BoardEffectsEntropy");
            set => SetRunValue("BoardEffectsEntropy", value);
        }
        public static bool UnlockedApocalypseBird {
            get => GetSaveBool("UnlockedApocalypseBird");
            set => SetSaveValue("UnlockedApocalypseBird", value);
        }
        public static bool UnlockedJesterOfNihil {
            get => GetSaveBool("UnlockedJesterOfNihil");
            set => SetSaveValue("UnlockedJesterOfNihil", value);
        }
        public static bool UnlockedLyingAdult {
            get => GetSaveBool("UnlockedLyingAdult");
            set => SetSaveValue("UnlockedLyingAdult", value);
        }
        public static bool UnlockedAngela {
            get => GetSaveBool("UnlockedAngela");
            set => SetSaveValue("UnlockedAngela", value);
        }
        public static bool DefeatedApocalypseBoss => AscensionSaveData.Data.conqueredChallenges.Contains(FinalApocalypse.Id);
        /*        public static bool DefeatedJesterBoss
                {
                    get => GetSaveBool("DefeatedJesterBoss");
                    set => SetSaveValue("DefeatedJesterBoss", value);
                }
                public static bool DefeatedEmeraldBoss
                {
                    get => GetSaveBool("DefeatedEmeraldBoss");
                    set => SetSaveValue("DefeatedEmeraldBoss", value);
                }
                public static bool DefeatedSaviourBoss
                {
                    get => GetSaveBool("DefeatedSaviourBoss");
                    set => SetSaveValue("DefeatedSaviourBoss", value);
                }*/
        private static int GetRunInt(string id) => ModdedSaveManager.RunState.GetValueAsInt(LobotomyPlugin.pluginGuid, id);
        private static bool GetRunBool(string id) => ModdedSaveManager.RunState.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetRunValue(string id, object value) => ModdedSaveManager.RunState.SetValue(LobotomyPlugin.pluginGuid, id, value);
        private static bool GetSaveBool(string id) => ModdedSaveManager.SaveData.GetValueAsBoolean(LobotomyPlugin.pluginGuid, id);
        private static void SetSaveValue(string id, object value) => ModdedSaveManager.SaveData.SetValue(LobotomyPlugin.pluginGuid, id, value);
    }
}