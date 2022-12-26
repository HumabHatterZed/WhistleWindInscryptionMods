using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers;
using InscryptionAPI.Regions;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core;
using WhistleWind.LobotomyMod.Core.Challenges;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;
using static WhistleWind.LobotomyMod.Core.Helpers.LobotomyCardHelper;
using static WhistleWind.LobotomyMod.Core.Opponents.AbnormalEncounterData;

namespace WhistleWind.LobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.spells", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("whistlewind.inscryption.abnormalsigils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class LobotomyPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.lobotomymod";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "WhistleWind Lobotomy Mod";
        private const string pluginVersion = "2.0.0";

        internal static ManualLogSource Log;
        private static Harmony HarmonyInstance = new(pluginGuid);

        public static AssetBundle sephirahBundle;

        public static List<CardInfo> AllLobotomyCards = new();
        public static List<CardInfo> ObtainableLobotomyCards = new();

        public static Tribe TribeDivine;
        public static Tribe TribeFae;
        public static Tribe TribeHumanoid;
        public static Tribe TribeMachine;
        public static Tribe TribePlant;

        private static bool _allCardsDisabled;
        public static bool AllCardsDisabled => _allCardsDisabled;

        private static bool _ruinaCardsDisabled;
        public static bool RuinaCardsDisabled => _ruinaCardsDisabled;

        private static bool _donatorCardsDisabled;
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

                sephirahBundle = LoadBundle("WhistleWind.LobotomyMod/talkingcardssephirah");
                
                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(11);

                Log.LogDebug("Loading challenges...");
                AddChallenges();

                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();

                Log.LogDebug("Loading cards...");
                AddAppearances();

                AddTribes();

                if (sephirahBundle != null)
                    InitSephirahAndDialogue();
                else
                    Log.LogWarning("AssetBundle has not been loaded! .");

                DialogueEventsManager.GenerateDialogueEvents();

                AddCards();

                AddStarterDecks();
                Log.LogDebug("Loading items...");
                AddItems();
                Log.LogDebug("Loading nodes...");
                AddNodes();
                Log.LogDebug("Loading encounters...");
                AddEncounters();
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
        private void InitSephirahAndDialogue()
        {
            Log.LogDebug("Waking up the Sefirot...");
            SephirahHod.Init();
            SephirahYesod.Init();
            // SephirahNetzach.Init();
            // SephirahMalkuth.Init();
            // SephirahChesed.Init();
            // SephirahGebura.Init();
            // SephirahTipherethB.Init();
            // SephirahTipherethA.Init();
            // SephirahBinah.Init();
            // SephirahHokma.Init();
            // Angela.Init();
        }
        private void AddTribes()
        {
            Log.LogDebug("Loading tribes...");
            if (TribeAPI.Enabled)
                TribeAPI.ChangeTribesToTribal();
            else
            {
                TribeDivine = TribeManager.Add(pluginGuid, "DivineTribe", TextureLoader.LoadTextureFromBytes(Artwork.tribeDivine), true, null);
                TribeFae = TribeManager.Add(pluginGuid, "FaeTribe", TextureLoader.LoadTextureFromBytes(Artwork.tribeFae), true, null);
                TribeHumanoid = TribeManager.Add(pluginGuid, "HumanoidTribe", TextureLoader.LoadTextureFromBytes(Artwork.tribeHumanoid), true, null);
                TribeMachine = TribeManager.Add(pluginGuid, "MachineTribe", TextureLoader.LoadTextureFromBytes(Artwork.tribeMachine), true, null);
                TribePlant = TribeManager.Add(pluginGuid, "PlantTribe", TextureLoader.LoadTextureFromBytes(Artwork.tribePlant), true, null);
            }
        }
        private void AddCards()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards.Where(c => c.name.StartsWith("wstl")))
                {
                    // if TribeAPI isn't installed, add custom tribes to AbnormalSigil cards
                    if (!TribeAPI.Enabled)
                    {
                        if (card.name.Contains("Brother"))
                            card.AddTribes(TribeHumanoid);
                        else if (card.name.Contains("Vine") || card.name.Contains("Pumpkin") || card.name.Contains("Sapling"))
                            card.AddTribes(TribePlant);
                    }

                    // add AbnormalSigils cards to the list of cards added by this mod
                    AllLobotomyCards.Add(card);
                }

                return cards;
            };

            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));

            // add Beauty and the Beast as a fallback card
            if (AllCardsDisabled)
                ObtainableLobotomyCards.Add(CardLoader.GetCardByName("wstl_beautyAndBeast"));
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
            MiracleWorker.Register(HarmonyInstance);
            AbnormalBosses.Register(HarmonyInstance);
            AbnormalEncounters.Register(HarmonyInstance);
            BetterRareChances.Register(HarmonyInstance);
        }
        private void AddItems()
        {
            AddBottleCards();
        }
        private void AddStarterDecks()
        {
            string[] randomCards = { "wstl_RANDOM_PLACEHOLDER", "wstl_RANDOM_PLACEHOLDER", "wstl_RANDOM_PLACEHOLDER"};
            if (LobotomyConfigManager.Instance.StarterDeckSize > 0)
            {
                for (int i = 0; i < LobotomyConfigManager.Instance.StarterDeckSize; i++)
                    randomCards.AddToArray("wstl_RANDOM_PLACEHOLDER");
            }

            StarterDeckHelper.AddStarterDeck("Random Mod Cards", Artwork.starterDeckRandom, 0, randomCards);

            StarterDeckHelper.AddStarterDeck("First Day", Artwork.starterDeckControl, 0,
                "wstl_oneSin",
                "wstl_fairyFestival",
                "wstl_oldLady");

            StarterDeckHelper.AddStarterDeck("Lonely Friends", Artwork.starterDeckChildren, 2,
                "wstl_scorchedGirl",
                "wstl_laetitia",
                "wstl_childOfTheGalaxy");

            StarterDeckHelper.AddStarterDeck("Blood Machines", Artwork.starterDeckBloodMachines, 4,
                "wstl_weCanChangeAnything",
                "wstl_allAroundHelper",
                "wstl_singingMachine");

            StarterDeckHelper.AddStarterDeck("People Pleasers", Artwork.starterDeckPeoplePleasers, 5,
                "wstl_todaysShyLook",
                RuinaCardsDisabled ? "wstl_mirrorOfAdjustment" : "wstl_pinocchio",
                "wstl_behaviourAdjustment");

            StarterDeckHelper.AddStarterDeck("Freak Show", Artwork.starterDeckFreakShow, 6,
                "wstl_beautyAndBeast",
                "wstl_voidDream",
                "wstl_queenBee");

            StarterDeckHelper.AddStarterDeck("Apocrypha", Artwork.starterDeckApocrypha, 7,
                "wstl_fragmentOfUniverse",
                "wstl_skinProphecy",
                "wstl_plagueDoctor");

            StarterDeckHelper.AddStarterDeck("Keter", Artwork.starterDeckKeter, 8,
                "wstl_bloodBath",
                "wstl_burrowingHeaven",
                "wstl_snowQueen");

            #region Event Decks
            StarterDeckHelper.AddStarterDeck("Road to Oz", Artwork.starterDeckFairyTale, 13,
                RuinaCardsDisabled ? "wstl_laetitia" : "wstl_theRoadHome",
                "wstl_warmHeartedWoodsman",
                "wstl_wisdomScarecrow",
                RuinaCardsDisabled ? "wstl_snowWhitesApple" : "wstl_ozma");

            StarterDeckHelper.AddStarterDeck("Magical Girls!", Artwork.starterDeckMagicalGirls, 13,
                "wstl_magicalGirlSpade",
                "wstl_magicalGirlHeart",
                "wstl_magicalGirlDiamond",
                RuinaCardsDisabled ? "wstl_voidDream" : "wstl_magicalGirlClover");
            
            StarterDeckHelper.AddStarterDeck("Twilight", Artwork.starterDeckBlackForest, 13,
                "wstl_punishingBird",
                "wstl_bigBird",
                "wstl_judgementBird");
            #endregion
        }
        private void AddEncounters()
        {
            foreach (EncounterBlueprintData i in ModEncounters)
                EncounterManager.Add(i);

            RegionProgression.Instance.regions[0].AddEncounters(StrangePack, BitterPack, StrangeFlock, HelperJuggernaut);
            RegionProgression.Instance.regions[1].AddEncounters(StrangeBees, StrangeCreatures1, WormsNest, StrangeCreatures2, StrangeFish);
            RegionProgression.Instance.regions[2].AddEncounters(StrangeHerd, AlriuneJuggernaut, SpidersNest, SwanJuggernaut);
        }

        public static AssetBundle LoadBundle(string path)
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(path.Replace("\\", ".").Replace("/", ".")))
                return AssetBundle.LoadFromStream(s);
        }

        public static class PackAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            public static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed, creating card pack...");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind's Lobotomy Mod";
                pack.SetTexture(TextureLoader.LoadTextureFromBytes(Artwork.wstl_pack));
                pack.Description = "A set of [count] abnormal cards originating from the bleak and intriguing world of Lobotomy Corporation and Library of Ruina.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }
        public static class TribeAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("tribes.libary");
            public static void ChangeTribesToTribal()
            {
                Log.LogDebug("Tribal Libary detected. Using its tribes instead.");
                TribeDivine = TribalLibary.Plugin.divinebeastTribe;
                TribeFae = TribalLibary.Plugin.fairyTribe;
                TribeHumanoid = TribalLibary.Plugin.humanoidTribe;
                TribeMachine = TribalLibary.Plugin.machineTribe;
                TribePlant = TribalLibary.Plugin.plantTribe;
            }
        }
    }
}
