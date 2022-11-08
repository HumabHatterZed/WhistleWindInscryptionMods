using BepInEx;
using BepInEx.Configuration;
using System.IO;

namespace WhistleWind.AbnormalSigils.Core
{
    public class AbnormalConfigManager // Taken from GrimoraMod
    {
        private static AbnormalConfigManager wstl_Instance;
        public static AbnormalConfigManager Instance => wstl_Instance ??= new AbnormalConfigManager();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "wstl.inscryption.abnormalsigils.cfg"), true);

        private ConfigEntry<bool> Config_EnableMod;
        public bool EnableMod => Config_EnableMod.Value;

        private ConfigEntry<int> Config_DisableModular;
        public int DisableModular => Config_DisableModular.Value;

        private ConfigEntry<int> Config_MakeModular;
        public int MakeModular => Config_MakeModular.Value;

        internal void BindConfig()
        {
            Config_EnableMod = WstlConfigFile.Bind(
                    "Settings", "ENABLE MOD", true,
                    new ConfigDescription("Enables the mod, allowing its content to be added to the game."));

            Config_DisableModular = WstlConfigFile.Bind(
                    "Settings.Abilities", "DISABLE MOD ABILITIES", 0,
                    new ConfigDescription(
                        "Disables certain types of abilites, preventing them from being seen in the Rulebook or obtained as totem bases.\n" +
                        "This will override 'MAKE ABILITIES MODULAR' if both affect the same group(s).\n" +
                        "For multiple types, set this to the sum of their respective values (eg, Normal + Activated = 3)\n" +
                        "1 - Normal | 2 - Activated | 4 - Special"));

            Config_MakeModular = WstlConfigFile.Bind(
                    "Settings.Abilities", "MAKE MOD ABILITIES MODULAR", 0,
                    new ConfigDescription(
                        "Makes certain types of abilites modular, making them obtainable as totem bases.\n" +
                        "For multiple types, set this to the sum of their respective values (eg, Normal + Activated = 3)\n" +
                        "1 - Normal | 2 - Activated | 4 - Special"));
        }
    }
}
