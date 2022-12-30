using APIPlugin;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static WhistleWindLobotomyMod.AbnormalEncounterData;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("whistlewind.inscryption.abnormalsigils", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("whistlewind.inscryption.lobotomymod", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class WstlPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginName = "WhistleWind Lobotomy Corp";
        private const string pluginVersion = "1.3.0";

        internal static ManualLogSource Log;
        private static Harmony harmony = new(pluginGuid);

        public static List<CardInfo> AllLobotomyCards = new();
        public static List<CardInfo> ObtainableLobotomyCards = new();

        private static bool _donatorCardsDisabled;
        public static bool DonatorCardsDisabled => _donatorCardsDisabled;

        private void OnDisable() => harmony.UnpatchSelf();
        private void Start()
        {
            if (NewVersion.Enabled)
                Log.LogWarning("A NEW VERSION OF THIS MOD IS ALSO INSTALLED!" +
                    "\nIf you have just updated, please remove both this DLL from your plugins folder, as well as the old config file ending in 'lobotomycorp'." +
                    " If you are trying to play the pre-2.0 version, please remove or otherwise disable the 2.0 version.");
            else if (ConfigManager.Instance.ModEnabled)
            {
                if (DonatorCardsDisabled)
                    Log.LogInfo("No Donators is set to true. Certain cards have been removed from the pool of obtainable cards.");

                Log.LogInfo($"There are [{AllLobotomyCards.Count}] total cards and [{ObtainableLobotomyCards.Count}] obtainable cards.");

                Log.LogInfo($"The Clock is at [{ConfigManager.Instance.NumOfBlessings}].");
            }
        }
        private void Awake()
        {
            WstlPlugin.Log = base.Logger;

            if (NewVersion.Enabled)
                return;

            ConfigManager.Instance.BindConfig();

            if (!ConfigManager.Instance.ModEnabled)
                Logger.LogWarning($"{pluginName} is disabled in the configuration, some things might break.");
            else
            {
                _donatorCardsDisabled = ConfigManager.Instance.NoDonators;

                harmony.PatchAll();

                if (ConfigManager.Instance.NumOfBlessings > 11)
                    ConfigManager.Instance.SetBlessings(11);

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

                Logger.LogInfo($"{pluginName} loaded! Let's get to work manager!");
            }
        }
        private void AddAppearances() =>
            AccessTools.GetDeclaredMethods(typeof(WstlPlugin))
            .Where(mi => mi.Name.StartsWith("Appearance"))
            .ForEach(mi => mi.Invoke(this, null));
        private void AddSpecialAbilities() =>
            AccessTools.GetDeclaredMethods(typeof(WstlPlugin))
            .Where(mi => mi.Name.StartsWith("SpecialAbility"))
            .ForEach(mi => mi.Invoke(this, null));
        private void AddNodes() => Node_ModCardChoice();
        private void AddChallenges()
        {
            MiracleWorker.Register(harmony);
            AbnormalBosses.Register(harmony);
            AbnormalEncounters.Register(harmony);
            BetterRareChances.Register(harmony);
        }
        private static void AddStarterDecks()
        {
            CardInfo rand = CardLoader.GetCardByName("wstl_RANDOM_PLACEHOLDER");
            List<CardInfo> randomCards = new()
            {
                rand.Clone() as CardInfo,
                rand.Clone() as CardInfo,
                rand.Clone() as CardInfo };
            if (ConfigManager.Instance.StarterDeckSize > 0)
            {
                for (int i = 0; i < ConfigManager.Instance.StarterDeckSize; i++)
                    randomCards.Add(rand.Clone() as CardInfo);
            }

            StarterDeckHelper.AddStartDeck("Random Mod Cards", Resources.starterDeckRandom, randomCards, 0);
            StarterDeckHelper.AddStartDeck("First Day", Resources.starterDeckControl, new()
            {
                CardLoader.GetCardByName("wstl_oneSin"),
                CardLoader.GetCardByName("wstl_fairyFestival"),
                CardLoader.GetCardByName("wstl_oldLady")
            }, 0);
            StarterDeckHelper.AddStartDeck("Lonely Friends", Resources.starterDeckChildren, new()
            {
                CardLoader.GetCardByName("wstl_scorchedGirl"),
                CardLoader.GetCardByName("wstl_laetitia"),
                CardLoader.GetCardByName("wstl_childOfTheGalaxy")
            }, 2);
            StarterDeckHelper.AddStartDeck("Road to Oz", Resources.starterDeckFairyTale, new()
            {
                CardLoader.GetCardByName("WolfCub"),
                CardLoader.GetCardByName("wstl_warmHeartedWoodsman"),
                CardLoader.GetCardByName("wstl_wisdomScarecrow")
            }, 3);
            StarterDeckHelper.AddStartDeck("Blood Machines", Resources.starterDeckBloodMachines, new()
            {
                CardLoader.GetCardByName("wstl_weCanChangeAnything"),
                CardLoader.GetCardByName("wstl_allAroundHelper"),
                CardLoader.GetCardByName("wstl_singingMachine")
            }, 4);
            StarterDeckHelper.AddStartDeck("Magical Girls!", Resources.starterDeckMagicalGirls, new()
            {
                CardLoader.GetCardByName("wstl_magicalGirlHeart"),
                CardLoader.GetCardByName("wstl_magicalGirlDiamond"),
                CardLoader.GetCardByName("wstl_magicalGirlSpade")
            }, 8);
            StarterDeckHelper.AddStartDeck("Twilight", Resources.starterDeckBlackForest, new()
            {
                CardLoader.GetCardByName("wstl_punishingBird"),
                CardLoader.GetCardByName("wstl_bigBird"),
                CardLoader.GetCardByName("wstl_judgementBird")
            }, 13);
        }
        private void AddEncounters()
        {
            EncounterManager.Add(StrangePack);
            EncounterManager.Add(BitterPack);
            EncounterManager.Add(StrangeFlock);
            EncounterManager.Add(HelperJuggernaut);
            EncounterManager.Add(StrangeBees);
            EncounterManager.Add(StrangeCreatures1);
            EncounterManager.Add(WormsNest);
            EncounterManager.Add(StrangeCreatures2);
            EncounterManager.Add(StrangeFish);
            EncounterManager.Add(StrangeHerd);
            EncounterManager.Add(AlriuneJuggernaut);
            EncounterManager.Add(SpidersNest);
            EncounterManager.Add(SwanJuggernaut);
            RegionProgression.Instance.regions[0].AddEncounters(StrangePack, BitterPack, StrangeFlock, HelperJuggernaut);
            RegionProgression.Instance.regions[1].AddEncounters(StrangeBees, StrangeCreatures1, WormsNest, StrangeCreatures2, StrangeFish);
            RegionProgression.Instance.regions[2].AddEncounters(StrangeHerd, AlriuneJuggernaut, SpidersNest, SwanJuggernaut);
        }
        private void AddAbilities()
        {
            if (AbnormalSigils.Enabled)
            {
                Log.LogWarning("Abnormal Sigils is installed! There may be conflicts!");
            }
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
            Ability_Alchemist();
            Ability_Nettles();
            Ability_Spores();
            Ability_Witness();
            Ability_Corrector();

            Ability_TimeMachine();
            Ability_Apostle();
            Ability_TrueSaviour();
            Ability_Confession();

            if (ConfigManager.Instance.RevealSpecials)
            {
                Log.LogDebug("Adding rulebook entries for special abilities.");
                AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("Rulebook")).ForEach(mi => mi.Invoke(this, null));
            }
        }
        private void AddCards()
        {
            // TestingDummy_XXXXX();

            TrainingDummy_00000();
            ScorchedGirl_F0102();
            OneSin_O0303();
            HundredsGoodDeeds_O0303();

            QueenOfHatred_O0104();
            QueenOfHatredTired_O0104();
            MagicalGirlHeart_O0104();

            HappyTeddyBear_T0406();
            RedShoes_O0408();
            Theresia_T0909();
            OldLady_O0112();

            NamelessFetusAwake_O0115();
            NamelessFetus_O0115();

            WallLady_F0118();

            NothingThereFinal_O0620();
            NothingThereEgg_O0620();
            NothingThereTrue_O0620();
            NothingThere_O0620();

            MHz176_T0727();
            SingingMachine_O0530();
            SilentOrchestra_T0131();
            SilentEnsemble_T0131();
            WarmHeartedWoodsman_F0532();
            SnowQueen_F0137();
            SnowQueenIceHeart_F0137();
            SnowQueenIceBlock_F0137();
            BigBird_O0240();
            AllAroundHelper_T0541();
            SnowWhitesApple_F0442();
            SnowWhitesVine_F0442();
            SpiderBud_O0243();

            SpiderBrood_O0243();
            Spiderling_O0243();

            BeautyAndBeast_O0244();
            PlagueDoctor_O0145();
            WhiteNight_T0346();
            ApostleScythe_T0346();
            ApostleScytheDown_T0346();
            ApostleStaff_T0346();
            ApostleStaffDown_T0346();
            ApostleSpear_T0346();
            ApostleSpearDown_T0346();
            ApostleHeretic_T0346();
            DontTouchMe_O0547();
            RudoltaSleigh_F0249();
            QueenBee_T0450();
            QueenBeeWorker_T0450();

            BloodBath3_T0551();
            BloodBath2_T0551();
            BloodBath1_T0551();
            BloodBath_T0551();

            CanOfWellCheers_F0552();
            Alriune_T0453();
            ForsakenMurderer_T0154();
            ChildOfTheGalaxy_O0155();
            PunishingBird_O0256();
            RedHoodedMercenary_F0157();
            WillBeBadWolf_F0258();
            YoureBald_BaldIsAwesome();
            FragmentOfUniverse_O0360();
            CrumblingArmour_O0561();
            JudgementBird_O0262();
            ApocalypseBird_O0263();
            MagicalGirlDiamond_O0164();
            KingOfGreed_O0164();
            TheLittlePrince_O0466();
            TheLittlePrinceMinion_O0466();
            Laetitia_O0167();
            LaetitiaFriend_O0167();
            FuneralOfButterflies_T0168();
            DerFreischutz_F0169();
            DreamOfABlackSwan_F0270();
            FirstBrother_F0270();
            SecondBrother_F0270();
            ThirdBrother_F0270();
            FourthBrother_F0270();
            FifthBrother_F0270();
            SixthBrother_F0270();
            DreamingCurrent_T0271();
            BurrowingHeaven_O0472();
            MagicalGirlSpade_O0173();
            KnightOfDespair_O0173();
            TheNakedNest_O0274();
            TheNakedWorm_O0274();
            MountainOfBodies_T0175();
            MountainOfBodies2_T0175();
            MountainOfBodies3_T0175();
            Schadenfreude_O0576();
            HeartOfAspiration_O0977();
            NotesFromResearcher_T0978();
            FleshIdol_T0979();
            GiantTreeSap_T0980();
            MirrorOfAdjustment_O0981();
            ShelterFrom27March_T0982();
            FairyFestival_F0483();
            MeatLantern_O0484();
            WeCanChangeAnything_T0985();
            ExpressHellTrain_T0986();
            WisdomScarecrow_F0187();
            DimensionalRefraction_O0388();
            CENSORED_O0389();
            CENSOREDMinion_O0389();
            SkinProphecy_T0990();
            WorldPortrait_O0991();
            TodaysShyLook_O0192();
            TodaysShyLookAngry_O0192();
            TodaysShyLookHappy_O0192();
            TodaysShyLookNeutral_O0192();
            BlueStar_O0393();
            BlueStar2_O0393();
            YouMustBeHappy_T0994();
            LuminousBracelet_O0995();
            BehaviourAdjustment_O0996();
            OldFaithAndPromise_T0997();
            Porccubus_O0298();

            VoidDreamRooster_T0299();
            VoidDream_T0299();

            GraveOfBlossoms_O04100();
            TheFirebird_O02101();
            Yin_O05102();
            Yang_O07103();
            YinYangHead_O07103();
            YinYangBody_O07103();

            BackwardClock_D09104();
            DellaLuna_D01105();

            ArmyInBlack_D01106();
            ArmyInPink_D01106();

            PpodaeBuff_D02107();
            Ppodae_D02107();

            ParasiteTree_D04108();
            ParasiteTreeSapling_D04108();
            MeltingLove_D03109();
            MeltingLoveMinion_D03109();

            CloudedMonk_D01110();
            HonouredMonk_D01110();


            // Opponent only cards
            Rudolta_Mule();
            ApostleGuardian_T0346();
            ApostleGuardianDown_T0346();
            ApostleMoleman_T0346();
            ApostleMolemanDown_T0346();
            SkeletonShrimp_F0552();
            Crumpled_Can();

            Card_RANDOM_PLACEHOLDER();
        }

        public static class PackAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            public static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed, creating card pack...");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind's Lobotomy Mod";
                pack.SetTexture(WstlTextureHelper.LoadTextureFromResource(Resources.wstl_pack));
                pack.Description = "A set of [count] abnormal cards originating from the bleak and intriguing world of Lobotomy Corporation.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }

        public static class AbnormalSigils
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("whistlewind.inscryption.abnormalsigils");
        }
        public static class NewVersion
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("whistlewind.inscryption.lobotomymod");
        }
    }
}
