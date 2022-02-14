using APIPlugin;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]

    public partial class Plugin : BaseUnityPlugin
    {
        public const string pluginGUID = "whistlewind.inscryption.lobotomycorp";
        public const string pluginName = "WhistleWindLobotomyCorp";
        private const string pluginVersion = "0.87.51.63"; // Major.Minor.Patch.Cards
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = base.Logger;
            Harmony harmony = new(pluginGUID);

            #region CONFIG
            ConfigHelper.Instance.BindConfig();

            #endregion

            if (ConfigHelper.Instance.ModEnabled)
            {
                #region SPECIAL ABILITIES
                // Unique special ability
                // Controls dialogue, some other stuff too
                AbilityHelper();

                SpecialAbility_Fetus();
                SpecialAbility_Bath();
                SpecialAbility_Nothing();
                SpecialAbility_Shy();
                SpecialAbility_Hate();

                if (ConfigHelper.Instance.RevealSpecials)
                {
                    // These special abilities' only exist for their Rulebook entries
                    // Their actual effects can be found in the custom ability they each possess / SpecialEvolve
                    //SpecialAbility_Greed();
                    SpecialAbility_Despair();
                    SpecialAbility_Devil();
                    SpecialAbility_Warrior();
                    SpecialAbility_Bodies();
                    SpecialAbility_CENSORED();
                    SpecialAbility_Judge();
                    SpecialAbility_Queen();
                    SpecialAbility_Army();
                }

                #endregion

                #region ABILITIES
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
                Ability_Hunter();
                Ability_Protector();

                Ability_Apostle();
                Ability_TrueSaviour();
                Ability_Confession();
                #endregion

                #region CARDS
                TestingDummy_XXXXX();

                TrainingDummy_00000();
                ScorchedGirl_F0102();
                OneSin_O0303();
                HundredsGoodDeeds_O0303();
                MagicalGirlHeart_O0104();
                QueenOfHatred_O0104();
                QueenOfHatredExhausted_O0104();
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
                //  ChildOfTheGalaxy_O0155();
                PunishingBird_O0256();
                RedHoodedMercenary_F0157();
                WillBeBadWolf_F0258();
                YoureBald_BaldIsAwesome();
                //  FragmentOfUniverse_O0360();
                CrumblingArmour_O0561();
                JudgementBird_O0262();
                //  ApocalypseBird_O0263();
                //  BigEyes_O0263();
                //  SmallBeak_O0263();
                //  LongArms_O0263();
                MagicalGirlDiamond_O0164();
                KingOfGreed_O0164();
                //  TheLittlePrince_O0466();
                //  TheLittlePrinceMinion_O0466();
                Laetitia_O0167();
                LaetitiaFriend_O0167();
                FuneralOfButterflies_T0168();
                DerFreischutz_F0169();
                //  DreamOfABlackSwan_F0270();
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
                //  GiantTreeSap_T0980();
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
                //  SkinProphecy_T0990();
                WorldPortrait_O0991();
                TodaysShyLook_O0192();
                TodaysShyLookAngry_O0192();
                TodaysShyLookHappy_O0192();
                TodaysShyLookNeutral_O0192();
                BlueStar_O0393();
                BlueStar2_O0393();
                BlueStar3_O0393();
                YouMustBeHappy_T0994();
                LuminousBracelet_O0995();
                //  BehaviourAdjustment_O0996();
                //  FaithAndPromise_T0997();
                Porccubus_O0298();
                VoidDream_T0299();
                VoidDreamRooster_T0299();
                GraveOfBlossoms_O04100();
                TheFirebird_O02101();
                //  Yin_O05102();
                //  Yang_O07103();
                //  YinYang_O07103();
                //  BackwardClock_D09104();
                //  DellaLuna_D01105();
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
                //  Ruina Expansion
                //  Wonderlab?
                #endregion

                harmony.PatchAll(typeof(GlobalPassives));
                harmony.PatchAll(typeof(ForcedEmissions));
                harmony.PatchAll(typeof(PersistentValues));
                harmony.PatchAll(typeof(DeathCardPatcher));

                RunStateHelper.Initialize(harmony);

                Logger.LogInfo($"The clock is at [{ConfigHelper.Instance.NumOfBlessings}].");
                Logger.LogInfo($"{pluginName} loaded! Time to get to work manager!");
            }
            else
            {
                Logger.LogInfo($"{pluginName} is loaded but is currently disabled in the configuration.");
            }
        }
    }
}
