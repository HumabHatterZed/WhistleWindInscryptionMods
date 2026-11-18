using BepInEx;
using BepInEx.Configuration;
using DiskCardGame;
using System.IO;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core {
    public static class LobotomyConfigManager // Taken from GrimoraMod
    {
        private static readonly ConfigFile WstlConfigFile = new(Path.Combine(Paths.ConfigPath, "whistlewind.inscryption.lobotomycorp.cfg"), true);

        #region Config
        private static ConfigEntry<bool> _configModEnabled;
        public static bool ModEnabled => _configModEnabled.Value;

        private static ConfigEntry<bool> _configFoundInGBC;
        public static bool GBCPacks => _configFoundInGBC.Value;

        private static ConfigEntry<bool> _configReskinSigils;
        public static bool ReskinSigils => _configReskinSigils.Value;

        private static ConfigEntry<bool> _configSpecialsInRulebook;
        public static bool RevealSpecials => _configSpecialsInRulebook.Value;

        #region Config.Cards

        private static ConfigEntry<LobotomyCardManager.RiskLevel> _configNoRisk;
        public static LobotomyCardManager.RiskLevel NoRisk => _configNoRisk.Value;

        private static ConfigEntry<bool> _configNoDonators;
        public static bool NoDonators => _configNoDonators.Value;

        private static ConfigEntry<bool> _configNoRuina;
        public static bool NoRuina => _configNoRuina.Value;

        #endregion

        #endregion

        #region Gameplay
        private static ConfigEntry<int> _configStarterDeckSize;
        public static int StarterDeckSize => _configStarterDeckSize.Value;

        private static ConfigEntry<bool> _configEventFlags;
        public static bool EventFlags => _configEventFlags.Value;

        private static ConfigEntry<bool> _configCustomBosses;
        public static bool CustomBosses => _configCustomBosses.Value;

        #region Gameplay.Nodes

        private static ConfigEntry<bool> _configNoBox;
        public static bool NoBox => _configNoBox.Value;

        private static ConfigEntry<bool> _configNoSefirot;
        public static bool NoSefirot => _configNoSefirot.Value;

        private static ConfigEntry<bool> _configNoEvents;
        public static bool NoEvents => _configNoEvents.Value;

        #endregion

        #region Gameplay.Other

        private static ConfigEntry<int> _configBlessings;
        public static int NumOfBlessings => _configBlessings.Value;

        internal static ConfigEntry<bool> _configHasSeenHim;
        public static bool HasSeenHim => _configHasSeenHim.Value;

        #endregion
        #endregion

        internal static void BindConfig() {
            _configModEnabled = WstlConfigFile.Bind(
                "Config", "Enable", true,
                new ConfigDescription("Enables this mod's content. If false, content will still load but be unavailable through normal means."));

            _configFoundInGBC = WstlConfigFile.Bind(
                "Config", "GBC Packs", true,
                new ConfigDescription("Allows some cards to be obtainable in Act 2 card packs."));

            _configReskinSigils = WstlConfigFile.Bind(
                "Config", "Reskin Sigils", true,
                new ConfigDescription("Changes the names and icons of Sniper and Sentry."));

            _configSpecialsInRulebook = WstlConfigFile.Bind(
                "Config", "Special Abilities in Rulebook", false,
                new ConfigDescription("Adds Rulebook entries for hidden abilities, describing their effect and what cards possess it."));

            _configNoRisk = WstlConfigFile.Bind(
                "Config.Cards", "Disable Cards", LobotomyCardManager.RiskLevel.None,
                new ConfigDescription("Removes cards of the specified risk level from the pool of obtainable cards."));

            _configNoDonators = WstlConfigFile.Bind(
                "Config.Cards", "Disable Donators", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nBackward Clock, Il Pianto della Luna, Army in Pink, Ppodae, Parasite Tree, Melting Love, Honoured Monk."));

            _configNoRuina = WstlConfigFile.Bind(
                "Config.Cards", "Disable Ruina", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nMagical Girl C, Price of Silence, Nosferatu, The Road Home, Ozma, Silent Girl."));

            _configNoEvents = WstlConfigFile.Bind(
                "Gameplay", "Disable Events", false,
                new ConfigDescription("Disables special in-game events added by this mod."));

            _configStarterDeckSize = WstlConfigFile.Bind(
                "Gameplay", "Extra Random Cards", 0,
                new ConfigDescription("Adds more cards to the 'Random' starter deck."));

            _configEventFlags = WstlConfigFile.Bind(
                "Gameplay", "Unlock All Events", false,
                new ConfigDescription("Unlocks the event starter decks and challenges for Kaycee's Mod, regardless of whether you've met the requirements."));

            _configCustomBosses = WstlConfigFile.Bind(
                "Gameplay", "Random Bosses", false,
                new ConfigDescription("KCM ONLY - This mod's bosses can be randomly encountered at the end of the first 3 regions."));

            _configNoBox = WstlConfigFile.Bind(
                "Gameplay.Nodes", "Disable Choice Node", false,
                new ConfigDescription("Prevents the abnormal card choice node from appearing."));

            _configNoSefirot = WstlConfigFile.Bind(
                "Gameplay.Nodes", "Disable Sefirot Node", false,
                new ConfigDescription("Prevents the sefirot card choice node from appearing."));

            _configBlessings = WstlConfigFile.Bind("Gameplay.Other", "Blessings", 0);
            _configHasSeenHim = WstlConfigFile.Bind("Gameplay.Other", "Blessed", false);
        }
        public static void UpdateBlessings(int value) {
            _configBlessings.Value += value;
            Log.LogDebug($"The Clock is now at [{NumOfBlessings}]");
        }
        public static void SetBlessings(int value) {
            _configBlessings.Value = value;
            Log.LogDebug($"The Clock is now at [{NumOfBlessings}]");
        }
        public static void SetHasSeenHim() {
            _configHasSeenHim.Value = true;
        }

        public static bool ChallengeIsActive(AscensionChallenge challenge) {
            if (SaveFile.IsAscension) {
                return AscensionSaveData.Data.ChallengeIsActive(challenge);
            }

            return false;
        }
    }
}