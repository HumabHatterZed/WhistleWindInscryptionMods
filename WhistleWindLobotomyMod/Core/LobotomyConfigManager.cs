using BepInEx;
using BepInEx.Configuration;
using DiskCardGame;
using System.IO;
using WhistleWindLobotomyMod.Challenges;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core {
    public static class LobotomyConfigManager // Taken from GrimoraMod
    {
        private static readonly ConfigFile WstlConfigFile = new(Path.Combine(Paths.ConfigPath, "whistlewind.inscryption.lobotomycorp.cfg"), true);

        #region Config
        private static ConfigEntry<bool> Config_ModEnabled;
        public static bool ModEnabled => Config_ModEnabled.Value;

        private static ConfigEntry<bool> Config_FoundInGBC;
        public static bool GBCPacks => Config_FoundInGBC.Value;

        private static ConfigEntry<bool> Config_ReskinSigils;
        public static bool ReskinSigils => Config_ReskinSigils.Value;

        private static ConfigEntry<bool> Config_SpecialsInRulebook;
        public static bool RevealSpecials => Config_SpecialsInRulebook.Value;

        #region Config.Cards

        private static ConfigEntry<LobotomyCardManager.RiskLevel> Config_NoRisk;
        public static LobotomyCardManager.RiskLevel NoRisk => Config_NoRisk.Value;

        private static ConfigEntry<bool> Config_NoDonators;
        public static bool NoDonators => Config_NoDonators.Value;

        private static ConfigEntry<bool> Config_NoRuina;
        public static bool NoRuina => Config_NoRuina.Value;

        #endregion

        #endregion

        #region Gameplay
        private static ConfigEntry<int> Config_StarterDeckSize;
        public static int StarterDeckSize => Config_StarterDeckSize.Value;

        private static ConfigEntry<bool> Config_EventFlags;
        public static bool EventFlags => Config_EventFlags.Value;

        private static ConfigEntry<bool> Config_CustomBosses;
        public static bool CustomBosses => Config_CustomBosses.Value;

        #region Gameplay.Nodes
        private static ConfigEntry<bool> Config_BoxStart;
        public static bool BoxStart => Config_BoxStart.Value;

        private static ConfigEntry<bool> Config_NoBox;
        public static bool NoBox => Config_NoBox.Value;

        private static ConfigEntry<bool> Config_SefirotChoiceAtStart;
        public static bool SefirotChoiceAtStart => Config_SefirotChoiceAtStart.Value;

        private static ConfigEntry<bool> Config_NoSefirot;
        public static bool NoSefirot => Config_NoSefirot.Value;

        private static ConfigEntry<bool> Config_NoEvents;
        public static bool NoEvents => Config_NoEvents.Value;

        #endregion

        #region Gameplay.Part1
        private static ConfigEntry<int> Config_StarterDeck;
        public static int StarterDeck => Config_StarterDeck.Value;

        #region Gameplay.Part1.Challenges
        private static ConfigEntry<bool> _configNoTime;
        private static ConfigEntry<bool> _configNoRares;
        private static ConfigEntry<bool> _configSoulbound;
        private static ConfigEntry<bool> _configApostleBears;
        private static ConfigEntry<bool> _configMeltdown;
        private static ConfigEntry<bool> _configMiracleWorker;

        private static ConfigEntry<bool> _configAbnormalBosses;
        private static ConfigEntry<bool> _configAbnormalBattles;
        private static ConfigEntry<bool> _configOrdealBosses;
        private static ConfigEntry<bool> _configOrdealBattles;

        private static ConfigEntry<bool> _configFinalOrdeal;
        private static ConfigEntry<bool> _configFinalBird;
        //private static ConfigEntry<bool> _configFinalLiar;
        //private static ConfigEntry<bool> _configFinalFool;
        //private static ConfigEntry<bool> _configFinalGod;

        private static ConfigEntry<bool> _configStartBird;
        private static ConfigEntry<bool> _configStartLiar;
        private static ConfigEntry<bool> _configStartFool;
        private static ConfigEntry<bool> _configMoreRares;

        private static bool ConfigNoTime => _configNoTime.Value;
        private static bool ConfigNoRares => _configNoRares.Value;
        private static bool ConfigSoulbound => _configSoulbound.Value;
        private static bool ConfigApostleBears => _configApostleBears.Value;
        private static bool ConfigMeltdown => _configMeltdown.Value;
        private static bool ConfigMiracleWorker => _configMiracleWorker.Value;

        private static bool ConfigAbnormalBosses => _configAbnormalBosses.Value;
        private static bool ConfigAbnormalBattles => _configAbnormalBattles.Value;
        private static bool ConfigOrdealBosses => _configOrdealBosses.Value;
        private static bool ConfigOrdealBattles => _configOrdealBattles.Value;

        private static bool ConfigFinalOrdeal => _configFinalOrdeal.Value;
        private static bool ConfigFinalBird => _configFinalBird.Value;
        //private static bool ConfigFinalLiar => _configFinalLiar.Value;
        //private static bool ConfigFinalFool => _configFinalFool.Value;
        //private static bool ConfigFinalGod => _configFinalGod.Value;

        private static bool ConfigStartBird => _configStartBird.Value;
        private static bool ConfigStartLiar => _configStartLiar.Value;
        private static bool ConfigStartFool => _configStartFool.Value;
        private static bool ConfigMoreRares => _configMoreRares.Value;

        #endregion

        #endregion

        #region Gameplay.Other

        private static ConfigEntry<int> Config_Blessings;
        public static int NumOfBlessings => Config_Blessings.Value;

        internal static ConfigEntry<bool> Config_HasSeenHim;
        public static bool HasSeenHim => Config_HasSeenHim.Value;

        #endregion

        #endregion

        internal static void BindConfig() {
            Config_ModEnabled = WstlConfigFile.Bind(
                "Config", "Enable", true,
                new ConfigDescription("Enables this mod's content. If false, content will still load but be unavailable through normal means."));

            Config_FoundInGBC = WstlConfigFile.Bind(
                "Config", "GBC Packs", true,
                new ConfigDescription("Allows some cards to be obtainable in Act 2 card packs."));

            Config_ReskinSigils = WstlConfigFile.Bind(
                "Config", "Reskin Sigils", true,
                new ConfigDescription("Changes the names and icons of Sniper and Sentry."));

            Config_SpecialsInRulebook = WstlConfigFile.Bind(
                "Config", "Special Abilities in Rulebook", false,
                new ConfigDescription("Adds Rulebook entries for hidden abilities, describing their effect and what cards possess it."));

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
                new ConfigDescription("Adds more cards to the 'Random' starter deck."));

            Config_EventFlags = WstlConfigFile.Bind(
                "Gameplay", "Unlock All Events", false,
                new ConfigDescription("Unlocks the event starter decks and challenges for Kaycee's Mod, regardless of whether you've met the requirements."));

            Config_CustomBosses = WstlConfigFile.Bind(
                "Gameplay", "Random Bosses", false,
                new ConfigDescription("KCM ONLY - This mod's bosses can be randomly encountered at the end of the first 3 regions."));

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

            Config_StarterDeck = WstlConfigFile.Bind(
                "Gameplay.Part1", "Starter Deck", 0,
                new ConfigDescription("Replaces your starting cards with one of this mod's custom decks." +
                "\n0 - Default Deck" +
                "\n1 - Random Mod Deck (3-12)" +
                "\n2 - Random Mod Cards" +
                "\n3 - One Sin, Fairy Festival, Old Lady" +
                "\n4 - Scorched Girl, Laetitia, Child of the Galaxy" +
                "\n5 - We Can Change Anything, All-Around Helper, Singing Machine" +
                "\n6 - Today's Shy Look, Pinocchio/Mirror of Adjustment, Behaviour Adjustment" +
                "\n7 - Beauty and the Beast, Void Dream, Queen Bee" +
                "\n8 - Fragment of the Universe, Skin Prophecy, Plague Doctor" +
                "\n9 - Bloodbath, Burrowing Heaven, The Snow Queen" +
                "\n10 - Magical Girl, Big Bird, Dream of A Black Swan" +
                "\n11 - The Road Home/Laetitia, Warm-Hearted-Woodsman, Wisdom Scarecrow, Ozma/Snow White's Apple" +
                "\n12 - Magical Girl, King of Greed, Knight of Despair, Servant of Wrath/Void Dream" +
                "\n13 - Punishing Bird, Big Bird, Judgement Bird"));

            string challenges = "Gameplay.Part1.Challenges";

            _configNoTime = WstlConfigFile.Bind(challenges, NoTime.TITLE, false, new ConfigDescription(NoTime.DESCRIPTION));
            _configNoRares = WstlConfigFile.Bind(challenges, NoRares.TITLE, false, new ConfigDescription(NoRares.DESCRIPTION));

            _configOrdealBattles = WstlConfigFile.Bind(challenges, AllOrdeals.TITLE, false, new ConfigDescription(AllOrdeals.DESCRIPTION));
            _configOrdealBosses = WstlConfigFile.Bind(challenges, BossOrdeals.TITLE, false, new ConfigDescription(BossOrdeals.DESCRIPTION));
            _configAbnormalBosses = WstlConfigFile.Bind(challenges, AbnormalBosses.TITLE, false, new ConfigDescription(AbnormalBosses.DESCRIPTION));
            _configAbnormalBattles = WstlConfigFile.Bind(challenges, AbnormalEncounters.TITLE, false, new ConfigDescription(AbnormalEncounters.DESCRIPTION));

            _configSoulbound = WstlConfigFile.Bind(challenges, SoulboundCards.TITLE, false, new ConfigDescription(SoulboundCards.DESCRIPTION));
            _configApostleBears = WstlConfigFile.Bind(challenges, ApostleGrizzlies.TITLE, false, new ConfigDescription(ApostleGrizzlies.DESCRIPTION));
            _configMeltdown = WstlConfigFile.Bind(challenges, QlippothMeltdown.TITLE, false, new ConfigDescription(QlippothMeltdown.DESCRIPTION));
            _configMiracleWorker = WstlConfigFile.Bind(challenges, MiracleWorker.TITLE, false, new ConfigDescription(MiracleWorker.DESCRIPTION));

            _configFinalOrdeal = WstlConfigFile.Bind(challenges, FinalOrdeal.TITLE, false, new ConfigDescription(FinalOrdeal.DESCRIPTION));
            //_configFinalFool = WstlConfigFile.Bind(challenges, FinalJester.TITLE, false, new ConfigDescription(FinalJester.DESCRIPTION));
            //_configFinalLiar = WstlConfigFile.Bind(challenges, FinalLie.TITLE, false, new ConfigDescription(FinalLie.DESCRIPTION));
            _configFinalBird = WstlConfigFile.Bind(challenges, FinalApocalypse.TITLE, false, new ConfigDescription(FinalApocalypse.DESCRIPTION));
            //_configFinalGod = WstlConfigFile.Bind(challenges, FinalComing.TITLE, false, new ConfigDescription(FinalComing.DESCRIPTION));

            _configMoreRares = WstlConfigFile.Bind(challenges, BetterRareChances.TITLE, false, new ConfigDescription(BetterRareChances.DESCRIPTION));
            _configStartBird = WstlConfigFile.Bind(challenges, StartingApocalypse.TITLE, false, new ConfigDescription(StartingApocalypse.DESCRIPTION));
            _configStartFool = WstlConfigFile.Bind(challenges, StartingJester.TITLE, false, new ConfigDescription(StartingJester.DESCRIPTION));
            _configStartLiar = WstlConfigFile.Bind(challenges, StartingLiar.TITLE, false, new ConfigDescription(StartingLiar.DESCRIPTION));

            Config_Blessings = WstlConfigFile.Bind("Gameplay.Other", "Blessings", 0);
            Config_HasSeenHim = WstlConfigFile.Bind("Gameplay.Other", "Blessed", false);
        }
        public static void UpdateBlessings(int value) {
            Config_Blessings.Value += value;
            Log.LogDebug($"The Clock is now at [{NumOfBlessings}]");
        }
        public static void SetBlessings(int value) {
            Config_Blessings.Value = value;
            Log.LogDebug($"The Clock is now at [{NumOfBlessings}]");
        }
        public static void SetHasSeenHim() {
            Config_HasSeenHim.Value = true;
        }

        public static bool ChallengeIsActive(AscensionChallenge challenge) {
            if (SaveFile.IsAscension) {
                return AscensionSaveData.Data.ChallengeIsActive(challenge);
            }

            if (challenge == AbnormalEncounters.ID) {
                return ConfigAbnormalBattles;
            }
            if (challenge == AbnormalBosses.ID) {
                return ConfigAbnormalBosses;
            }
            if (challenge == AllOrdeals.ID) {
                return ConfigOrdealBattles;
            }
            if (challenge == BossOrdeals.ID) {
                return ConfigOrdealBosses;
            }

            if (challenge == NoTime.ID) {
                return ConfigNoTime;
            }
            if (challenge == NoRares.ID) {
                return ConfigNoRares;
            }
            if (challenge == MiracleWorker.ID) {
                return ConfigMiracleWorker;
            }
            if (challenge == QlippothMeltdown.ID) {
                return ConfigMeltdown;
            }
            if (challenge == SoulboundCards.ID) {
                return ConfigSoulbound;
            }
            if (challenge == ApostleGrizzlies.ID) {
                return ConfigApostleBears;
            }

            if (challenge == FinalOrdeal.ID) {
                return ConfigFinalOrdeal;
            }
            if (challenge == FinalApocalypse.ID) {
                return ConfigFinalBird;
            }
            //if (challenge == FinalComing.ID) {
            //    return ConfigFinalGod;
            //}
            //if (challenge == FinalLie.ID) {
            //    return ConfigFinalLiar;
            //}
            //if (challenge == FinalJester.ID) {
            //    return ConfigFinalFool;
            //}

            if (challenge == BetterRareChances.ID) {
                return ConfigMoreRares;
            }
            if (challenge == StartingApocalypse.ID) {
                return ConfigStartBird;
            }
            if (challenge == StartingJester.ID) {
                return ConfigStartFool;
            }
            if (challenge == StartingLiar.ID) {
                return ConfigStartLiar;
            }

            return false;
        }
    }
}