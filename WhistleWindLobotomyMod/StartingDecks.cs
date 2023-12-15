using BepInEx.Bootstrap;
using DiskCardGame;
using Infiniscryption.Achievements;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Patches;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void AddStarterDecks()
        {
            List<string> randomCards = new() { "wstl_RANDOM_PLACEHOLDER", "wstl_RANDOM_PLACEHOLDER", "wstl_RANDOM_PLACEHOLDER" };
            if (LobotomyConfigManager.Instance.StarterDeckSize > 0)
            {
                for (int i = 0; i < LobotomyConfigManager.Instance.StarterDeckSize; i++)
                    randomCards.Add("wstl_RANDOM_PLACEHOLDER");
            }

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Random Mod Cards", "starterDeckRandom", 0, cardNames: randomCards);

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "First Day", "starterDeckControl", 0, cardNames: new() {
                "wstl_oneSin",
                "wstl_fairyFestival",
                "wstl_oldLady" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Lonely Friends", "starterDeckChildren", 2, cardNames: new() {
                "wstl_scorchedGirl",
                "wstl_laetitia",
                "wstl_childOfTheGalaxy" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Blood Machines", "starterDeckBloodMachines", 4, cardNames: new() {
                "wstl_weCanChangeAnything",
                "wstl_singingMachine",
                "wstl_allAroundHelper" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "People Pleasers", "starterDeckPeoplePleasers", 5, cardNames: new() {
                "wstl_todaysShyLook",
                LobotomyConfigManager.Instance.NoRuina ? "wstl_mirrorOfAdjustment" : "wstl_pinocchio",
                "wstl_behaviourAdjustment" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Freak Show", "starterDeckFreakShow", 6, cardNames: new() {
                "wstl_voidDream",
                "wstl_beautyAndBeast",
                "wstl_queenBee" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Apocrypha", "starterDeckApocrypha", 7, cardNames: new() {
                "wstl_fragmentOfUniverse",
                "wstl_skinProphecy",
                LobotomyConfigManager.Instance.NoRuina ? "wstl_mhz176" : "wstl_priceOfSilence" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Keter", "starterDeckKeter", 8, cardNames: new() {
                "wstl_bloodBath",
                "wstl_burrowingHeaven",
                "wstl_snowQueen" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Road to Oz", "starterDeckFairyTale", 0, cardNames: new() {
                LobotomyConfigManager.Instance.NoRuina ? "wstl_laetitia" : "wstl_theRoadHome",
                "wstl_warmHeartedWoodsman",
                "wstl_wisdomScarecrow",
                LobotomyConfigManager.Instance.NoRuina ? "wstl_snowWhitesApple" : "wstl_ozma" },
                customUnlock: dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.Instance.EventFlags);

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Magical Girls!", "starterDeckMagicalGirls", 0, cardNames: new() {
                "wstl_magicalGirlSpade",
                "wstl_magicalGirlHeart",
                "wstl_magicalGirlDiamond",
                LobotomyConfigManager.Instance.NoRuina ? "wstl_voidDream" : "wstl_magicalGirlClover" },
                customUnlock: dummy => LobotomySaveManager.UnlockedJesterOfNihil || LobotomyConfigManager.Instance.EventFlags);

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Twilight", "starterDeckBlackForest", 0, cardNames: new() {
                "wstl_punishingBird",
                "wstl_bigBird",
                "wstl_judgementBird" },
                customUnlock: dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomySaveManager.DefeatedApocalypseBoss || LobotomyConfigManager.Instance.EventFlags);
        }
    }
}