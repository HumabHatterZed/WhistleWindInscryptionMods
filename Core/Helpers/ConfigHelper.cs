using System.IO;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using BepInEx;
using BepInEx.Configuration;
using Sirenix.Utilities;
using static WhistleWindLobotomyMod.Plugin;

namespace WhistleWindLobotomyMod
{
    public class ConfigHelper
    {
        private static ConfigHelper wstl_Instance;
        public static ConfigHelper Instance => wstl_Instance ??= new ConfigHelper();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "wstl.LobotomyCorpMod.cfg"), true);
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

        private ConfigEntry<int> Config_Blessings;
        public int NumOfBlessings => Config_Blessings.Value;

        internal void BindConfig()
        {
            Config_ModEnabled = WstlConfigFile.Bind(
                    pluginName, "ENABLE MOD", true,
                    new ConfigDescription("Enables the mod when set to true."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                    pluginName, "SPECIAL ABILITIES IN RULEBOOK", false,
                    new ConfigDescription("Adds hidden special abilities to the rulebook when set to true."));

            Config_WhiteNightInRulebook = WstlConfigFile.Bind(
                    pluginName, "REVEAL DESCRIPTIONS", false,
                    new ConfigDescription("Changes the descriptions of the abilities Apostle, True Saviour, and Confession and Pentinence."));

            Config_AllModular = WstlConfigFile.Bind(
                    pluginName, "ALL MODULAR", false,
                    new ConfigDescription("Makes all added abilities modular. This means they are available as totems and den trial sigils."));

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
            Plugin.Log.LogInfo($"The clock now strikes: [{ConfigHelper.Instance.NumOfBlessings}]");
        }
    }
}
