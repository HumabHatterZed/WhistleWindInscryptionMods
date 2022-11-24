using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using Sirenix.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
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

        public static AssetBundle sephirahBundle;

        private void OnDisable()
        {
            /*            if (sephirahBundle != null)
                            sephirahBundle.Unload(false);*/
            HarmonyInstance.UnpatchSelf();
        }

        private void Awake()
        {
            Log = base.Logger;
            ConfigManager.Instance.BindConfig();

            if (!ConfigManager.Instance.ModEnabled)
                Log.LogWarning($"{pluginName} is disabled in the configuration. Things will likely break.");
            else
            {
                sephirahBundle = LoadBundle("WhistleWindLobotomyMod/talkingcardssephirah");

                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                if (ConfigManager.Instance.NumOfBlessings > 11)
                    ConfigManager.Instance.SetBlessings(11);

                Log.LogDebug("Loading challenges...");
                AddChallenges();

                Log.LogDebug("Loading abilities...");
                AddAbilities();
                AddSpecialAbilities();
                Log.LogDebug("Loading cards...");
                AddAppearances();

                if (sephirahBundle != null)
                    InitSephirahAndDialogue();
                else
                    Log.LogWarning("AssetBundle has not been loaded! The Sefirot will remain asleep.");

                DialogueEventsManager.GenerateDialogueEvents();

                AddCards();
                TestingDummy_XXXXX();

                AddStarterDecks();
                Log.LogDebug("Loading nodes...");
                AddNodes();
                Log.LogDebug("Loading encounters...");
                AddEncounters();
                if (PackAPI.Enabled)
                    PackAPI.CreateCardPack();

                Log.LogInfo($"Plugin loaded! Let's get to work manager!");
            }
        }
        private void Start() => Log.LogInfo($"The clock is at [{ConfigManager.Instance.NumOfBlessings}].");
        private void AddAppearances() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
        private void AddSpecialAbilities() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));
        private void AddNodes() => AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Node")).ForEach(mi => mi.Invoke(this, null));
        private void InitSephirahAndDialogue()
        {
            Log.LogDebug("Waking up the Sefirot...");
            SephirahHod.Init();
            SephirahYesod.Init();
        }
        private void AddCards()
        {
            AccessTools.GetDeclaredMethods(typeof(LobotomyPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));

            if (ConfigManager.Instance.NoDonators)
                Log.LogDebug("'No Donators' is set to true. Certain cards have been removed from the pool of obtainable cards.");

            if (ConfigManager.Instance.NoRuina)
                Log.LogDebug("'No Ruina' is set to true. Certain cards have been removed from the pool of obtainable cards.");
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
        private void AddStarterDecks()
        {
            StarterDeckHelper.AddStarterDeck("Debug", Artwork.starterDeckControl, 0,
                "wstl_testingDummy", "wstl_testingDummy", "wstl_testingDummy");

            StarterDeckHelper.AddStarterDeck("First Day", Artwork.starterDeckControl, 0,
                "wstl_oneSin", "wstl_fairyFestival", "wstl_oldLady");
            StarterDeckHelper.AddStarterDeck("Lonely Friends", Artwork.starterDeckChildren, 2,
                "wstl_scorchedGirl", "wstl_laetitia", "wstl_childOfTheGalaxy");
            StarterDeckHelper.AddStarterDeck("Blood Machines", Artwork.starterDeckBloodMachines, 4,
                "wstl_weCanChangeAnything", "wstl_allAroundHelper", "wstl_singingMachine");
            StarterDeckHelper.AddStarterDeck("Road to Oz", Artwork.starterDeckFairyTale, 8,
                "wstl_theRoadHome", "wstl_warmHeartedWoodsman", "wstl_wisdomScarecrow");
            StarterDeckHelper.AddStarterDeck("Magical Girls!", Artwork.starterDeckMagicalGirls, 10,
                "wstl_magicalGirlHeart", "wstl_magicalGirlDiamond", ConfigManager.Instance.NoRuina ? "wstl_magicalGirlSpade" : "wstl_magicalGirlClover");
            StarterDeckHelper.AddStarterDeck("Twilight", Artwork.starterDeckBlackForest, 13,
                "wstl_punishingBird", "wstl_bigBird", "wstl_judgementBird");
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
