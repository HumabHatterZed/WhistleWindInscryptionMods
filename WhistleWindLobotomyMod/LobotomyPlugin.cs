using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Regions;
using InscryptionAPI.TalkingCards;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Challenges;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;
using static WhistleWindLobotomyMod.Core.LobotomyEncounterManager;

namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.spells", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("whistlewind.inscryption.abnormalsigils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class LobotomyPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "WhistleWind Lobotomy Mod";
        private const string pluginVersion = "2.0.0";

        internal static ManualLogSource Log;
        private static Harmony HarmonyInstance = new(pluginGuid);

        private static bool _allCardsDisabled;
        public static bool AllCardsDisabled => _allCardsDisabled;

        private static bool _ruinaCardsDisabled;
        public static bool RuinaCardsDisabled => _ruinaCardsDisabled;

        public static bool _donatorCardsDisabled;
        public static bool DonatorCardsDisabled => _donatorCardsDisabled;

        private static RiskLevel _disabledRiskLevels;
        public static RiskLevel DisabledRiskLevels => _disabledRiskLevels;

        private void Awake()
        {
            Log = base.Logger;
            LobotomyConfigManager.Instance.BindConfig();

            if (!LobotomyConfigManager.Instance.ModEnabled)
                Log.LogWarning($"{pluginName} is disabled in the configuration. Things will likely break.");
            else
            {
                _allCardsDisabled = LobotomyConfigManager.Instance.NoRisk.HasFlag(RiskLevel.All) || LobotomyConfigManager.Instance.NoRisk.HasFlags(RiskLevel.Zayin, RiskLevel.Teth, RiskLevel.He, RiskLevel.Waw, RiskLevel.Aleph);
                _disabledRiskLevels = LobotomyConfigManager.Instance.NoRisk;
                _donatorCardsDisabled = LobotomyConfigManager.Instance.NoDonators;
                _ruinaCardsDisabled = LobotomyConfigManager.Instance.NoRuina;

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(11);

                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                GenerateDialogueEvents();

                AddChallenges();

                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();

                Log.LogDebug("Loading cards...");
                AddCards();
                AddAppearances();
                AddStarterDecks();

                Log.LogDebug("Loading encounters...");
                AddEncounters();

                Log.LogDebug("Loading items and nodes...");
                AddItems();
                AddNodes();

                if (PackAPI.Enabled)
                    PackAPI.CreateCardPack();

                Log.LogInfo($"Plugin loaded! Let's get to work manager!");
            }
        }

        private void Start()
        {
            if (LobotomyConfigManager.Instance.ModEnabled)
            {
                if (AllCardsDisabled)
                    Log.LogWarning("Disable Cards is set to [All]. All mod cards have been removed from the pool of obtainable cards");
                else
                {
                    if (DisabledRiskLevels != RiskLevel.None)
                        Log.LogWarning($"Disable Cards is set to [{DisabledRiskLevels}]. Cards with the affected risk level(s) have been removed from the pool of obtainable cards.");

                    if (DonatorCardsDisabled)
                        Log.LogWarning("Disable Donators is set to true. Some cards have been removed from the pool of obtainable cards.");

                    if (RuinaCardsDisabled)
                        Log.LogWarning("Disable Ruina is set to true. Some cards have been removed from the pool of obtainable cards.");

                    Log.LogInfo($"There are [{AllLobotomyCards.Count}] total cards and [{ObtainableLobotomyCards.Count}] obtainable cards.");
                }
                Log.LogInfo($"The Clock is at [{LobotomyConfigManager.Instance.NumOfBlessings}].");
            }
        }
        private void OnDisable() => HarmonyInstance.UnpatchSelf();
        private void AddAppearances() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
        private void AddSpecialAbilities() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));
        private void AddNodes() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Node")).ForEach(mi => mi.Invoke(this, null));
        private void CreateTalkingCards()
        {
            TalkingCardManager.New<TalkingCardHod>();
            TalkingCardManager.New<TalkingCardYesod>();
            TalkingCardManager.New<TalkingCardNetzach>();
            // SephirahMalkuth.Init();
            // SephirahChesed.Init();
            // SephirahGebura.Init();
            // SephirahTipherethB.Init();
            // SephirahTipherethA.Init();
            // SephirahBinah.Init();
            // SephirahHokma.Init();
            // Angela.Init();
        }
        private void AddCards()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards.Where(c => c.GetModTag() == "whistlewind.inscryption.abnormalsigils"))
                {
                    if (!AllLobotomyCards.Contains(card))
                        AllLobotomyCards.Add(card);
                }
                return cards;
            };

            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));
            CreateTalkingCards();
            if (AllCardsDisabled)
            {
                Log.LogInfo("All mod cards are disabled, adding Standard Training-Dummy Rabbit as a fallback to prevent issues.");
                ObtainableLobotomyCards = new() { CardLoader.GetCardByName("wstl_trainingDummy") };
            }
        }
        private void AddAbilities()
        {
            Ability_TimeMachine();
            Ability_Apostle();
            Ability_TrueSaviour();
            Ability_Confession();

            if (LobotomyConfigManager.Instance.RevealSpecials)
            {
                Log.LogDebug("Adding rulebook entries for special abilities.");
                AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Rulebook")).ForEach(mi => mi.Invoke(this, null));
            }
        }
        private void AddChallenges()
        {
            // MiracleBoss
            // BirdBoss
            // FoolBoss
            // AdultBoss

            ApocalypseBirdStart.Register();
            JesterOfNihilStart.Register();
            LyingAdultStart.Register();
            BetterRareChances.Register();

            MiracleWorker.Register(HarmonyInstance);
            AbnormalBosses.Register(HarmonyInstance);
            AbnormalEncounters.Register(HarmonyInstance);
        }
        private void AddItems()
        {
            AddBottleCards();
        }
        private void AddStarterDecks()
        {
            List<string> randomCards = new() { "wstl_RANDOM_PLACEHOLDER", "wstl_RANDOM_PLACEHOLDER", "wstl_RANDOM_PLACEHOLDER" };
            if (LobotomyConfigManager.Instance.StarterDeckSize > 0)
            {
                for (int i = 0; i < LobotomyConfigManager.Instance.StarterDeckSize; i++)
                    randomCards.Add("wstl_RANDOM_PLACEHOLDER");
            }

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Random Mod Cards", Artwork.starterDeckRandom, 0, cardNames: randomCards);

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "First Day", Artwork.starterDeckControl, 0, cardNames: new() {
                "wstl_oneSin",
                "wstl_fairyFestival",
                "wstl_oldLady" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Lonely Friends", Artwork.starterDeckChildren, 2, cardNames: new() {
                "wstl_scorchedGirl",
                "wstl_laetitia",
                "wstl_childOfTheGalaxy" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Blood Machines", Artwork.starterDeckBloodMachines, 4, cardNames: new() {
                "wstl_weCanChangeAnything",
                "wstl_singingMachine",
                "wstl_allAroundHelper" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "People Pleasers", Artwork.starterDeckPeoplePleasers, 5, cardNames: new() {
                "wstl_todaysShyLook",
                RuinaCardsDisabled ? "wstl_mirrorOfAdjustment" : "wstl_pinocchio",
                "wstl_behaviourAdjustment" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Freak Show", Artwork.starterDeckFreakShow, 6, cardNames: new() {
                "wstl_beautyAndBeast",
                "wstl_voidDream",
                "wstl_queenBee" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Apocrypha", Artwork.starterDeckApocrypha, 7, cardNames: new() {
                "wstl_fragmentOfUniverse",
                "wstl_skinProphecy",
                RuinaCardsDisabled ? "wstl_mhz176" : "wstl_priceOfSilence" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Keter", Artwork.starterDeckKeter, 8, cardNames: new() {
                "wstl_bloodBath",
                "wstl_burrowingHeaven",
                "wstl_snowQueen" });

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Road to Oz", Artwork.starterDeckFairyTale, 0, cardNames: new() {
                RuinaCardsDisabled ? "wstl_laetitia" : "wstl_theRoadHome",
                "wstl_warmHeartedWoodsman",
                "wstl_wisdomScarecrow",
                RuinaCardsDisabled ? "wstl_snowWhitesApple" : "wstl_ozma" },
                customUnlock: dummy => LobotomySaveManager.UnlockedLyingAdult || LobotomyConfigManager.Instance.EventFlags);

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Magical Girls!", Artwork.starterDeckMagicalGirls, 0, cardNames: new() {
                "wstl_magicalGirlSpade",
                "wstl_magicalGirlHeart",
                "wstl_magicalGirlDiamond",
                RuinaCardsDisabled? "wstl_voidDream" : "wstl_magicalGirlClover" },
                customUnlock: dummy => LobotomySaveManager.UnlockedJesterOfNihil || LobotomyConfigManager.Instance.EventFlags);

            StarterDeckHelper.AddStarterDeck(pluginPrefix, "Twilight", Artwork.starterDeckBlackForest, 0, cardNames: new() {
                "wstl_punishingBird",
                "wstl_bigBird",
                "wstl_judgementBird" },
                customUnlock: dummy => LobotomySaveManager.UnlockedApocalypseBird || LobotomyConfigManager.Instance.EventFlags);
        }
        private void AddEncounters()
        {
            BuildEncounters();
            for (int i = 0; i < 3; i++)
                RegionProgression.Instance.regions[i].AddEncounters(ModEncounters[i].ToArray());
        }

        private void GenerateDialogueEvents()
        {
            DialogueEventsManager.DialogueEvents ??= new();
            DialogueEventsManager.RepeatDialogueEvents ??= new();

            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Dialogue")).ForEach(mi => mi.Invoke(this, null));

            foreach (KeyValuePair<string, List<CustomLine>> dialogue in DialogueEventsManager.DialogueEvents)
            {
                DialogueEvent.Speaker speaker = DialogueEvent.Speaker.Single;

                if (dialogue.Key.StartsWith("NothingThere"))
                    speaker = DialogueEvent.Speaker.Leshy;
                else if (dialogue.Key.StartsWith("WhiteNight"))
                    speaker = DialogueEvent.Speaker.Bonelord;

                if (!DialogueEventsManager.RepeatDialogueEvents.TryGetValue(dialogue.Key, out List<List<CustomLine>> repeatLines))
                    repeatLines = null;

                DialogueManager.GenerateEvent(pluginGuid, dialogue.Key, dialogue.Value, repeatLines, defaultSpeaker: speaker);
            }
        }

        public static class PackAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            internal static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed, creating card pack...");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind Lobotomy Mod";
                pack.SetTexture(TextureLoader.LoadTextureFromBytes(Artwork.wstl_pack));
                pack.Description = $"A set of {ObtainableLobotomyCards} abnormal cards hailing from the world of Lobotomy Corporation.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }
    }
}
