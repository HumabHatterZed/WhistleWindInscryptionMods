using BepInEx;
using BepInEx.Logging;
using System;
using System.Reflection;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DiskCardGame;
using UnityEngine;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Saves;
using InscryptionAPI.Helpers;
using InscryptionAPI.Regions;
using InscryptionAPI.Ascension;
using InscryptionAPI.Encounters;
using System.Linq;
using Sirenix.Utilities;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]

    public partial class WstlPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.lobotomycorp";
        public const string pluginName = "WhistleWind Lobotomy Corp";
        private const string pluginVersion = "1.1.0";

        internal static ManualLogSource Log;
        private static Harmony harmony;
        public static string Directory;

        private void Awake()
        {
            WstlPlugin.Log = base.Logger;
            harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), pluginGuid);
            ConfigUtils.Instance.BindConfig();

            if (!ConfigUtils.Instance.ModEnabled)
            {
                Logger.LogWarning($"{pluginName} is loaded but is disabled in the configuration.");
            }
            else
            {
                if (ConfigUtils.Instance.NumOfBlessings > 11)
                {
                    ConfigUtils.Instance.SetBlessings(11);
                }
                AddAbilities();
                AddSpecialAbilities();
                AddAppearances();
                AddCards();
                AddNodes();
                //AddEncounters();
                AddStarterDecks();
                Logger.LogInfo($"The clock is at [{ConfigUtils.Instance.NumOfBlessings}].");
                Logger.LogInfo($"{pluginName} loaded! Let's get to work manager!");
            }
        }
        private void AddNodes()
        {
            Node_ModCardChoice();
        }
        private void AddAppearances()
        {
            AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
        }
        private void AddSpecialAbilities()
        {
            AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));
        }
        private static void AddEncounters()
        {
            var encounterData = ScriptableObject.CreateInstance<EncounterBlueprintData>();

            encounterData.name = "wstl_debug";
            encounterData.turns = new List<List<EncounterBlueprintData.CardBlueprint>>
            {
                new List<EncounterBlueprintData.CardBlueprint> { EncounterHelper.CreateCardBlueprint("Sniper") }
            };
            RegionProgression.Instance.regions[0].encounters.Clear();
            RegionProgression.Instance.regions[0].AddEncounters(encounterData);
        }
        private static void AddStarterDecks()
        {
            StarterDeckHelper.AddStartDeck("First Day", Resources.starterDeckControl, new()
            {
                CardLoader.GetCardByName("wstl_oneSin"),
                CardLoader.GetCardByName("wstl_fairyFestival"),
                CardLoader.GetCardByName("wstl_oldLady")
            }, 0);
            StarterDeckHelper.AddStartDeck("Road to Oz", Resources.starterDeckFairyTale, new()
            {
                CardLoader.GetCardByName("WolfCub"),
                CardLoader.GetCardByName("wstl_warmHeartedWoodsman"),
                CardLoader.GetCardByName("wstl_wisdomScarecrow")

            }, 3);
            /*StarterDeckHelper.AddStartDeck("Blood Machines", Resources.starterDeckBloodMachines, new()
            {
                CardLoader.GetCardByName("wstl_allAroundHelper"),
                CardLoader.GetCardByName("wstl_weCanChangeAnything"),
                CardLoader.GetCardByName("wstl_youMustBeHappy")
            }, 4);*/
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
        private void AddAbilities()
        {
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
            Ability_TimeMachine();
            Ability_Nettles();
            Ability_Spores();
            Ability_Witness();
            Ability_Corrector();

            Ability_Apostle();
            Ability_TrueSaviour();
            Ability_Confession();

            if (ConfigUtils.Instance.RevealSpecials)
            {
                Log.LogDebug("Adding special ability rulebook entries.");
                AccessTools.GetDeclaredMethods(typeof(WstlPlugin)).Where(mi => mi.Name.StartsWith("Rulebook")).ForEach(mi => mi.Invoke(this, null));
            }
        }
        private void AddCards()
        {
            TestingDummy_XXXXX();
            TrainingDummy_00000();
            ScorchedGirl_F0102();
            OneSin_O0303();
            HundredsGoodDeeds_O0303();
            MagicalGirlHeart_O0104();
            QueenOfHatred_O0104();
            QueenOfHatredTired_O0104();
            HappyTeddyBear_T0406();
            RedShoes_O0408();
            Theresia_T0909();
            OldLady_O0112();
            NamelessFetus_O0115();
            NamelessFetusAwake_O0115();
            WallLady_F0118();
            NothingThere_O0620();
            NothingThereTrue_O0620();
            NothingThereEgg_O0620();
            NothingThereFinal_O0620();
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
            Spiderling_O0243();
            SpiderBrood_O0243();
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
            BloodBath_T0551();
            BloodBath1_T0551();
            BloodBath2_T0551();
            BloodBath3_T0551();
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
            //  BigEyes_O0263();
            //  SmallBeak_O0263();
            //  LongArms_O0263();
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
            VoidDream_T0299();
            VoidDreamRooster_T0299();
            GraveOfBlossoms_O04100();
            TheFirebird_O02101();
            Yin_O05102();
            Yang_O07103();
            YinYangHead_O07103();
            YinYangBody_O07103();
            BackwardClock_D09104();
            DellaLuna_D01105();
            ArmyInPink_D01106();
            ArmyInBlack_D01106();
            Ppodae_D02107();
            PpodaeBuff_D02107();
            ParasiteTree_D04108();
            ParasiteTreeSapling_D04108();
            MeltingLove_D03109();
            MeltingLoveMinion_D03109();
            HonouredMonk_D01110();
            CloudedMonk_D01110();
        }
    }
}
