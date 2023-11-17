using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using System.Collections.Generic;
using System.Reflection;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod;
using static InscryptionAPI.Encounters.EncounterManager;

namespace ModDebuggingMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(LobotomyPlugin.pluginGuid, BepInDependency.DependencyFlags.HardDependency)]

    public partial class Plugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.moddebuggingmod";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "Mod Debugging Mod";
        private const string pluginVersion = "1.0.0";

        internal static ManualLogSource Log;
        private static readonly Harmony HarmonyInstance = new(pluginGuid);
        public static EncounterBlueprintData ModdingEncounter() =>
            New("DebugEncounter")
                    .AddDominantTribes(Tribe.Canine)
                    .AddTurns(
                    CreateTurn()
                    //CreateTurn("wstlcard", "wstlcard", "wstlcard", "wstlcard")
                    );

        private void Awake()
        {
            Log = base.Logger;
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            //AddChallenges();
            //ItemDebug();
            Ability_Test();
            CARD_DEBUG();
            DebugEncounters();
            //ModifyCardList();

            StarterDeckHelper.AddStarterDeck("wstl", "DEBUG HUG", "starterDeckMagicalGirls", 0, cardNames: new()
            {
                "Squirrel",
                "wstlcard",
                "wstlcard"
            });

            Logger.LogInfo($"{pluginName} loaded.");
        }

        private void ModifyCardList()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards)
                {
                    if (card.name == "Squirrel")
                    {
                        card.AddTraits(Trait.Gem);
                        //card.SetPixelAlternatePortrait(TextureLoader.LoadTextureFromFile("allAroundHelper_emission"));
                        //card.abilities = new() { Reflector.ability };
                        //card.SetEvolve(CardLoader.GetCardByName("wstl_apostleMoleman"), 1)
                        //.SetHideStats()
                        //.SetBaseAttackAndHealth(0, 7)
                        //.SetGlobalSpell()
                        //.AddSpecialAbilities(SpecialTriggeredAbility.Shapeshifter)
                        //;
                    }
                }

                return cards;
            };
        }

        private void DebugEncounters()
        {
            for (int i = 0; i < 3; i++)
            {
                RegionProgression.Instance.regions[i].encounters.Clear();
                RegionProgression.Instance.regions[i].encounters = new() { ModdingEncounter() };
            }
        }
    }
}
