using BepInEx;
using BepInEx.Configuration;
using System.IO;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Core
{
    public class AbnormalConfigManager // Taken from GrimoraMod
    {
        private static AbnormalConfigManager wstl_Instance;
        public static AbnormalConfigManager Instance => wstl_Instance ??= new AbnormalConfigManager();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "whistlewind.inscryption.abnormalsigils.cfg"), true);

        private ConfigEntry<bool> Config_EnableMod;
        public bool EnableMod => Config_EnableMod.Value;

        private ConfigEntry<AbnormalAbilityHelper.AbilityGroup> Config_DisableModular;
        public AbnormalAbilityHelper.AbilityGroup DisableModular => Config_DisableModular.Value;

        private ConfigEntry<AbnormalAbilityHelper.AbilityGroup> Config_MakeModular;
        public AbnormalAbilityHelper.AbilityGroup MakeModular => Config_MakeModular.Value;

        internal void BindConfig()
        {
            Config_EnableMod = WstlConfigFile.Bind(
                    "Settings", "Enable Mod", true,
                    new ConfigDescription("Enables the mod's content."));

            Config_DisableModular = WstlConfigFile.Bind(
                    "Settings.Abilities", "Disable Abilities", AbnormalAbilityHelper.AbilityGroup.None,
                    new ConfigDescription(
                        "Disables abilities based on type group, preventing them from being seen in the Rulebook or obtained as totem bases.\n" +
                        "This overrides any settings in Make Modular."));

            Config_MakeModular = WstlConfigFile.Bind(
                    "Settings.Abilities", "Make Modular", AbnormalAbilityHelper.AbilityGroup.None,
                    new ConfigDescription(
                        "Forces abilities to be modular based on type group, meaning they can be found on totem bases."));
        }
    }
}
