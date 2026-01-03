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
using System.IO;
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

namespace WhistleWindLobotomyMod {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(InscryptionAPIPlugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(InfiniscryptionSpellsPlugin.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(AbnormalPlugin.pluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.achievements", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("arackulele.inscryption.grimoramod", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.p03kayceerun", BepInDependency.DependencyFlags.SoftDependency)]
    public class LobotomyPlugin : BaseUnityPlugin {
        private void Awake() {
            Log = base.Logger;
            ModAssembly = Assembly.GetExecutingAssembly();
            LobotomyConfigManager.BindConfig();
            if (!LobotomyConfigManager.ModEnabled) {
                Log.LogWarning($"{pluginName} is disabled in the configuration. Things will likely break.");
                return;
            }

            if (LobotomyConfigManager.NoRisk == RiskLevel.All) {
                DisabledRiskLevels = RiskLevel.Zayin & RiskLevel.Teth & RiskLevel.He & RiskLevel.Waw & RiskLevel.Aleph;
            }
            else {
                DisabledRiskLevels = LobotomyConfigManager.NoRisk;
            }
            AllCardsDisabled = DisabledRiskLevels.HasFlag(RiskLevel.All) || DisabledRiskLevels.HasFlags(RiskLevel.Zayin, RiskLevel.Teth, RiskLevel.He, RiskLevel.Waw, RiskLevel.Aleph);

            if (LobotomySaveManager.OpponentBlessings > 11)
                LobotomySaveManager.OpponentBlessings = 11;

            if (LobotomyConfigManager.NumOfBlessings > 11)
                LobotomyConfigManager.SetBlessings(11);

            Log.LogDebug("Loading assets...");
            AssetManager.Initialise();

            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Log.LogDebug("Loading dialogue...");
            GenerateDialogueEvents();

            AddChallenges();
            OrdealPages.AddPages();

            Log.LogDebug("Loading abilities...");
            Abilities.AddAbilities(this);

            Log.LogDebug("Loading cards...");
            AccessTools.GetDeclaredMethods(typeof(Appearances)).ForEach(mi => mi.Invoke(this, null));
            AddCards();
            StarterDecks.AddStarterDecks();

            Log.LogDebug("Loading encounters...");
            AddEncounters();

            Log.LogDebug("Loading everything else...");
            // Enkephalin box - max out and recharge energy
            // Pebble - Gives Pebble effect to a card
            // Accelerator - Gives card +X Haste
            // Decelerator - Gives card +X Bind

            AccessTools.GetDeclaredMethods(typeof(Items)).ForEach(mi => mi.Invoke(this, null));
            AccessTools.GetDeclaredMethods(typeof(Nodes)).ForEach(mi => mi.Invoke(this, null));

            if (PackAPI.Enabled)
                PackAPI.CreateCardPack();

            AchievementAPI.CreateAchievements();

            AssetManager.UnloadAssetBundle();
            Log.LogInfo($"Plugin loaded! Let's get to work manager!");
        }

        private void Start() {
            if (!LobotomyConfigManager.ModEnabled)
                return;

            if (AllCardsDisabled)
                Log.LogWarning("Disable Cards is set to [All]. All mod cards have been removed from the pool of obtainable cards.");
            else {
                if (DisabledRiskLevels != RiskLevel.None)
                    Log.LogWarning($"Disable Cards is set to [{DisabledRiskLevels}]. Cards with the affected risk level(s) have been removed from the pool of obtainable cards.");

                if (LobotomyConfigManager.NoDonators)
                    Log.LogWarning("Disable Donators is set to [true]. Some cards have been removed from the pool of obtainable cards.");

                if (LobotomyConfigManager.NoRuina)
                    Log.LogWarning("Disable Ruina is set to [true]. Some cards have been removed from the pool of obtainable cards.");

                Log.LogInfo($"There are [{AllLobotomyCards.Count}:{BaseModCards.Count} + WL:{WonderLabCards.Count} + LC:{LimbusCards.Count}] total cards and [{ObtainableLobotomyCards.Count}] obtainable cards.");
            }
            Log.LogInfo($"The Clock is at [{LobotomyConfigManager.NumOfBlessings}].");
        }
        private void OnDisable() => HarmonyInstance.UnpatchSelf();

        private void AddCards() {
            foreach (CardInfo card in CardManager.AllCardsCopy.Where(c => c.GetModTag() == "whistlewind.inscryption.abnormalsigils")) {
                if (!AllLobotomyCards.Contains(card))
                    AllLobotomyCards.Add(card);
            }
            AccessTools.GetDeclaredMethods(typeof(Cards)).ForEach(mi => mi.Invoke(this, null));
            Cards.AddCustomDeathCards();
            CreateTalkingCards();

            if (AllCardsDisabled) {
                Log.LogInfo("All mod cards are disabled, adding [Standard Training-Dummy Rabbit] as a fallback card.");
            }

            QlippothCards.InitialiseQlippothCardInfos();
        }
        private void CreateTalkingCards() {
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

        /// <summary>
        /// 
        /// </summary>
        private void AddChallenges() {

            //FinalComing.Register();
            FinalApocalypse.Register();
            //Arbiter
            //Red Mist
            FinalOrdeal.Register();
            //FinalJester.Register();
            //FinalLie.Register();

            StartingApocalypse.Register();
            StartingJester.Register();
            StartingLiar.Register();
            BetterRareChances.Register();

            ApostleGrizzlies.Register();
            SoulboundCards.Register();
            BossOrdeals.Register();
            AbnormalBosses.Register(HarmonyInstance);
            AllOrdeals.Register(HarmonyInstance);
            AbnormalEncounters.Register(HarmonyInstance);

            QlippothMeltdown.Register();
            MiracleWorker.Register(HarmonyInstance);
            NoRares.Register();
            NoTime.Register(HarmonyInstance);

            BetterRareChances.Info.SetIncompatibleChallengeGetterStatic(NoRares.Id);
            AllOrdeals.Info.SetIncompatibleChallengeGetterStatic(AbnormalEncounters.Id);
            BossOrdeals.Info.SetIncompatibleChallengeGetterStatic(AbnormalBosses.Id);
        }

        private void AddEncounters() {
            BuildEncounters();
            RegionProgression.Instance.regions[0].AddEncounters(ModEncounters[0].ToArray());
            RegionProgression.Instance.regions[1].AddEncounters(ModEncounters[1].ToArray());
            RegionProgression.Instance.regions[2].AddEncounters(ModEncounters[2].ToArray());
        }

        private void GenerateDialogueEvents() {
            DialogueEventsManager.DialogueEvents ??= new();
            DialogueEventsManager.RepeatDialogueEvents ??= new();

            AccessTools.GetDeclaredMethods(typeof(LobotomyDialogue)).Where(mi => mi.Name.StartsWith("Dialogue")).ForEach(mi => mi.Invoke(new LobotomyDialogue(), null));

            foreach (KeyValuePair<string, List<CustomLine>> dialogue in DialogueEventsManager.DialogueEvents) {
                Speaker speaker = Speaker.Single;

                if (dialogue.Key.StartsWith("NothingThere"))
                    speaker = Speaker.Leshy;
                else if (dialogue.Key.StartsWith("WhiteNight"))
                    speaker = Speaker.Bonelord;

                if (!DialogueEventsManager.RepeatDialogueEvents.TryGetValue(dialogue.Key, out List<List<CustomLine>> repeatLines))
                    repeatLines = null;

                GenerateEvent(LobotomyPlugin.pluginGuid, dialogue.Key, dialogue.Value, repeatLines, defaultSpeaker: speaker);
            }
        }
        public static bool AllCardsDisabled { get; internal set; }
        public static RiskLevel DisabledRiskLevels { get; internal set; }

        public static readonly StoryEvent ApocalypseBossDefeated = GuidManager.GetEnumValue<StoryEvent>(LobotomyPlugin.pluginGuid, "ApocalpyseBossDefeated");
        public static readonly StoryEvent JesterBossDefeated = GuidManager.GetEnumValue<StoryEvent>(LobotomyPlugin.pluginGuid, "JesterBossDefeated");
        public static readonly StoryEvent EmeraldBossDefeated = GuidManager.GetEnumValue<StoryEvent>(LobotomyPlugin.pluginGuid, "EmeraldBossDefeated");
        public static readonly StoryEvent SaviourBossDefeated = GuidManager.GetEnumValue<StoryEvent>(LobotomyPlugin.pluginGuid, "SaviourBossDefeated");

        public static readonly StoryEvent OrdealDefeated = GuidManager.GetEnumValue<StoryEvent>(LobotomyPlugin.pluginGuid, "OrdealDefeated");

        internal static readonly Harmony HarmonyInstance = new(LobotomyPlugin.pluginGuid);
        internal static Assembly ModAssembly { get; private set; }
        internal static ManualLogSource Log;

        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginPrefix = "wstl";
        public const string pixelPrefix = "wstlGBC";
        public const string wonderlabPrefix = "wstlWonder";
        public const string limbusPrefix = "wstlLimbus";

        public const string pluginName = "WhistleWind Lobotomy Mod";
        private const string pluginVersion = "3.0.0";
    }
}
