using BepInEx;
using BepInEx.Configuration;
using System.IO;
using WhistleWindLobotomyMod.Core.Helpers;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core
{
    public class ConfigManager // Taken from GrimoraMod
    {
        private static ConfigManager wstl_Instance;
        public static ConfigManager Instance => wstl_Instance ??= new ConfigManager();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "wstl.inscryption.lobotomycorp.cfg"), true);

        #region Config

        private ConfigEntry<bool> Config_ModEnabled;
        public bool ModEnabled => Config_ModEnabled.Value;

        private ConfigEntry<bool> Config_SpecialsInRulebook;
        public bool RevealSpecials => Config_SpecialsInRulebook.Value;

        #region Config.Cards

        private ConfigEntry<LobotomyCardHelper.RiskLevel> Config_NoRisk;
        public LobotomyCardHelper.RiskLevel NoRisk => Config_NoRisk.Value;

        private ConfigEntry<bool> Config_NoDonators;
        public bool NoDonators => Config_NoDonators.Value;

        private ConfigEntry<bool> Config_NoRuina;
        public bool NoRuina => Config_NoRuina.Value;

        #endregion

        #endregion

        #region Gameplay

        private ConfigEntry<int> Config_StarterDeck;
        public int StarterDeck => Config_StarterDeck.Value;

        #region Gameplay.Challenges

        private ConfigEntry<bool> Config_AbnormalBosses;
        public bool AbnormalBosses => Config_AbnormalBosses.Value;

        private ConfigEntry<bool> Config_AbnormalBattles;
        public bool AbnormalBattles => Config_AbnormalBattles.Value;

        private ConfigEntry<bool> Config_MiracleWorker;
        public bool MiracleWorker => Config_MiracleWorker.Value;

        #endregion

        #region Gameplay.Cheats

        private ConfigEntry<bool> Config_BetterRareChances;
        public bool BetterRareChances => Config_BetterRareChances.Value;

        #endregion

        #region Gameplay.Other
        private ConfigEntry<bool> Config_BoxStart;
        public bool BoxStart => Config_BoxStart.Value;

        private ConfigEntry<bool> Config_NoBox;
        public bool NoBox => Config_NoBox.Value;

        private ConfigEntry<bool> Config_SefirotChoiceAtStart;
        public bool SefirotChoiceAtStart => Config_SefirotChoiceAtStart.Value;

        private ConfigEntry<bool> Config_NoSefirot;
        public bool NoSefirot => Config_NoSefirot.Value;

        private ConfigEntry<bool> Config_NoEvents;
        public bool NoEvents => Config_NoEvents.Value;

        #endregion

        #endregion

        #region Secrets

        private ConfigEntry<int> Config_Blessings;
        public int NumOfBlessings => Config_Blessings.Value;

        private ConfigEntry<int> Config_EventFlags;
        public int EventFlags => Config_EventFlags.Value;

        #endregion

        internal void BindConfig()
        {
            Config_ModEnabled = WstlConfigFile.Bind(
                "Config", "ENABLE MOD", true,
                new ConfigDescription("Allows this mod's content to be loaded."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                "Config", "SPECIALS IN RULEBOOK", false,
                new ConfigDescription("Adds Rulebook entries for special abilities describing their behaviour and what cards possess them."));

            Config_NoRisk = WstlConfigFile.Bind(
                "Config.Cards", "DISABLE CARDS", LobotomyCardHelper.RiskLevel.None,
                new ConfigDescription("Removes cards of the specified risk level from the pool of obtainable cards."));

            Config_NoDonators = WstlConfigFile.Bind(
                "Config.Cards", "DISABLE DONATORS", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nBackward Clock, Il Pianto della Luna, Army in Pink, Ppodae, Parasite Tree, Melting Love, Honoured Monk."));

            Config_NoRuina = WstlConfigFile.Bind(
                "Config.Cards", "DISABLE RUINA", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nMagical Girl C, Price of Silence, Nosferatu, The Road Home, Ozma, Silent Girl."));

            Config_StarterDeck = WstlConfigFile.Bind(
                "Gameplay", "STARTER DECK", 0,
                new ConfigDescription("PART 1 ONLY - Replaces your starting cards with one of this mod's custom decks." +
                "\n0 - Default Vanilla Deck" +
                "\n1 - One Sin, Fairy Festival, Old Lady" +
                "\n2 - Scorched Girl, Laetitia, Child of the Galaxy" +
                "\n3 - We Can Change Anything, All-Around Helper, Singing Machine" +
                "\n4 - Squirrel" +
                "\n5 - The Road Home / Laetitia, Warm-Hearted-Woodsman, Wisdom Scarecrow" +
                "\n6 - Magical Girl H, Magical D, Magical Girl C / Magical Girl S" +
                "\n7 - Punishing Bird, Big Bird, Judgement Bird" +
                "\n8 - 3 Random Mod Cards" +
                "\n9 - Random Mod Deck"));

            Config_AbnormalBosses = WstlConfigFile.Bind(
                "Gameplay.Challenges", "ABNORMAL BOSSES", false,
                new ConfigDescription("PART 1 ONLY - Bosses will only use Abnormality cards."));

            Config_AbnormalBattles = WstlConfigFile.Bind(
                "Gameplay.Challenges", "ABNORMAL ENCOUNTERS", false,
                new ConfigDescription("PART 1 ONLY - All regular battles will only use Abnormality cards."));

            Config_MiracleWorker = WstlConfigFile.Bind(
                "Gameplay.Challenges", "MIRACLE WORKER", false,
                new ConfigDescription("PART 1 ONLY - Leshy will play Plague Doctor during regular battles. Beware the Clock."));

            Config_BetterRareChances = WstlConfigFile.Bind(
                "Gameplay.Cheats", "BETTER RARE CHANCES", false,
                new ConfigDescription("PART 1 ONLY - Raises the chance of getting a Rare card from the abnormal choice node."));

            Config_NoEvents = WstlConfigFile.Bind(
                "Gameplay.Other", "DISABLE EVENTS", false,
                new ConfigDescription("Disables special in-game events added by this mod."));

            Config_NoBox = WstlConfigFile.Bind(
                "Gameplay.Other", "DISABLE ABNORMAL NODE", false,
                new ConfigDescription("Prevents the abnormal card choice node from appearing."));

            Config_NoSefirot = WstlConfigFile.Bind(
                "Gameplay.Other", "DISABLE SEFIROT NODE", false,
                new ConfigDescription("Prevents the sefirot card choice node from appearing."));

            Config_BoxStart = WstlConfigFile.Bind(
                "Gameplay.Other", "ABNORMAL NODE AT START", false,
                new ConfigDescription("Each new region will have an abnormal choice node at its start."));

            Config_SefirotChoiceAtStart = WstlConfigFile.Bind(
                "Gameplay.Other", "SEFIROT NODE AT START", false,
                new ConfigDescription("Each new region will have a sephirah choice node at its start."));

            Config_Blessings = WstlConfigFile.Bind(
                "Secrets", "BLESSINGS", 0);

            Config_EventFlags = WstlConfigFile.Bind(
                "Secrets", "EVENTS", 0);
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