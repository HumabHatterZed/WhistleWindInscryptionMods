using BepInEx;
using BepInEx.Configuration;
using System.IO;
using WhistleWindLobotomyMod.Core.Helpers;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core
{
    public class LobotomyConfigManager // Taken from GrimoraMod
    {
        private static LobotomyConfigManager wstl_Instance;
        public static LobotomyConfigManager Instance => wstl_Instance ??= new LobotomyConfigManager();

        private readonly ConfigFile WstlConfigFile = new(
            Path.Combine(Paths.ConfigPath, "whistlewind.inscryption.lobotomycorp.cfg"), true);

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

        private ConfigEntry<int> Config_StarterDeckSize;
        public int StarterDeckSize => Config_StarterDeckSize.Value;

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
                "Config", "Enable", true,
                new ConfigDescription("Enables this mod's content."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                "Config", "Special Abilities in Rulebook", false,
                new ConfigDescription("Adds Rulebook entries for hidden abilities, describing their effect and what card possesses it."));

            Config_NoRisk = WstlConfigFile.Bind(
                "Config.Cards", "Disable Cards", LobotomyCardHelper.RiskLevel.None,
                new ConfigDescription("Removes cards of the specified risk level from the pool of obtainable cards."));

            Config_NoDonators = WstlConfigFile.Bind(
                "Config.Cards", "Disable Donators", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nBackward Clock, Il Pianto della Luna, Army in Pink, Ppodae, Parasite Tree, Melting Love, Honoured Monk."));

            Config_NoRuina = WstlConfigFile.Bind(
                "Config.Cards", "DISABLE RUINA", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nMagical Girl C, Price of Silence, Nosferatu, The Road Home, Ozma, Silent Girl."));

            Config_StarterDeck = WstlConfigFile.Bind(
                "Gameplay", "Starter Deck", 0,
                new ConfigDescription("PART 1 ONLY - Replaces your starting cards with one of this mod's custom decks." +
                "\n0 - Default Deck" +
                "\n1 - Random Mod Deck" +
                "\n2 - 3 Random Mod Cards" +
                "\n3 - One Sin, Fairy Festival, Old Lady" +
                "\n4 - Scorched Girl, Laetitia, Child of the Galaxy" +
                "\n5 - We Can Change Anything, All-Around Helper, Singing Machine" +
                "\n6 - Today's Shy Look, Pinocchio/Mirror of Adjustment, Behaviour Adjustment" +
                "\n7 - Beauty and the Beast, Void Dream, Queen Bee" +
                "\n8 - Fragment of the Universe, Skin Prophecy, Plague Doctor" +
                "\n9 - Bloodbath, Burrowing Heaven, The Snow Queen" +
                "\n10 - The Road Home/Laetitia, Warm-Hearted-Woodsman, Wisdom Scarecrow, Ozma/Snow White's Apple" +
                "\n11 - Magical Girl S, Magical Girl H, Magical D, Magical Girl C/Void Dream" +
                "\n12 - Punishing Bird, Big Bird, Judgement Bird"));

            Config_StarterDeckSize = WstlConfigFile.Bind(
                "Gameplay", "Extra Random Cards", 0,
                new ConfigDescription("Adds more cards to the 3 Random Mod Cards starter decks in Part 1 and Kaycee's Mod."));

            Config_AbnormalBosses = WstlConfigFile.Bind(
                "Gameplay.Challenges", "Abnormal Bosses", false,
                new ConfigDescription("PART 1 ONLY - Bosses will only use Abnormality cards."));

            Config_AbnormalBattles = WstlConfigFile.Bind(
                "Gameplay.Challenges", "Abnormal Encounters", false,
                new ConfigDescription("PART 1 ONLY - All regular battles will only use Abnormality cards."));

            Config_MiracleWorker = WstlConfigFile.Bind(
                "Gameplay.Challenges", "Miracle Worker", false,
                new ConfigDescription("PART 1 ONLY - Leshy will play Plague Doctor during regular battles. Beware the Clock."));

            Config_BetterRareChances = WstlConfigFile.Bind(
                "Gameplay.Cheats", "Better Rare Chances", false,
                new ConfigDescription("PART 1 ONLY - Raises the chance of getting a Rare card from the abnormal choice node."));

            Config_NoEvents = WstlConfigFile.Bind(
                "Gameplay", "Disable Events", false,
                new ConfigDescription("Disables special in-game events added by this mod."));

            Config_NoBox = WstlConfigFile.Bind(
                "Gameplay.Nodes", "Disable Choice Node", false,
                new ConfigDescription("Prevents the abnormal card choice node from appearing."));

            Config_NoSefirot = WstlConfigFile.Bind(
                "Gameplay.Nodes", "Disable Sefirot Node", false,
                new ConfigDescription("Prevents the sefirot card choice node from appearing."));

            Config_BoxStart = WstlConfigFile.Bind(
                "Gameplay.Nodes", "Choice Node at Start", false,
                new ConfigDescription("Each new region will have an abnormal choice node at its start."));

            Config_SefirotChoiceAtStart = WstlConfigFile.Bind(
                "Gameplay.Nodes", "Sefirot Node at Start", false,
                new ConfigDescription("Each new region will have a sephirah choice node at its start."));

            Config_Blessings = WstlConfigFile.Bind(
                "Gameplay.Other", "Blessings", 0);

            Config_EventFlags = WstlConfigFile.Bind(
                "Gameplay.Other", "Events", 0);
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