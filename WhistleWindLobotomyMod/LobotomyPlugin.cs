using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells;
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
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using static DialogueEvent;
using static InscryptionAPI.Dialogue.DialogueManager;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;
using static WhistleWindLobotomyMod.Core.LobotomyEncounterManager;

namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(InscryptionAPIPlugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(InfiniscryptionSpellsPlugin.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(AbnormalPlugin.pluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.achievements", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("arackulele.inscryption.grimoramod", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.p03kayceerun", BepInDependency.DependencyFlags.SoftDependency)]
    public partial class LobotomyPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Log = base.Logger;
            ModAssembly = Assembly.GetExecutingAssembly();
            LobotomyConfigManager.Instance.BindConfig();
            if (!LobotomyConfigManager.Instance.ModEnabled)
                Log.LogWarning($"{pluginName} is disabled in the configuration. Things will likely break.");
            else
            {
                if (LobotomyConfigManager.Instance.NoRisk == RiskLevel.All)
                {
                    DisabledRiskLevels = RiskLevel.Zayin & RiskLevel.Teth & RiskLevel.He & RiskLevel.Waw & RiskLevel.Aleph;
                }
                else
                {
                    DisabledRiskLevels = LobotomyConfigManager.Instance.NoRisk;
                }
                AllCardsDisabled = DisabledRiskLevels.HasFlag(RiskLevel.All) || DisabledRiskLevels.HasFlags(RiskLevel.Zayin, RiskLevel.Teth, RiskLevel.He, RiskLevel.Waw, RiskLevel.Aleph);

                if (LobotomySaveManager.OpponentBlessings > 11)
                    LobotomySaveManager.OpponentBlessings = 11;

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(11);

                CustomOpponentUtils.InitBossObjects();
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
                AddStarterDecks();

                Log.LogDebug("Loading encounters...");
                AddEncounters();
                OrdealUtils.InitOrdeals();

                Log.LogDebug("Loading everything else...");
                AddItems();
                AddNodes();
                OrdealPages.AddPages();

                if (PackAPI.Enabled)
                    PackAPI.CreateCardPack();

                if (AchievementAPI.Enabled)
                    AchievementAPI.CreateAchievements();

                Log.LogInfo($"Plugin loaded! Let's get to work manager!");
            }
        }

        private void Start()
        {
            if (!LobotomyConfigManager.Instance.ModEnabled)
                return;

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

                Log.LogInfo($"There are [{AllLobotomyCards.Count}] total cards and [{BaseModCards.Count} | {WonderLabCards.Count} | {LimbusCards.Count}] obtainable cards.");
            }
            Log.LogInfo($"The Clock is at [{LobotomyConfigManager.Instance.NumOfBlessings}].");
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
            foreach (CardInfo card in CardManager.AllCardsCopy.Where(c => c.GetModTag() == "whistlewind.inscryption.abnormalsigils"))
            {
                if (!AllLobotomyCards.Contains(card))
                    AllLobotomyCards.Add(card);
            }
            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));
            AddCustomDeathCards();
            CreateTalkingCards();

            if (AllCardsDisabled)
            {
                Log.LogInfo("All mod cards are disabled, adding [Standard Training-Dummy Rabbit] as a fallback card.");
                ObtainableLobotomyCards = new() { AllLobotomyCards.Find(x => x.name == "wstl_trainingDummy") };
            }
            else
            {
                ObtainableLobotomyCards = AllLobotomyCards.Where(x => x.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.Rare)).ToList();
            }
        }
        private void AddAbilities()
        {
            if (LobotomyConfigManager.Instance.ReskinSigils)
            {
                AbilityManager.ModifyAbilityList += delegate (List<AbilityManager.FullAbility> abilities)
                {
                    abilities.AbilityByID(Ability.Sniper).Info
                        .SetRulebookName("Marksman")
                        .SetAbilityLearnedDialogue("Your beast strikes with precision.")
                        .SetIcon(TextureLoader.LoadTextureFromFile("sigilMarksman.png"))
                        .SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilMarksman_pixel.png"))
                        .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    abilities.AbilityByID(Ability.Sentry).Info
                        .SetRulebookName("Quick Draw")
                        .SetAbilityLearnedDialogue("The early bird gets the worm.")
                        .SetIcon(TextureLoader.LoadTextureFromFile("sigilQuickDraw.png"))
                        .SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilQuickDraw_pixel.png"))
                        .SetCanStack()
                        .SetFlipYIfOpponent()
                        .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    abilities.AbilityByID(Ability.Transformer).Info
                        .SetRulebookDescription("[creature] will transform into a different form after 1 turn on the board.")
                        .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    abilities.AbilityByID(Ability.ExplodeOnDeath).Info
                        .SetRulebookName("Volatile")
                        .SetCustomFlippedTexture(TextureLoader.LoadTextureFromFile("sigilVolatile_flipped.png", ModAssembly))
                        .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    return abilities;
                };
            }

            Ability_BoneMeal();
            Ability_TimeMachine();
            Ability_Apostle();
            Ability_TrueSaviour();
            Ability_Confession();

            Ability_Life();
            Ability_Harmony();
            Ability_Food();
            Ability_Survival();

            Ability_Apocalypse();
            Ability_BigEyes();

            StatusEffect_Enchanted();
            Ability_Dazzling();
            
            Ability_SmallBeak();
            Ability_Misdeeds();
            Ability_LongArms();

            StatusEffect_Sin();
            Ability_UnjustScale();
            
            Ability_GiantBlocker();

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
            AllOrdeals.Register(HarmonyInstance);
            AbnormalBosses.Register(HarmonyInstance);
            AbnormalEncounters.Register(HarmonyInstance);
        }
        private void AddItems()
        {
            // Enkephalin box - max out and recharge energy
            // Pebble - Gives Pebble effect to a card
            // Accelerator - Gives card +X Haste
            // Decelerator - Gives card +X Bind
            Item_RecallBottle();
        }

        private void AddEncounters()
        {
            BuildEncounters();
            RegionProgression.Instance.regions[0].AddEncounters(ModEncounters[0].ToArray());
            RegionProgression.Instance.regions[1].AddEncounters(ModEncounters[1].ToArray());
            RegionProgression.Instance.regions[2].AddEncounters(ModEncounters[2].ToArray());
        }

        private void GenerateDialogueEvents()
        {
            DialogueEventsManager.DialogueEvents ??= new();
            DialogueEventsManager.RepeatDialogueEvents ??= new();

            AccessTools.GetDeclaredMethods(typeof(LobotomyDialogue)).Where(mi => mi.Name.StartsWith("Dialogue")).ForEach(mi => mi.Invoke(new LobotomyDialogue(), null));

            foreach (KeyValuePair<string, List<CustomLine>> dialogue in DialogueEventsManager.DialogueEvents)
            {
                Speaker speaker = Speaker.Single;

                if (dialogue.Key.StartsWith("NothingThere"))
                    speaker = Speaker.Leshy;
                else if (dialogue.Key.StartsWith("WhiteNight"))
                    speaker = Speaker.Bonelord;

                if (!DialogueEventsManager.RepeatDialogueEvents.TryGetValue(dialogue.Key, out List<List<CustomLine>> repeatLines))
                    repeatLines = null;

                GenerateEvent(pluginGuid, dialogue.Key, dialogue.Value, repeatLines, defaultSpeaker: speaker);
            }
        }
        public static bool AllCardsDisabled { get; internal set; }
        public static RiskLevel DisabledRiskLevels { get; internal set; }

        public static readonly StoryEvent ApocalypseBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "ApocalpyseBossDefeated");
        public static readonly StoryEvent JesterBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "JesterBossDefeated");
        public static readonly StoryEvent EmeraldBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "EmeraldBossDefeated");
        public static readonly StoryEvent SaviourBossDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "SaviourBossDefeated");

        public static readonly StoryEvent OrdealDefeated = GuidManager.GetEnumValue<StoryEvent>(pluginGuid, "OrdealDefeated");

        internal static readonly Harmony HarmonyInstance = new(pluginGuid);
        internal static Assembly ModAssembly { get; private set; }
        internal static ManualLogSource Log;

        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginPrefix = "wstl";
        public const string wonderlabPrefix = "wstl_WL";
        public const string limbusPrefix = "wstl_LiC";

        public const string pluginName = "WhistleWind Lobotomy Mod";
        private const string pluginVersion = "3.0.0";
    }
}
