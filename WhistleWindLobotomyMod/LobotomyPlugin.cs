using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using Infiniscryption.Spells;
using Infiniscryption.Achievements;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Guid;
using InscryptionAPI.Regions;
using InscryptionAPI.TalkingCards;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using static DialogueEvent;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;
using static WhistleWindLobotomyMod.Core.LobotomyEncounterManager;

using System.IO;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(InscryptionAPIPlugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(InfiniscryptionSpellsPlugin.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(AbnormalPlugin.pluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.achievements", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class LobotomyPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Log = base.Logger;
            LobotomyConfigManager.Instance.BindConfig();

            if (!LobotomyConfigManager.Instance.ModEnabled)
                Log.LogWarning($"{pluginName} is disabled in the configuration. Things will likely break.");
            else
            {
                DisabledRiskLevels = LobotomyConfigManager.Instance.NoRisk;
                AllCardsDisabled = DisabledRiskLevels.HasFlag(RiskLevel.All) || DisabledRiskLevels.HasFlags(RiskLevel.Zayin, RiskLevel.Teth, RiskLevel.He, RiskLevel.Waw, RiskLevel.Aleph);

                if (LobotomySaveManager.OpponentBlessings > 11)
                    LobotomySaveManager.OpponentBlessings = 11;

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(11);

                CustomBossUtils.InitBossObjects();
                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                Log.LogDebug("Loading dialogue...");
                GenerateDialogueEvents();

                AddChallenges();

                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();

                Log.LogDebug("Loading cards...");
                AddAppearances();
                AddCards();
                Log.LogDebug("Loading starter decks...");
                AddStarterDecks();
                
                Log.LogDebug("Loading encounters...");
                AddEncounters();

                Log.LogDebug("Loading map nodes...");
                //AddItems();
                AddNodes();

                if (PackAPI.Enabled)
                    PackAPI.CreateCardPack();

                if (AchievementAPI.Enabled)
                    AchievementAPI.CreateAchievements();

                Log.LogInfo($"Plugin loaded! Let's get to work manager!");
            }
        }

        private void Start()
        {
            if (LobotomyConfigManager.Instance.ModEnabled)
            {
                if (AllCardsDisabled)
                    Log.LogWarning("Disable Cards is set to [All]. All mod cards have been removed from the pool of obtainable cards.");
                else
                {
                    if (DisabledRiskLevels != RiskLevel.None)
                        Log.LogWarning($"Disable Cards is set to [{DisabledRiskLevels}]. Cards with the affected risk level(s) have been removed from the pool of obtainable cards.");

                    if (LobotomyConfigManager.Instance.NoDonators)
                        Log.LogWarning("Disable Donators is set to [true]. Some cards have been removed from the pool of obtainable cards.");

                    if (LobotomyConfigManager.Instance.NoRuina)
                        Log.LogWarning("Disable Ruina is set to [true]. Some cards have been removed from the pool of obtainable cards.");

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
            TalkingCardManager.New<TalkingCardMalkuth>();
            TalkingCardManager.New<TalkingCardChesed>();
            TalkingCardManager.New<TalkingCardGebura>();
            TalkingCardManager.New<TalkingCardTipherethA>();
            TalkingCardManager.New<TalkingCardTipherethB>();
            TalkingCardManager.New<TalkingCardBinah>();
            TalkingCardManager.New<TalkingCardHokma>();
            TalkingCardManager.New<TalkingCardAngela>();
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
            AddCustomDeathCards();
            CreateTalkingCards();

            if (AllCardsDisabled)
            {
                Log.LogInfo("All mod cards are disabled, adding Standard Training-Dummy Rabbit as a fallback to prevent issues.");
                ObtainableLobotomyCards.Clear();
                ObtainableLobotomyCards.Add(CardLoader.GetCardByName("wstl_trainingDummy"));
            }
        }
        private void AddAbilities()
        {
            AbilityManager.ModifyAbilityList += delegate (List<AbilityManager.FullAbility> abilities)
            {
                var sniperInfo = abilities.Find(x => x.Info.name == "Sniper").Info;
                var sentryInfo = abilities.Find(x => x.Info.name == "Sentry").Info;

                sniperInfo.rulebookName = "Marksman";
                sniperInfo.triggerText = "Your beast strikes with precision.";
                sniperInfo.SetIcon(TextureLoader.LoadTextureFromFile("sigilMarksman"));
                sniperInfo.SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilMarksman_pixel"));

                sentryInfo.rulebookName = "Quick Draw";
                sentryInfo.triggerText = "The early bird gets the worm.";
                sentryInfo.SetIcon(TextureLoader.LoadTextureFromFile("sigilQuickDraw"));
                sentryInfo.SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilQuickDraw_pixel"));
                sentryInfo.SetCanStack();

                return abilities;
            };

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
            //FinalComing.Register();
            FinalApocalypse.Register();
            //FinalJester.Register();
            //FinalTrick.Register();

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

        }
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
                "wstl_beautyAndBeast",
                "wstl_voidDream",
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
                Speaker speaker = Speaker.Single;

                if (dialogue.Key.StartsWith("NothingThere"))
                    speaker = Speaker.Leshy;
                else if (dialogue.Key.StartsWith("WhiteNight"))
                    speaker = Speaker.Bonelord;

                if (!DialogueEventsManager.RepeatDialogueEvents.TryGetValue(dialogue.Key, out List<List<CustomLine>> repeatLines))
                    repeatLines = null;

                DialogueManager.GenerateEvent(pluginGuid, dialogue.Key, dialogue.Value, repeatLines, defaultSpeaker: speaker);
            }
        }

        internal static class PackAPI
        {
            internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            internal static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed.");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind Lobotomy Mod";
                pack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack"));
                pack.Description = $"A set of {ObtainableLobotomyCards.Count} abnormal cards hailing from the world of Lobotomy Corporation.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }
        internal static class AchievementAPI
        {
            internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.achievements");

            // bosses
            internal static Achievement ThroughTheTwilight;
            internal static Achievement WhereAllPathsLead;
            internal static Achievement EndOfTheRoad;
            internal static Achievement ParadiseLost;

            // event decks
            internal static Achievement TheThreeBirds;
            internal static Achievement MagicalGirls;
            internal static Achievement YellowBrickRoad;

            internal static Achievement Test1;
            internal static Achievement Test2;
            internal static void CreateAchievements()
            {
                Log.LogDebug("Achievements API is installed.");
                ModdedAchievementManager.AchievementGroup grp = ModdedAchievementManager.NewGroup(pluginGuid, "Lobotomy Mod", TextureLoader.LoadTextureFromFile("achievementBox.png")).ID;

                Test1 = ModdedAchievementManager.New(pluginGuid, "Test1", "pes",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossEmerald.png")).ID;
                Test2 = ModdedAchievementManager.New(pluginGuid, "Test2", "asd",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossJester.png")).ID;

                ThroughTheTwilight = ModdedAchievementManager.New(pluginGuid, "Through the Twilight", "Survive the apocalypse and defeat the Beast.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossTwilight.png")).ID;

                WhereAllPathsLead = ModdedAchievementManager.New(pluginGuid, "Where All Paths Lead", "Hold on to hope and defeat the Fool.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossJester.png")).ID;

                EndOfTheRoad = ModdedAchievementManager.New(pluginGuid, "End of the Road", "Keep your wits and defeat the Adult.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossEmerald.png")).ID;

                ParadiseLost = ModdedAchievementManager.New(pluginGuid, "Paradise Lost", "Reject His gifts and stall the Saviour.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossSaviour.png")).ID;

                TheThreeBirds = ModdedAchievementManager.New(pluginGuid, "The Three Birds", "You heard the story of the Black Forest.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementTwilight.png")).ID;

                MagicalGirls = ModdedAchievementManager.New(pluginGuid, "Magical Girls", "You walked the paths of all four magical girls.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementMagicalGirls.png")).ID;

                YellowBrickRoad = ModdedAchievementManager.New(pluginGuid, "Yellow Brick Road", "You visited the Emerald City.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementRoadToOz.png")).ID;
            }
            internal static void Unlock(Achievement achievement)
            {
                if (Enabled) AchievementManager.Unlock(achievement);
            }
        }
        public static bool AllCardsDisabled { get; internal set; }
        public static RiskLevel DisabledRiskLevels { get; internal set; }

        public static readonly Trait LittleEgg = GuidManager.GetEnumValue<Trait>(pluginGuid, "SmallBeakEgg");
        public static readonly Trait BigEgg = GuidManager.GetEnumValue<Trait>(pluginGuid, "BigEyesEgg");
        public static readonly Trait LongEgg = GuidManager.GetEnumValue<Trait>(pluginGuid, "LongArmsEgg");

        public static readonly StoryEvent ApocalypseBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "ApocalpyseBossDefeated");
        public static readonly StoryEvent JesterBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "JesterBossDefeated");
        public static readonly StoryEvent EmeraldBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "EmeraldBossDefeated");
        public static readonly StoryEvent SaviourBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "SaviourBossDefeated");

        private static readonly Harmony HarmonyInstance = new(pluginGuid);
        internal static ManualLogSource Log;

        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "WhistleWind Lobotomy Mod";
        private const string pluginVersion = "2.1.0";
    }
}
