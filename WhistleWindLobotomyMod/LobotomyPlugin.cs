using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using Sirenix.Utilities;
using System.Linq;
using System.Reflection;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Challenges;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Opponents.AbnormalEncounterData;

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

        private void OnDisable()
        {
            HarmonyInstance.UnpatchSelf();
        }

        private void Awake()
        {
            Log = base.Logger;
            ConfigManager.Instance.BindConfig();

            if (!ConfigManager.Instance.ModEnabled)
            {
                Log.LogWarning($"{pluginName} is disabled in the configuration. Things will likely break.");
            }
            else
            {
                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                if (ConfigManager.Instance.NumOfBlessings > 11)
                    ConfigManager.Instance.SetBlessings(11);

                DialogueEventsManager.GenerateDialogueEvents();
                Log.LogDebug("Loading challenges...");
                AddChallenges();
                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();
                Log.LogDebug("Loading cards...");
                TestingDummy_XXXXX();
                AddAppearances();
                AddCards();
                AddStarterDecks();
                Log.LogDebug("Loading nodes...");
                AddNodes();
                Log.LogDebug("Loading encounters...");
                AddEncounters();
                if (PackAPI.Enabled)
                    PackAPI.CreateCardPack();

                Log.LogInfo($"The clock is at [{ConfigManager.Instance.NumOfBlessings}].");
                Log.LogInfo($"Plugin loaded! Let's get to work manager!");
            }
        }
        private void AddAppearances() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));

        private void AddSpecialAbilities() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));

        private void AddNodes()
        {
            Node_ModCardChoice();
        }
        private void AddCards()
        {
            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));

            if (ConfigManager.Instance.NoDonators)
                Log.LogDebug("'No Donators' is set to true. Certain cards have been removed from the pool of obtainable cards.");
        }
        private void AddAbilities()
        {
            Ability_TimeMachine();

            Ability_Apostle();
            Ability_TrueSaviour();
            Ability_Confession();

            if (ConfigManager.Instance.RevealSpecials)
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
        private static void AddStarterDecks()
        {
            StarterDeckHelper.AddStartDeck("Debug", Artwork.starterDeckControl, new()
            {
                CardLoader.GetCardByName("wstl_testingDummy"),
                CardLoader.GetCardByName("wstl_testingDummy"),
                CardLoader.GetCardByName("wstl_testingDummy")
            }, 0);
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
                    _enabled ??= Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
                    return (bool)_enabled;
                }
            }
            public static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed, creating card pack...");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind's Lobotomy Mod";
                pack.SetTexture(TextureLoader.LoadTextureFromBytes(Artwork.wstl_pack));
                pack.Description = "A set of [count] cards based on the abnormalities from Lobotomy Corporation and Library of Ruina.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }
    }
}
