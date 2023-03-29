﻿using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using InscryptionAPI.TalkingCards;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Challenges;
using WhistleWindLobotomyMod.Core.Helpers;
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

                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                if (LobotomyConfigManager.Instance.NumOfBlessings > 11)
                    LobotomyConfigManager.Instance.SetBlessings(11);

                DialogueEventsManager.GenerateDialogueEvents();

                AddChallenges();

                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();

                Log.LogDebug("Loading cards...");
                AddAppearances();
                AddTribes();

                AddCards();
                CreateTalkingCards();

                AddStarterDecks();

                Log.LogDebug("Loading items and nodes...");
                AddItems();
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
                    // DebugCardInfo();

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

        }
        private void AddCards()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards.Where(c => c.GetModTag() == "whistlewind.inscryption.abnormalsigils"))
                {
                    // add AbnormalSigils cards to the list of cards added by this mod
                    if (!AllLobotomyCards.Contains(card))
                        AllLobotomyCards.Add(card);
                }

                return cards;
            };

            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));

            if (AllCardsDisabled)
            {
                Log.LogInfo("All mod cards are disabled, adding Standard Training-Dummy Rabbit as a fallback card.");
                ObtainableLobotomyCards.Add(CardLoader.GetCardByName("wstl_trainingDummy"));
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
                RuinaCardsDisabled ? "wstl_mhz176" : "wstl_priceOfSilence");

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
            BuildEncounters();
            for (int i = 0; i < 3; i++)
                RegionProgression.Instance.regions[i].AddEncounters(ModEncounters[i].ToArray());
        }

        private void DebugCardInfo()
        {
            foreach (CardInfo card in AllLobotomyCards)
            {
                Log.LogInfo($"{card.name,-30} | {card.baseAttack,2}/{card.baseHealth,-2} | {card.cost,2}B :: x{card.bonesCost,-2} :: E{card.energyCost,-2}");
            }
        }

        public static class PackAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            public static void CreateCardPack()
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
