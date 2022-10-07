using InscryptionAPI;
using InscryptionAPI.Card;
using BepInEx;
using BepInEx.Configuration;
using DiskCardGame;
using System.Collections;
using System.IO;
using UnityEngine;
using static WhistleWindLobotomyMod.WstlPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public class ConfigManager // Taken from GrimoraMod
    {
        private static ConfigManager wstl_Instance;
        public static ConfigManager Instance => wstl_Instance ??= new ConfigManager();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "wstl.inscryption.lobotomycorp.cfg"), true);
        /* May move dialogue bools to separate config eventually, idk
        private readonly ConfigFile WstlValuesFile = new(
            Path.Combine(Paths.ConfigPath, "Wstl.LobotomyCorpModValues.cfg"), true);
        */
        private ConfigEntry<bool> Config_ModEnabled;
        public bool ModEnabled => Config_ModEnabled.Value;

        // Special
        private ConfigEntry<bool> Config_NoDonators;
        public bool NoDonators => Config_NoDonators.Value;

        private ConfigEntry<bool> Config_BoxStart;
        public bool BoxStart => Config_BoxStart.Value;

        private ConfigEntry<bool> Config_AllModular;
        public bool AllModular => Config_AllModular.Value;

        // Challenge equivalents
        private ConfigEntry<bool> Config_AbnormalBosses;
        public bool AbnormalBosses => Config_AbnormalBosses.Value;

        private ConfigEntry<bool> Config_AbnormalBattles;
        public bool AbnormalBattles => Config_AbnormalBattles.Value;

        private ConfigEntry<bool> Config_MiracleWorker;
        public bool MiracleWorker => Config_MiracleWorker.Value;

        private ConfigEntry<bool> Config_BetterRareChances;
        public bool BetterRareChances => Config_BetterRareChances.Value;

        // Rulebook
        private ConfigEntry<bool> Config_SpecialsInRulebook;
        public bool RevealSpecials => Config_SpecialsInRulebook.Value;

        private ConfigEntry<bool> Config_WhiteNightInRulebook;
        public bool RevealWhiteNight => Config_WhiteNightInRulebook.Value;

        //private ConfigEntry<bool> Config_NoRuina;
        //public bool NoRuina => Config_NoRuina.Value;

        private ConfigEntry<int> Config_Blessings;
        public int NumOfBlessings => Config_Blessings.Value;

        internal void BindConfig()
        {
            Config_ModEnabled = WstlConfigFile.Bind(
                    pluginName, "ENABLE MOD", true,
                    new ConfigDescription("Enables the mod when set to true."));

            Config_NoDonators = WstlConfigFile.Bind(
                pluginName, "NO DONATORS", false,
                new ConfigDescription("Prevents 7 abnormalities from being obtainable in-game (Backward Clock through Honoured Monk on the ReadMe)"));

            Config_BoxStart = WstlConfigFile.Bind(
                    pluginName, "CARD CHOICE AT START", false,
                    new ConfigDescription("Adds an Abnormality card choice node to the start of each region."));

            Config_AllModular = WstlConfigFile.Bind(
                    pluginName, "ALL MODULAR", false,
                    new ConfigDescription("Makes custom abilities modular, meaning they can be found on totem bases and on cards from the den trial."));

            Config_AbnormalBosses = WstlConfigFile.Bind(
                pluginName, "ABNORMAL BOSSES", false,
                new ConfigDescription("PART 1 ONLY - Bosses will only play Abnormality cards."));

            Config_AbnormalBattles = WstlConfigFile.Bind(
                pluginName, "ABNORMAL ENCOUNTERS", false,
                new ConfigDescription("PART 1 ONLY - All regular battles will only use Abnormality cards."));

            Config_MiracleWorker = WstlConfigFile.Bind(
                pluginName, "MIRACLE WORKER", false,
                new ConfigDescription("PART 1 ONLY - Leshy will play Plague Doctor during regular battles. Beware the Clock."));

            Config_BetterRareChances = WstlConfigFile.Bind(
                pluginName, "BETTER RARE CHANCES", false,
                new ConfigDescription("PART 1 ONLY - Raises the chance of getting a Rare card from the abnormal choice node."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                    pluginName, "HIDDEN ABILITIES IN RULEBOOK", false,
                    new ConfigDescription("Adds Rulebook entries for special abilities when set to true."));

            Config_WhiteNightInRulebook = WstlConfigFile.Bind(
                    pluginName, "REVEAL DESCRIPTIONS", false,
                    new ConfigDescription("Changes the descriptions of the abilities Apostle, True Saviour, and Confession and Pentinence."));

            //Config_NoDonators = WstlConfigFile.Bind(
            //    pluginName, "NO RUINA", false,
            //    new ConfigDescription("Prevents abnormalities from the Library of Ruina expansion from being obtainable in-game."));

            Config_Blessings = WstlConfigFile.Bind(
                    pluginName, "NUMBER OF BLESSINGS", 0);
        }
        public void UpdateBlessings(int value)
        {
            Config_Blessings.Value += value;
            Log.LogDebug($"The Clock is now at [{ConfigManager.Instance.NumOfBlessings}]");
        }
        public void SetBlessings(int value)
        {
            Config_Blessings.Value = value;
            Log.LogDebug($"The Clock is now at [{ConfigManager.Instance.NumOfBlessings}]");
        }
    }
}
