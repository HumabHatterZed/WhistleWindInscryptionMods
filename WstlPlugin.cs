using BepInEx;
using BepInEx.Logging;
using BepInEx.Bootstrap;
using System;
using System.Reflection;
using HarmonyLib;
using DiskCardGame;
using UnityEngine;
using InscryptionAPI;
using InscryptionAPI.Regions;
using InscryptionAPI.Encounters;
using System.Linq;
using Sirenix.Utilities;
using Infiniscryption.PackManagement;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Opponents.AbnormalEncounterData;

namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.spells", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class WstlPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginName = "WhistleWind Lobotomy Corp";
        private const string pluginVersion = "2.0.0";

        internal static ManualLogSource Log;
        private static Harmony harmony;

        private void Awake()
        {
            WstlPlugin.Log = base.Logger;
            harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), pluginGuid);
            ConfigManager.Instance.BindConfig();

            if (!ConfigManager.Instance.ModEnabled)
            {
                Logger.LogWarning($"{pluginName} is disabled in the configuration, some things might break.");
            }
            else
            {
                if (ConfigManager.Instance.NumOfBlessings > 11)
                {
                    ConfigManager.Instance.SetBlessings(11);
                }
                Log.LogDebug("Loading challenges...");
                AddChallenges();
                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();
                Log.LogDebug("Loading cards...");
                AddAppearances();
                AddCards();
                AddStarterDecks();
                Log.LogDebug("Loading nodes...");
                AddNodes();
                Log.LogDebug("Loading encounters...");
                AddEncounters();
                if (PackAPI.Enabled)
                    PackAPI.CreateCardPack();


                Logger.LogInfo($"The clock is at [{ConfigManager.Instance.NumOfBlessings}].");
                Logger.LogInfo($"{pluginName} loaded! Let's get to work manager!");
            }
        }
        private void AddAppearances()
        {
            AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
        }
        private void AddSpecialAbilities()
        {
            AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));

            StatIcon_Time();
        }
        private void AddNodes()
        {
            Node_ModCardChoice();
        }
        private void AddCards()
        {
            TestingDummy_XXXXX();

            AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));

            if (ConfigManager.Instance.NoDonators)
                Log.LogDebug("No Donators is set to true. Certain cards have been removed from the pool of obtainable cards.");
        }
        private void AddAbilities()
        {
            // v1.0
            Ability_Punisher();
            Ability_Bloodfiend();
            Ability_Martyr();
            Ability_Aggravating();
            Ability_TeamLeader();
            Ability_Idol();
            Ability_Conductor();
            Ability_Woodcutter();
            Ability_FrozenHeart();
            Ability_FrostRuler();
            Ability_Roots();
            Ability_BroodMother();
            Ability_Cursed();
            Ability_Healer();
            Ability_QueenNest();
            Ability_BitterEnemies();
            Ability_Courageous();
            Ability_SerpentsNest();
            Ability_Assimilator();
            Ability_GroupHealer();
            Ability_Reflector();
            Ability_FlagBearer();
            Ability_Grinder();
            Ability_TheTrain();
            Ability_Burning();
            Ability_Regenerator();
            Ability_Volatile();
            Ability_GiftGiver();
            Ability_Piercing();
            Ability_Scrambler();
            Ability_Gardener();
            Ability_Slime();
            Ability_Marksman();
            Ability_Protector();
            Ability_QuickDraw();
            // v1.1
            Ability_Alchemist();
            Ability_TimeMachine();
            Ability_Nettles();
            Ability_Spores();
            Ability_Witness();
            Ability_Corrector();
            // v2.0
            Ability_ThickSkin();
            Ability_OneSided();
            Ability_Copycat();
            Ability_CatLover();
            Ability_Cowardly();
            Ability_Neutered();
            Ability_NeuteredLatch();
            Ability_RightfulHeir();
            Ability_GreedyHealing();
            // Spells
            Ability_TargetGainStats();
            Ability_TargetGainSigils();
            Ability_TargetGainStatsSigils();

            // special
            Ability_FalseThrone();
            Ability_Nihil();

            Ability_Apostle();
            Ability_TrueSaviour();
            Ability_Confession();

            if (ConfigManager.Instance.RevealSpecials)
            {
                Log.LogDebug("Adding rulebook entries for special abilities.");
                AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("Rulebook")).ForEach(mi => mi.Invoke(this, null));
            }
        }
        private void AddChallenges()
        {
            // RaptureBoss
            // BirdBoss
            // JesterBoss
            // AdultBoss
            MiracleWorker.Register(harmony);
            AbnormalBosses.Register(harmony);
            AbnormalEncounters.Register(harmony);
            BetterRareChances.Register(harmony);
        }
        private static void AddStarterDecks()
        {
            /*StarterDeckHelper.AddStartDeck("Debug", Artwork.starterDeckControl, new()
            {
                CardLoader.GetCardByName("wstl_testingDummy"),
                CardLoader.GetCardByName("wstl_testingDummy"),
                CardLoader.GetCardByName("wstl_testingDummy")
            }, 0);*/
            StarterDeckHelper.AddStartDeck("First Day", Artwork.starterDeckControl, new()
            {
                CardLoader.GetCardByName("wstl_oneSin"),
                CardLoader.GetCardByName("wstl_fairyFestival"),
                CardLoader.GetCardByName("wstl_oldLady")
            }, 0);
            StarterDeckHelper.AddStartDeck("Lonely Friends", Artwork.starterDeckChildren, new()
            {
                CardLoader.GetCardByName("wstl_scorchedGirl"),
                CardLoader.GetCardByName("wstl_laetitia"),
                CardLoader.GetCardByName("wstl_childOfTheGalaxy")
            }, 2);
            /*StarterDeckHelper.AddStartDeck("Lonely Friends", Artwork.starterDeckChildren, new()
            {
                CardLoader.GetCardByName("wstl_scorchedGirl"),
                CardLoader.GetCardByName("wstl_laetitia"),
                CardLoader.GetCardByName("wstl_childOfTheGalaxy")
            }, 3);*/
            StarterDeckHelper.AddStartDeck("Blood Machines", Artwork.starterDeckBloodMachines, new()
            {
                CardLoader.GetCardByName("wstl_weCanChangeAnything"),
                CardLoader.GetCardByName("wstl_allAroundHelper"),
                CardLoader.GetCardByName("wstl_singingMachine")
            }, 4);
            /*StarterDeckHelper.AddStartDeck("Lonely Friends", Artwork.starterDeckChildren, new()
            {
                CardLoader.GetCardByName("wstl_scorchedGirl"),
                CardLoader.GetCardByName("wstl_laetitia"),
                CardLoader.GetCardByName("wstl_childOfTheGalaxy")
            }, 6);*/
            StarterDeckHelper.AddStartDeck("Road to Oz", Artwork.starterDeckFairyTale, new()
            {
                CardLoader.GetCardByName("wstl_theRoadHome"),
                CardLoader.GetCardByName("wstl_warmHeartedWoodsman"),
                CardLoader.GetCardByName("wstl_wisdomScarecrow")
            }, 8);
            StarterDeckHelper.AddStartDeck("Magical Girls!", Artwork.starterDeckMagicalGirls, new()
            {
                CardLoader.GetCardByName("wstl_magicalGirlDiamond"),
                CardLoader.GetCardByName("wstl_magicalGirlHeart"),
                CardLoader.GetCardByName("wstl_magicalGirlClover")
            }, 10);
            StarterDeckHelper.AddStartDeck("Twilight", Artwork.starterDeckBlackForest, new()
            {
                CardLoader.GetCardByName("wstl_punishingBird"),
                CardLoader.GetCardByName("wstl_bigBird"),
                CardLoader.GetCardByName("wstl_judgementBird")
            }, 13);
        }
        private void AddEncounters()
        {
            foreach (EncounterBlueprintData i in ModEncounters)
            {
                EncounterManager.Add(i);
            }
            RegionProgression.Instance.regions[0].AddEncounters(StrangePack, BitterPack, StrangeFlock, HelperJuggernaut);
            RegionProgression.Instance.regions[1].AddEncounters(StrangeBees, StrangeCreatures1, WormsNest, StrangeCreatures2, StrangeFish);
            RegionProgression.Instance.regions[2].AddEncounters(StrangeHerd, AlriuneJuggernaut, SpidersNest, SwanJuggernaut);
        }

        public static class PackAPI
        {
            private static bool? _enabled;
            public static bool Enabled
            {
                get
                {
                    if (_enabled == null)
                        _enabled = Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
                    return (bool)_enabled;
                }
            }
            public static void CreateCardPack()
            {
                Log.LogDebug("PackManager installed, creating card pack...");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind's Lobotomy Mod";
                pack.SetTexture(WstlTextureHelper.LoadTextureFromResource(Artwork.wstl_pack));
                pack.Description = "This card pack adds 91 obtainable cards based on abnormalities.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }
    }
}
