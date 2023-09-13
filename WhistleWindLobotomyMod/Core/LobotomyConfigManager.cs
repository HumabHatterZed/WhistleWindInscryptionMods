using BepInEx;
using BepInEx.Configuration;
using System.IO;
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

        private ConfigEntry<bool> Config_FoundInGBC;
        public bool GBCPacks => Config_FoundInGBC.Value;

        private ConfigEntry<bool> Config_SpecialsInRulebook;
        public bool RevealSpecials => Config_SpecialsInRulebook.Value;

        #region Config.Cards

        private ConfigEntry<LobotomyCardManager.RiskLevel> Config_NoRisk;
        public LobotomyCardManager.RiskLevel NoRisk => Config_NoRisk.Value;

        private ConfigEntry<bool> Config_NoDonators;
        public bool NoDonators => Config_NoDonators.Value;

        private ConfigEntry<bool> Config_NoRuina;
        public bool NoRuina => Config_NoRuina.Value;

        #endregion

        #endregion

        #region Gameplay
        private ConfigEntry<int> Config_StarterDeckSize;
        public int StarterDeckSize => Config_StarterDeckSize.Value;

        private ConfigEntry<bool> Config_EventFlags;
        public bool EventFlags => Config_EventFlags.Value;

        private ConfigEntry<bool> Config_CustomBosses;
        public bool CustomBosses => Config_CustomBosses.Value;

        #region Gameplay.Nodes
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

        #region Gameplay.Part1
        private ConfigEntry<int> Config_StarterDeck;
        public int StarterDeck => Config_StarterDeck.Value;

        #region Gameplay.Part1.Challenges
        private ConfigEntry<bool> Config_AbnormalBosses;
        public bool AbnormalBosses => Config_AbnormalBosses.Value;

        private ConfigEntry<bool> Config_AbnormalBattles;
        public bool AbnormalBattles => Config_AbnormalBattles.Value;

        private ConfigEntry<bool> Config_MiracleWorker;
        public bool MiracleWorker => Config_MiracleWorker.Value;

        private ConfigEntry<bool> Config_FinalApocalypse;
        public bool FinalApocalypse => Config_FinalApocalypse.Value;

        private ConfigEntry<bool> Config_FinalJester;
        public bool FinalJester => Config_FinalJester.Value;

        private ConfigEntry<bool> Config_FinalEmerald;
        public bool FinalEmerald => Config_FinalEmerald.Value;

        private ConfigEntry<bool> Config_FinalComing;
        public bool FinalComing => Config_FinalComing.Value;

        #endregion

        #region Gameplay.Part1.Cheats
        private ConfigEntry<bool> Config_BetterRareChances;
        public bool BetterRareChances => Config_BetterRareChances.Value;

        private ConfigEntry<bool> Config_StartApocalypseBird;
        public bool StartApocalypseBird => Config_StartApocalypseBird.Value;

        private ConfigEntry<bool> Config_StartJesterOfNihil;
        public bool StartJesterOfNihil => Config_StartJesterOfNihil.Value;

        private ConfigEntry<bool> Config_StartLyingAdult;
        public bool StartLyingAdult => Config_StartLyingAdult.Value;

        #endregion

        #endregion

        #endregion

        #region Gameplay.Other

        private ConfigEntry<int> Config_Blessings;
        public int NumOfBlessings => Config_Blessings.Value;

        internal ConfigEntry<bool> Config_HasSeenHim;
        public bool HasSeenHim => Config_HasSeenHim.Value;

        #endregion

        internal void BindConfig()
        {
            Config_ModEnabled = WstlConfigFile.Bind(
                "Config", "Enable", true,
                new ConfigDescription("Enables this mod's content."));

            Config_FoundInGBC = WstlConfigFile.Bind(
                "Config", "GBC Packs", true,
                new ConfigDescription("Allows some cards to be obtainable in Act 2 card packs."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                "Config", "Special Abilities in Rulebook", false,
                new ConfigDescription("Adds Rulebook entries for hidden abilities, describing their effect and what card possesses it."));

            Config_NoRisk = WstlConfigFile.Bind(
                "Config.Cards", "Disable Cards", LobotomyCardManager.RiskLevel.None,
                new ConfigDescription("Removes cards of the specified risk level from the pool of obtainable cards."));

            Config_NoDonators = WstlConfigFile.Bind(
                "Config.Cards", "Disable Donators", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nBackward Clock, Il Pianto della Luna, Army in Pink, Ppodae, Parasite Tree, Melting Love, Honoured Monk."));

            Config_NoRuina = WstlConfigFile.Bind(
                "Config.Cards", "Disable Ruina", false,
                new ConfigDescription("Removes the following abnormalities from the pool of obtainable cards:" +
                "\nMagical Girl C, Price of Silence, Nosferatu, The Road Home, Ozma, Silent Girl."));

            Config_NoEvents = WstlConfigFile.Bind(
                "Gameplay", "Disable Events", false,
                new ConfigDescription("Disables special in-game events added by this mod."));

            Config_StarterDeckSize = WstlConfigFile.Bind(
                "Gameplay", "Extra Random Cards", 0,
                new ConfigDescription("Adds more cards to the 3 Random Mod Cards starter decks in Part 1 and Kaycee's Mod."));

            Config_EventFlags = WstlConfigFile.Bind(
                "Gameplay", "Unlock All Events", false,
                new ConfigDescription("Unlocks the event starter decks and challenges for Kaycee's Mod, regardless of whether you've met the requirements."));

            Config_CustomBosses = WstlConfigFile.Bind(
                "Gameplay", "Random Bosses", false,
                new ConfigDescription("KCM ONLY - This mod's bosses can be randomly encountered at the end of the first 3 regions."));

            Config_StarterDeck = WstlConfigFile.Bind(
                "Gameplay.Part1", "Starter Deck", 0,
                new ConfigDescription("Replaces your starting cards with one of this mod's custom decks." +
                "\n0 - Default Deck" +
                "\n1 - Random Mod Deck (3-12)" +
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

            Config_AbnormalBosses = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Abnormal Bosses", false,
                new ConfigDescription("Bosses will only use Abnormality cards."));

            Config_AbnormalBattles = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Abnormal Encounters", false,
                new ConfigDescription("All regular battles will only use Abnormality cards."));

            Config_MiracleWorker = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Miracle Worker", false,
                new ConfigDescription("Leshy will play Plague Doctor during regular battles. Beware the Clock."));

            Config_FinalApocalypse = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Final Apocalypse", false,
                new ConfigDescription("The Beast boss will be stronger and guaranteed to appear."));

/*            Config_FinalJester = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Final Laugh", false,
                new ConfigDescription("The Fool boss will be stronger and guaranteed to appear."));

            Config_FinalEmerald = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Final Trick", false,
                new ConfigDescription("The Adult boss will be stronger and guaranteed to appear."));

            Config_FinalComing = WstlConfigFile.Bind(
                "Gameplay.Part1.Challenges", "Final Coming", false,
                new ConfigDescription("The Saviour boss will be stronger and guaranteed to appear."));*/

            Config_BetterRareChances = WstlConfigFile.Bind(
                "Gameplay.Part1.Cheats", "Better Rare Chances", false,
                new ConfigDescription("Raises the chance of getting a Rare card from the abnormal choice node."));

            Config_StartApocalypseBird = WstlConfigFile.Bind(
                "Gameplay.Part1.Cheats", "Start with a Beast", false,
                new ConfigDescription("Start your run with Apocalypse Bird in your deck."));

            Config_StartJesterOfNihil = WstlConfigFile.Bind(
                "Gameplay.Part1.Cheats", "Start with a Fool", false,
                new ConfigDescription("Start your run with Jester of Nihil in your deck."));

            Config_StartLyingAdult = WstlConfigFile.Bind(
                "Gameplay.Part1.Cheats", "Start with a Liar", false,
                new ConfigDescription("Start your run with Adult Who Tells Lies in your deck."));

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

            Config_HasSeenHim = WstlConfigFile.Bind(
                "Gameplay.Other", "Blessed", false);
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
        public void SetHasSeenHim()
        {
            Config_HasSeenHim.Value = true;
        }
    }
}