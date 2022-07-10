using InscryptionAPI;
using BepInEx;
using BepInEx.Configuration;
using DiskCardGame;
using System.Collections;
using System.IO;
using UnityEngine;
using static WhistleWindLobotomyMod.WstlPlugin;

namespace WhistleWindLobotomyMod
{
    public class ConfigUtils // Taken from GrimoraMod
    {
        private static ConfigUtils wstl_Instance;
        public static ConfigUtils Instance => wstl_Instance ??= new ConfigUtils();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "wstl.inscryption.lobotomyCorpMod.cfg"), true);
        /* May move dialogue bools to separate config eventually, idk
        private readonly ConfigFile WstlValuesFile = new(
            Path.Combine(Paths.ConfigPath, "Wstl.LobotomyCorpModValues.cfg"), true);
        */
        private ConfigEntry<bool> Config_ModEnabled;
        public bool ModEnabled => Config_ModEnabled.Value;

        private ConfigEntry<bool> Config_SpecialsInRulebook;
        public bool RevealSpecials => Config_SpecialsInRulebook.Value;

        private ConfigEntry<bool> Config_WhiteNightInRulebook;
        public bool RevealWhiteNight => Config_WhiteNightInRulebook.Value;

        private ConfigEntry<bool> Config_AllModular;
        public bool AllModular => Config_AllModular.Value;

        private ConfigEntry<bool> Config_NoDonators;
        public bool NoDonators => Config_NoDonators.Value;

        //private ConfigEntry<bool> Config_NoRuina;
        //public bool NoRuina => Config_NoRuina.Value;

        private ConfigEntry<int> Config_Blessings;
        public int NumOfBlessings => Config_Blessings.Value;

        internal void BindConfig()
        {
            Config_ModEnabled = WstlConfigFile.Bind(
                    pluginName, "ENABLE MOD", true,
                    new ConfigDescription("Enables the mod when set to true."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                    pluginName, "HIDDEN ABILITIES IN RULEBOOK", false,
                    new ConfigDescription("Adds hidden special abilities to the rulebook when set to true."));

            Config_WhiteNightInRulebook = WstlConfigFile.Bind(
                    pluginName, "REVEAL DESCRIPTIONS", false,
                    new ConfigDescription("Changes the descriptions of the abilities Apostle, True Saviour, and Confession and Pentinence."));

            Config_AllModular = WstlConfigFile.Bind(
                    pluginName, "ALL MODULAR", false,
                    new ConfigDescription("Makes -most- custom abilities modular, meaning they can be found on totem bases and on cards from the den trial."));

            Config_NoDonators = WstlConfigFile.Bind(
                pluginName, "NO DONATORS", false,
                new ConfigDescription("Prevents Donator-class abnormalities from being obtainable in-game (Backward Clock... Honoured Monk)"));

            //Config_NoDonators = WstlConfigFile.Bind(
            //    pluginName, "NO RUINA", false,
            //    new ConfigDescription("Prevents abnormalities from the Library of Ruina expansion from being obtainable in-game."));

            Config_Blessings = WstlConfigFile.Bind(
                    pluginName, "NUMBER OF BLESSINGS", 0);
        }
        /*
        internal void BindValues()
        {

        }
        */
        public void UpdateBlessings(int value)
        {
            Config_Blessings.Value += value;
        }
        public void SetBlessings(int value)
        {
            Config_Blessings.Value = value;
        }
    }
}
