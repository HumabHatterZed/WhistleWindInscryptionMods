using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers;
using InscryptionAPI.Pelts;
using InscryptionAPI.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace ModDebuggingMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]

    public partial class Plugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.moddebuggingmod";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "Mod Debugging Mod";
        private const string pluginVersion = "1.0.0";

        internal static ManualLogSource Log;
        private static Harmony harmony;
        public static EncounterBlueprintData ModdingEncounter
        {
            get
            {
                string name = "ModDebuggingEncounter";
                List<Tribe> tribes = new() { Tribe.Hooved };
                List<CardInfo> replacements = new();
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("Stoat") },//, CreateCardBlueprint("Mole"), CreateCardBlueprint("Mole") },
                    new()
                };
                return BuildBlueprint(name, tribes, 0, 20, replacements, redundant, turns);
            }
        }

        private void Start()
        {
        }
        private void Awake()
        {
            Plugin.Log = base.Logger;
            harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), pluginGuid);
            
            // AddChallenges();

            ItemDebug();
            CARD_DEBUG();

            // clears regions and add debug encounter
            for (int i = 0; i < 3; i++)
            {
                RegionProgression.Instance.regions[i].encounters.Clear();
                RegionProgression.Instance.regions[i].AddEncounters(ModdingEncounter);
            }

            AddStartDeck("DEBUG HUG", Properties.Resources.starterDeckMagicalGirls, new()
            {
                CardLoader.GetCardByName("Squirrel"),
                CardLoader.GetCardByName("wstlcard"),
                CardLoader.GetCardByName("wstlcard")
            }, 0);

            //ModifyCardList();

            Logger.LogInfo($"{pluginName} loaded.");
        }

        private void ModifyCardList()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards)
                {
                    //Log.LogInfo($"{card.HasCardMetaCategory(LobotomyCardHelper.CannotBoostStats)}");
                }

                return cards;
            };
        }

        private static StarterDeckManager.FullStarterDeck AddStartDeck(string title, byte[] icon, List<CardInfo> cards, int unlockLevel = 0)
        {
            StarterDeckInfo starterDeckInfo = ScriptableObject.CreateInstance<StarterDeckInfo>();
            starterDeckInfo.title = title;
            starterDeckInfo.iconSprite = TextureLoader.LoadSpriteFromBytes(icon, new(0.5f, 0.5f));
            starterDeckInfo.cards = cards;
            return StarterDeckManager.Add("wstl", starterDeckInfo, unlockLevel);
        }

        private static EncounterBlueprintData.CardBlueprint CreateCardBlueprint(string cardName, int replacementChance = 25)
        {
            CardInfo info = CardLoader.GetCardByName(cardName);
            return new()
            {
                card = info,
                randomReplaceChance = replacementChance
            };
        }
        private static EncounterBlueprintData BuildBlueprint(
            string name, List<Tribe> tribes, int min, int max,
            List<CardInfo> randomCards, List<Ability> redundantAbilities,
            List<List<EncounterBlueprintData.CardBlueprint>> turns)
        {
            EncounterBlueprintData encounterData = ScriptableObject.CreateInstance<EncounterBlueprintData>();
            encounterData.name = name;
            encounterData.dominantTribes = tribes;
            encounterData.SetDifficulty(min, max);
            encounterData.randomReplacementCards = randomCards;
            encounterData.redundantAbilities = redundantAbilities;
            encounterData.regionSpecific = true;
            encounterData.turns = turns;
            return encounterData;
        }
    }
}
