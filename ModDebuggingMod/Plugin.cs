using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using System.Collections.Generic;
using System.Reflection;
using WhistleWind.Core.Helpers;
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
        private static readonly Harmony HarmonyInstance = new(pluginGuid);
        public static EncounterBlueprintData ModdingEncounter() =>
            New("DebugEncounter")
                    .AddDominantTribes(Tribe.Canine)
                    .AddTurns(
                    CreateTurn("wstl_theNakedNest"),
                    CreateTurn()
                    );

        private void Awake()
        {
            Log = base.Logger;
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            // AddChallenges();
            ItemDebug();

            CARD_DEBUG();
            DebugEncounters();
            ModifyCardList();

            StarterDeckHelper.AddStarterDeck("wstl", "DEBUG HUG", Properties.Resources.starterDeckMagicalGirls, 0, cardNames: new()
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
                        card.abilities = new() { DirectDamage.AbilityID };
                        card.SetTargetedSpell();
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
                RegionProgression.Instance.regions[i].AddEncounters(ModdingEncounter());
            }
        }
    }
}
