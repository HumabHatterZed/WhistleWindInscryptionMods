using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Regions;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        internal static void Add() {
            //AccessTools.GetDeclaredMethods(typeof(Cards)).ForEach(mi => mi.Invoke(this, null));
            UtilityCards();
            RANDOM_PLACEHOLDER();

            AllAroundHelper_T0541();
            Alriune_T0453();
            ApocalypseBird_O0263();
            ApocalypseBirdEggs();
            ApocalypseBirdMinions();
            ApostleGuardian_T0346();
            ApostleHeretic_T0346();
            ApostleMoleman_T0346();
            ApostleScythe_T0346();
            ApostleSpear_T0346();
            ApostleStaff_T0346();
            ArmyInBlack_D01106();
            ArmyInPink_D01106();
            BackwardClock_D09104();
            BeautyAndBeast_O0244();
            BehaviourAdjustment_O0996();
            BigBird_O0240();
            Bloodbath_T0551();
            BlueStar_O0393();
            BurrowingHeaven_O0472();
            CanOfWellCheers_F0552();
            CENSORED_O0389();
            ChildOfTheGalaxy_O0155();
            CrumblingArmour_O0561();
            DellaLuna_D01105();
            DerFreischutz_F0169();
            DimensionalRefraction_O0388();
            DontTouchMe_O0547();
            DreamingCurrent_T0271();
            DreamOfABlackSwan_F0270();
            ExpressHellTrain_T0986();
            FairyFestival_F0483();
            FleshIdol_T0979();
            ForsakenMurderer_T0154();
            FragmentOfUniverse_O0360();
            FuneralOfButterflies_T0168();
            GiantTreeSap_T0980();
            GraveOfBlossoms_O04100();
            HappyTeddyBear_T0406();
            HeartOfAspiration_O0977();
            HonouredMonk_D01110();
            HundredsGoodDeeds_O0303();
            JudgementBird_O0262();
            Laetitia_O0167();
            LuminousBracelet_O0995();
            MagicalGirlDiamond_O0164();
            MagicalGirlHeart_O0104();
            MagicalGirlSpade_O0173();
            MeatLantern_O0484();
            MeltingLove_D03109();
            MHz176_T0727();
            MirrorOfAdjustment_O0981();
            MountainOfBodies_T0175();
            NamelessFetus_O0115();
            NotesFromResearcher_T0978();
            NothingThere_O0620();
            OldFaithAndPromise_T0997();
            OldLady_O0112();
            OneSin_O0303();
            ParasiteTree_D04108();
            PlagueDoctor_O0145();
            Porccubus_O0298();
            Ppodae_D02107();
            PunishingBird_O0256();
            QueenBee_T0450();
            RedHoodedMercenary_F0157();
            RedShoes_O0408();
            Rudolta_F0249();
            Schadenfreude_O0576();
            ScorchedGirl_F0102();
            ShelterFrom27March_T0982();
            SilentOrchestra_T0131();
            SingingMachine_O0530();
            SkinProphecy_T0990();
            SnowQueen_F0137();
            SnowWhitesApple_F0442();
            SpiderBud_O0243();
            TheFirebird_O02101();
            TheLittlePrince_O0466();
            TheNakedNest_O0274();
            TheNakedWorm_O0274();
            Theresia_T0909();
            TodaysShyLook_O0192();
            TrainingDummy_00000();
            VoidDream_T0299();
            WallLady_F0118();
            WarmHeartedWoodsman_F0532();
            WeCanChangeAnything_T0985();
            WhiteNight_T0346();
            WillBeBadWolf_F0258();
            WisdomScarecrow_F0187();
            WorldPortrait_O0991();
            Yang_O07103();
            Yin_O05102();
            YinYangDragon_O07103();
            YouMustBeHappy_T0994();
            YoureBald_BaldIsAwesome();

            JesterOfNihil_O01118();
            AdultWhoTellsLies_F01117();
            MagicalGirlClover_O01111();
            Nosferatu_F01113();
            Ozma_F04116();
            Pinocchio_F01112();
            PriceOfSilence_O0565();
            ScaredyCat_F02115();
            SilentGirl_O010();
            TheRoadHome_F01114();

            Angela();
            Binah();
            Chesed();
            Gebura();
            Hod();
            Hokma();
            Malkuth();
            Netzach();
            TipherethA();
            TipherethB();
            Yesod();
            //Sinclair(); // heehee

            Cards_AmberOrdeal(); // art
            Cards_CrimsonOrdeal();
            Cards_GreenOrdeal();
            Cards_IndigoOrdeal();
            Cards_VioletOrdeal();
            Cards_WhiteOrdeal(); // art

            BlueSmockedShepherd(); // art
            BottleOfTears();
            DingleDangle();
            DrownedSisters();
            HookahCaterpillar();
            MySweetHome(); // reptile art
            Nobodyis(); // art
            PenitentGirl();
            PiscineMermaid(); // art
            Pygmalion();
            ReddenedBuddy();
            RedQueen();
            StainingRose();
            Tangle();
            Titania(); // art
            WhiteLake();
        }
    }
}