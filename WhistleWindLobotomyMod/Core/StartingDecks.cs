using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod {
    public class StarterDecks {
        public const int NUM_DECKS = 13;
        public static readonly List<string> firstDay = new() {
            Cards.oneSin,
            Cards.fairyFestival,
            Cards.oldLady
        };
        public static readonly List<string> lonelyFriends = new() {
            Cards.scorchedGirl,
            Cards.laetitia,
            Cards.childOfTheGalaxy
        };
        public static readonly List<string> bloodMechs = new() {
            Cards.weCanChangeAnything,
            Cards.singingMachine,
            Cards.allAroundHelper
        };
        public static readonly List<string> peoplePleasers = new() {
            Cards.todaysShyLook,
            LobotomyConfigManager.NoRuina? Cards.mirrorOfAdjustment : Cards.pinocchio,
            Cards.behaviourAdjustment
        };
        public static readonly List<string> freakShow = new() {
            Cards.voidDream,
            Cards.beautyAndBeast,
            Cards.queenBee
        };
        public static readonly List<string> apocrypha = new() {
            Cards.fragmentOfUniverse,
            Cards.skinProphecy,
            LobotomyConfigManager.NoRuina ? Cards.mhz176 : Cards.priceOfSilence
        };
        public static readonly List<string> keter = new() {
            Cards.burrowingHeaven,
            Cards.heartOfAspiration,
            Cards.snowQueen
        };
        public static readonly List<string> deathLovers = new() {
            Cards.bloodBath,
            Cards.bigBird,
            Cards.dreamOfABlackSwan
        };
        public static readonly List<string> roadToOz = new() {
            LobotomyConfigManager.NoRuina ? Cards.laetitia : Cards.theRoadHome,
            Cards.warmHeartedWoodsman,
            Cards.wisdomScarecrow,
            LobotomyConfigManager.NoRuina ? Cards.snowWhitesApple : Cards.ozma
        };
        public static readonly List<string> magicGirls = new() {
            Cards.magicalGirlSpade,
            Cards.magicalGirlHeart,
            Cards.magicalGirlDiamond,
            LobotomyConfigManager.NoRuina ? Cards.wallLady : Cards.magicalGirlClover
        };
        public static readonly List<string> twilight = new() {
            Cards.punishingBird,
            Cards.bigBird,
            Cards.judgementBird
        };
        internal static void AddStarterDecks() {
            List<string> randomCards = new() { Cards.randomPlaceholder, Cards.randomPlaceholder, Cards.randomPlaceholder };
            if (LobotomyConfigManager.StarterDeckSize > 0) {
                for (int i = 0; i < LobotomyConfigManager.StarterDeckSize; i++)
                    randomCards.Add(Cards.randomPlaceholder);
            }

            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Random Mod Cards", "starterDeckRandom.png", 12, cardNames: randomCards);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "First Day", "starterDeckControl.png", 0, cardNames: firstDay);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Lonely Friends", "starterDeckChildren.png", 2, cardNames: lonelyFriends);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Blood Machines", "starterDeckBloodMachines.png", 4, cardNames: bloodMechs);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "People Pleasers", "starterDeckPeoplePleasers.png", 5, cardNames: peoplePleasers);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Freak Show", "starterDeckFreakShow.png", 6, cardNames: freakShow);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Apocrypha", "starterDeckApocrypha.png", 7, cardNames: apocrypha);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Keter", "starterDeckKeter.png", 8, cardNames: keter);
            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Death Lovers", "starterDeckDeathLovers.png", 8, cardNames: deathLovers);

            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Road to Oz", "starterDeckFairyTale.png", 0, cardNames: roadToOz,
                customUnlock: dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.EventFlags);

            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Magical Girls!", "starterDeckMagicalGirls.png", 0, cardNames: magicGirls,
                customUnlock: dummy => LobotomySaveManager.UnlockedJesterOfNihil || LobotomyConfigManager.EventFlags);

            StarterDeckHelper.AddStarterDeck(LobotomyPlugin.pluginPrefix, "Twilight", "starterDeckBlackForest.png", 0, cardNames: twilight,
                customUnlock: dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomySaveManager.DefeatedApocalypseBoss || LobotomyConfigManager.EventFlags);
        }
    }
}