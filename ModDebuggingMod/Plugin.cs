using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod;
using static InscryptionAPI.Encounters.EncounterManager;

namespace ModDebuggingMod {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]

    [BepInDependency(LobotomyPlugin.pluginGuid, BepInDependency.DependencyFlags.HardDependency)]

    public partial class Plugin : BaseUnityPlugin {
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
                    CreateTurn("wstlcard", "wstlcard", "wstlcard", "wstlcard"),
                    CreateTurn("wstlcard", "wstlcard", "wstlcard", "wstlcard")
                    );

        private void Awake() {
            Log = base.Logger;
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            //AddChallenges();
            //ItemDebug();
            Ability_Test();
            CARD_DEBUG();
            //PrintCardsToConsole("mag");
            //DebugEncounters();
            //ModifyCardList();

            //PeltManager.New(pluginGuid, CardLoader.GetCardByName("Squirrel"), 0, 0, 4, () => CardManager.AllCardsCopy);

            StarterDeckHelper.AddStarterDeck("wstl", "DEBUG HUG", "starterDeckMagicalGirls.png", 0, cardNames: new()
            {
                "Squirrel",
                "wstlcard",
                "wstlcard"
            });

            Logger.LogInfo($"{pluginName} loaded.");
        }

        private void ModifyCardList() {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards) {
                foreach (CardInfo card in cards) {
                    if (card.name == "Squirrel") {
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

        private void DebugEncounters() {
            for (int i = 0; i < 3; i++) {
                RegionProgression.Instance.regions[i].encounters.Clear();
                RegionProgression.Instance.regions[i].encounters = new() { ModdingEncounter() };
            }
        }

        private void DebugEncounterInfo() {
            /*            for (int i = 0; i < RegionProgression.Instance.regions.Count; i++) {
                RegionData region = RegionProgression.Instance.regions[i];

                Log.LogInfo($"Encounters for region {i}: " + region.name);
                for (int j = 0; j < RegionProgression.Instance.regions[i].encounters.Count; j++) {
                    EncounterBlueprintData encounter = RegionProgression.Instance.regions[i].encounters[j];
                    List<List<EncounterBlueprintData.CardBlueprint>> turns = encounter.turns;

                    Log.LogInfo("|   " + encounter.name + ":");
                    Log.LogInfo($"|   Difficulty: [{encounter.minDifficulty}, {encounter.maxDifficulty}]");
                    Log.LogInfo($"|   Tribes:");
                    for (int h = 0; h < encounter.dominantTribes.Count; h++) {
                        Log.LogInfo($"|   |   {encounter.dominantTribes[h]}");
                    }

                    Log.LogInfo($"|   Replacements:");
                    for (int p = 0; p < encounter.randomReplacementCards.Count; p++) {
                        Log.LogInfo("|   |   " + encounter.randomReplacementCards[p].name);
                    }
                    Log.LogInfo($"|   Turns:");
                    for (int k = 0; k < turns.Count; k++) {
                        Log.LogInfo($"|   |   Turn {k}:");
                        for (int m = 0; m < turns[k].Count; m++) {
                            Log.LogInfo($"|   |   |   {turns[k][m].card?.name}");
                            Log.LogInfo($"|   |   |   Difficulty: [{turns[k][m].minDifficulty}, {turns[k][m].maxDifficulty}]");
                            Log.LogInfo($"|   |   |   Difficulty replacement: {turns[k][m].replacement?.name} [{turns[k][m].difficultyReplace}, {turns[k][m].difficultyReq}]");
                            Log.LogInfo($"|   |   |   Random replacement: {turns[k][m].randomReplaceChance}");
                        }
                    }
                }
            }*/

            /*            List<EncounterBlueprintData> data = new() {
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/ProspectorBossP1"),
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/ProspectorBossP2"),
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/AnglerBossP1"),
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/AnglerBossP2"),
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/TrapperTraderBossP1"),
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/LeshyBossP1"),
                            ResourceBank.Get<EncounterBlueprintData>("Data/EncounterBlueprints/Part1/PirateSkullBossP1")
                        };
                        for (int i = 0; i < data.Count; i++) {
                            EncounterBlueprintData encounter = data[i];
                            List<List<EncounterBlueprintData.CardBlueprint>> turns = encounter.turns;

                            Log.LogInfo(encounter.name + ":");
                            Log.LogInfo($"|   Difficulty: [{encounter.minDifficulty}, {encounter.maxDifficulty}]");
                            Log.LogInfo($"|   Tribes:");
                            for (int h = 0; h < encounter.dominantTribes.Count; h++) {
                                Log.LogInfo($"|   |   {encounter.dominantTribes[h]}");
                            }

                            Log.LogInfo($"|   Replacements:");
                            for (int p = 0; p < encounter.randomReplacementCards.Count; p++) {
                                Log.LogInfo("|   |   " + encounter.randomReplacementCards[p].name);
                            }
                            Log.LogInfo($"|   Turns:");
                            for (int k = 0; k < turns.Count; k++) {
                                Log.LogInfo($"|   |   Turn {k}:");
                                for (int m = 0; m < turns[k].Count; m++) {
                                    Log.LogInfo($"|   |   |   {turns[k][m].card?.name}");
                                    Log.LogInfo($"|   |   |   Difficulty: [{turns[k][m].minDifficulty}, {turns[k][m].maxDifficulty}]");
                                    Log.LogInfo($"|   |   |   Difficulty replacement: {turns[k][m].replacement?.name} [{turns[k][m].difficultyReplace}, {turns[k][m].difficultyReq}]");
                                    Log.LogInfo($"|   |   |   Random replacement: {turns[k][m].randomReplaceChance}");
                                }
                            }
                        }*/
        }

        private void PrintCardsToConsole(string pluginPrefix) {
            foreach (CardInfo info in CardManager.AllCardsCopy.Where(x => x.ModPrefixIs(pluginPrefix))) {
                string gems = "";
                foreach (GemType type in info.gemsCost) {
                    gems += type.ToString() + " ";
                }

                Log.LogInfo($"Card: {info.name} ({info.displayedName}) Rare: {info.HasCardMetaCategory(CardMetaCategory.Rare)}");
                Log.LogInfo($"- Stats: {info.Attack}/{info.Health}");
                if (info.SpecialStatIcon != SpecialStatIcon.None) {
                    StatIconInfo stat = StatIconInfo.GetIconInfo(info.SpecialStatIcon);
                    Log.LogInfo($"- Stat Icon: {stat.rulebookName} ({stat.iconType}) {stat.appliesToAttack}/{stat.appliesToHealth}");
                }
                Log.LogInfo($"- Blood: {info.cost}");
                Log.LogInfo($"- Bones: {info.bonesCost}");
                Log.LogInfo($"- Energy: {info.energyCost}");
                Log.LogInfo($"- Gems: {gems}");
                if (info.Abilities.Count > 0) {
                    Log.LogInfo("Abilities:");
                    foreach (Ability ability in info.Abilities) {
                        Log.LogInfo($"- {AbilitiesUtil.GetInfo(ability).rulebookName} ({ability})");
                    }
                }
                Log.LogInfo("\n");
            }
        }
    }
}
