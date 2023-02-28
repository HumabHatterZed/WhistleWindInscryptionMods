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

                return New("DebugEncounter") // Create new EncounterBlueprintData
                .AddDominantTribes(Tribe.Canine)
                .AddTurns(
                    CreateTurn("Squirrel"),                             // Play a Squirrel (creates a CardBlueprint with default values)
                    CreateTurn("Squirrel", "Skeleton"),                 // Play a Squirrel and Skeleton
                    CreateTurn())                                       // Create an empty turn (nothing is played)
                .DuplicateTurns(1)                                      // Duplicate the current turn plan once (last 3 turns will repeat once)
                .AddTurn(CreateTurn("Mole", "Mole", "Mole", "Mole"))    // Play 4 Moles
                .AddTurns(
                    CreateTurn(                 // Play 2 Cats that will be replaced with Wolves at difficulty = 4
                        NewCardBlueprint("Cat").SetReplacement("Wolf", 4),
                        NewCardBlueprint("Cat").SetReplacement("Wolf", 4)),
                    CreateTurn(                 // Play 2 Zombies with random chance of replacement of 50% and 25% respectively
                        NewCardBlueprint("Zombie", randomReplaceChance: 50),
                        NewCardBlueprint("Zombie", randomReplaceChance: 25)))
                .DuplicateTurns(2)              // Duplicates the current turn plan twice
                .AddTurns(CreateTurn().DuplicateTurn(9))    // Adds 10 empty turns at the end of the turn plan
                .SyncTurnDifficulties(1, 10);               // ensures all CardBlueprints have the same difficulty values
            }
            // The created turn plan looks like this
            // 1 - Squirrel
            // 2 - Squirrel, Skeleton
            // 3 - 
            // 4 - Squirrel
            // 5 - Squirrel, Skeleton
            // 6 - 
            // 7 - 4 Moles
            // 8 - 2 Cats/Wolves
            // 9 - Zombie (50%), Zombie (25%)
            // Repeat the above sequence 2 more times
            // 10 empty turns
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
