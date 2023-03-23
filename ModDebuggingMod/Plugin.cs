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
using WhistleWindLobotomyMod.Core.Opponents;
using static DiskCardGame.EncounterBlueprintData;
using static InscryptionAPI.Encounters.EncounterManager;

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
                return New("DebugEncounter")
                    .AddDominantTribes(Tribe.Canine)
                    .AddTurns(
                    CreateTurn(),
                    CreateTurn()
                    );
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

            // TestField();

            // clears regions and add debug encounter
/*            for (int i = 0; i < 3; i++)
            {
                RegionProgression.Instance.regions[i].encounters.Clear();
                RegionProgression.Instance.regions[i].AddEncounters(LobotomyEncounterManager.HelperJuggernaut);
            }*/


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
    }
}
