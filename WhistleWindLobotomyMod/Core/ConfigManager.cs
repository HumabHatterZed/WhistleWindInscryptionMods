using BepInEx;
using BepInEx.Configuration;
using System.IO;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core
{
    public class ConfigManager // Taken from GrimoraMod
    {
        private static ConfigManager wstl_Instance;
        public static ConfigManager Instance => wstl_Instance ??= new ConfigManager();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "wstl.inscryption.lobotomycorp.cfg"), true);

        // Enable Mod
        private ConfigEntry<bool> Config_ModEnabled;
        public bool ModEnabled => Config_ModEnabled.Value;

        // Disable cards
        private ConfigEntry<bool> Config_NoDonators;
        public bool NoDonators => Config_NoDonators.Value;

        private ConfigEntry<bool> Config_NoRuina;
        public bool NoRuina => Config_NoRuina.Value;

        // Special
        private ConfigEntry<bool> Config_BoxStart;
        public bool BoxStart => Config_BoxStart.Value;

        // Challenge equivalents
        private ConfigEntry<bool> Config_AbnormalBosses;
        public bool AbnormalBosses => Config_AbnormalBosses.Value;

        private ConfigEntry<bool> Config_AbnormalBattles;
        public bool AbnormalBattles => Config_AbnormalBattles.Value;

        private ConfigEntry<bool> Config_MiracleWorker;
        public bool MiracleWorker => Config_MiracleWorker.Value;

        private ConfigEntry<bool> Config_BetterRareChances;
        public bool BetterRareChances => Config_BetterRareChances.Value;

        // Specials in Rulebook
        private ConfigEntry<bool> Config_SpecialsInRulebook;
        public bool RevealSpecials => Config_SpecialsInRulebook.Value;

        // Internal
        private ConfigEntry<int> Config_Blessings;
        public int NumOfBlessings => Config_Blessings.Value;

        internal void BindConfig()
        {
            Config_ModEnabled = WstlConfigFile.Bind(
                "Settings", "ENABLE MOD", true,
                new ConfigDescription("Enables the mod, allowing its content to be added to the game."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                "Settings.Abilities", "SPECIALS IN RULEBOOK", false,
                new ConfigDescription("Adds Rulebook entries for special abilities describing their behaviour and what cards possess them."));

            Config_NoDonators = WstlConfigFile.Bind(
                "Settings.Cards", "NO DONATORS", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards.\nBackward Clock, Honoured Monk."));

            Config_NoRuina = WstlConfigFile.Bind(
                "Settings.Cards", "NO RUINA", false,
                new ConfigDescription("Removes Library of Ruina cards from the pool of obtainable cards."));

            Config_AbnormalBosses = WstlConfigFile.Bind(
                "Challenges", "ABNORMAL BOSSES", false,
                new ConfigDescription("PART 1 ONLY - Bosses will only play Abnormality cards."));

            Config_AbnormalBattles = WstlConfigFile.Bind(
                "Challenges", "ABNORMAL ENCOUNTERS", false,
                new ConfigDescription("PART 1 ONLY - All regular battles will only use Abnormality cards."));

            Config_MiracleWorker = WstlConfigFile.Bind(
                "Challenges", "MIRACLE WORKER", false,
                new ConfigDescription("PART 1 ONLY - Leshy will play Plague Doctor during regular battles. Beware the Clock."));

            Config_BetterRareChances = WstlConfigFile.Bind(
                "Cheats", "BETTER RARE CHANCES", false,
                new ConfigDescription("PART 1 ONLY - Raises the chance of getting a Rare card from the abnormal choice node."));

            Config_BoxStart = WstlConfigFile.Bind(
                "Gameplay", "CARD CHOICE AT START", false,
                new ConfigDescription("Each new region will have an abnormal choice node at its start."));

            Config_Blessings = WstlConfigFile.Bind(
                "Gameplay", "NUMBER OF BLESSINGS", 0);
        }
        public void UpdateBlessings(int value)
        {
            Config_Blessings.Value += value;
            Log.LogDebug($"The Clock is now at [{Instance.NumOfBlessings}]");
        }
        public void SetBlessings(int value)
        {
            Config_Blessings.Value = value;
            Log.LogDebug($"The Clock is now at [{Instance.NumOfBlessings}]");
        }
    }
}
