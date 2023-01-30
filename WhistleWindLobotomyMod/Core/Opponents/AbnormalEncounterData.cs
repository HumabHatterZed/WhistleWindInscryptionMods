using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using static WhistleWindLobotomyMod.Core.Helpers.EncounterHelper;

namespace WhistleWindLobotomyMod.Core.Opponents
{
    public class AbnormalEncounterData
    {
        public static List<EncounterBlueprintData> ModEncounters = new()
        {
            StrangePack, BitterPack, StrangeFlock, HelperJuggernaut,
            StrangeBees, StrangeCreatures1, WormsNest, StrangeCreatures2, StrangeFish,
            StrangeHerd, AlriuneJuggernaut, SpidersNest, SwanJuggernaut
        };
        public static EncounterBlueprintData ModDebugEncounter
        {
            get
            {
                string name = "DebugEncounter";
                List<Tribe> tribes = new() { Tribe.Canine };
                List<CardInfo> replacements = new();
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("Mule") },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 0, 20, replacements, redundant, turns);
            }
        }
        // Forest encounters
        public static EncounterBlueprintData StrangePack
        {
            get
            {
                string name = "StrangePack";
                List<Tribe> tribes = new() { Tribe.Canine };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_burrowingHeaven"),
                    CardLoader.GetCardByName("wstl_oldLady"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_ppodae") },
                    new() { CreateCardBlueprint("wstl_heartOfAspiration") },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 1, 4, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData BitterPack
        {
            get
            {
                string name = "BitterPack";
                List<Tribe> tribes = new() { Tribe.Canine };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_canOfWellCheers"),
                    CardLoader.GetCardByName("wstl_ppodae"),
                    CardLoader.GetCardByName("wstl_redShoes"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_willBeBadWolf") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_redHoodedMercenary", 40) }
                };
                return BuildBlueprint(name, tribes, 1, 4, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData StrangeFlock
        {
            get
            {
                string name = "StrangeFlock";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_oldLady"),
                    CardLoader.GetCardByName("wstl_willBeBadWolf"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_punishingBird", 30), CreateCardBlueprint("wstl_judgementBird", 30) },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 1, 4, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData HelperJuggernaut
        {
            get
            {
                string name = "HelperJuggernaut";
                List<Tribe> tribes = new() { Tribe.Hooved };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_behaviourAdjustment"),
                    CardLoader.GetCardByName("wstl_dontTouchMe"),
                    CardLoader.GetCardByName("wstl_mhz176")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_allAroundHelper") },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 1, 4, replacements, redundant, turns);
            }
        }

        // Swamp encounters
        public static EncounterBlueprintData StrangeBees
        {
            get
            {
                string name = "StrangeBees";
                List<Tribe> tribes = new() { Tribe.Insect };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_meatLantern")
                };
                List<Ability> redundant = new() { QueenNest.ability };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_queenBee", 0), CreateCardBlueprint("wstl_queenBeeWorker") },
                    new() { CreateCardBlueprint("wstl_queenBeeWorker") },
                    new() { CreateCardBlueprint("wstl_queenBeeWorker") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_queenBeeWorker"), CreateCardBlueprint("wstl_queenBeeWorker") },
                    new() { },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 5, 10, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData StrangeCreatures1
        {
            get
            {
                string name = "StrangeCreatures1";
                List<Tribe> tribes = new() { Tribe.Reptile };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_voidDream"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = new() { Ability.Flying };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_magicalGirlHeart", 0) },
                    new() { CreateCardBlueprint("wstl_porccubus"), CreateCardBlueprint("wstl_wallLady") },
                    new() { CreateCardBlueprint("wstl_meatLantern") },
                    new() { },
                    new() { },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 5, 10, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData WormsNest
        {
            get
            {
                string name = "WormsNest";
                List<Tribe> tribes = new() { Tribe.Insect };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_voidDream"),
                    CardLoader.GetCardByName("wstl_burrowingHeaven"),
                    CardLoader.GetCardByName("wstl_oldLady"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = new() { SerpentsNest.ability };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_theNakedNest", 0), CreateCardBlueprint("wstl_theNakedWorm") },
                    new() { CreateCardBlueprint("wstl_theNakedWorm") },
                    new() { CreateCardBlueprint("wstl_theNakedWorm"), CreateCardBlueprint("wstl_theNakedWorm") },
                    new() { CreateCardBlueprint("wstl_theNakedWorm") }
                };
                return BuildBlueprint(name, tribes, 6, 10, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData StrangeCreatures2
        {
            get
            {
                string name = "StrangeCreatures2";
                List<Tribe> tribes = new() { Tribe.Reptile };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_oldLady"),
                    CardLoader.GetCardByName("wstl_allAroundHelper"),
                    CardLoader.GetCardByName("wstl_mhz176")
                };
                List<Ability> redundant = new() { Ability.Flying };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_magicalGirlHeart", 0) },
                    new() { CreateCardBlueprint("wstl_schadenfreude") },
                    new() { CreateCardBlueprint("wstl_forsakenMurderer") },
                    new() { CreateCardBlueprint("wstl_porccubus") }
                };
                return BuildBlueprint(name, tribes, 6, 9, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData StrangeFish
        {
            get
            {
                string name = "StrangeFish";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_voidDream"),
                    CardLoader.GetCardByName("wstl_punishingBird"),
                    CardLoader.GetCardByName("wstl_yang")
                };
                List<Ability> redundant = new() { Ability.Submerge, Ability.WhackAMole, Ability.TailOnHit, Ability.Sharp, Punisher.ability };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_canOfWellCheers") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_yin", 0) },
                    new() { },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 6, 14, replacements, redundant, turns);
            }
        }

        // Alpine encounters
        public static EncounterBlueprintData StrangeHerd
        {
            get
            {
                string name = "StrangeHerd";
                List<Tribe> tribes = new() { Tribe.Hooved };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_burrowingHeaven"),
                    CardLoader.GetCardByName("wstl_trainingDummy"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_voidDream"), CreateCardBlueprint("wstl_beautyAndBeast") },
                    new() { },
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_rudoltaSleigh") },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 11, 14, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData AlriuneJuggernaut
        {
            get
            {
                string name = "AlriuneJuggernaut";
                List<Tribe> tribes = new() { Tribe.Hooved };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_redShoes"),
                    CardLoader.GetCardByName("wstl_oldLady"),
                    CardLoader.GetCardByName("wstl_allAroundHelper"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_alriune", 0), CreateCardBlueprint("wstl_burrowingHeaven") },
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_voidDream") },
                    new() { },
                    new() { },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 11, 14, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData SpidersNest
        {
            get
            {
                string name = "SpidersNest";
                List<Tribe> tribes = new() { Tribe.Insect };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_redShoes"),
                    CardLoader.GetCardByName("wstl_heartOfAspiration"),
                    CardLoader.GetCardByName("wstl_ppodae"),
                    CardLoader.GetCardByName("wstl_voidDream")
                };
                List<Ability> redundant = new() { BroodMother.ability };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_spiderBud", 0), CreateCardBlueprint("wstl_spiderBrood") },
                    new() { CreateCardBlueprint("wstl_spiderling"), CreateCardBlueprint("wstl_spiderBrood") },
                    new() { CreateCardBlueprint("wstl_spiderBrood") },
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_spiderling") }
                };
                return BuildBlueprint(name, tribes, 11, 14, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData SwanJuggernaut
        {
            get
            {
                string name = "SwanJuggernaut";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_willBeBadWolf"),
                    CardLoader.GetCardByName("wstl_dreamingCurrent"),
                    CardLoader.GetCardByName("wstl_allAroundHelper")
                };
                List<Ability> redundant = new() { Ability.WhackAMole, Ability.Sharp, Reflector.ability, Nettles.ability };
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_dreamOfABlackSwan", 0) },
                    new() { CreateCardBlueprint("wstl_dreamOfABlackSwanBrother5", 50), CreateCardBlueprint("wstl_dreamOfABlackSwanBrother2", 50) },
                    new() { },
                    new() { CreateCardBlueprint("wstl_bigBird") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_punishingBird") },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 11, 14, replacements, redundant, turns);
            }
        }

        // Boss encounters
        public static EncounterBlueprintData ProspectorAbnormalBossP1
        {
            get
            {
                string name = "ProspectorAbnormalBossP1";
                List<Tribe> tribes = new() { Tribe.Canine };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_porccubus"),
                    CardLoader.GetCardByName("wstl_burrowingHeaven")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_RUDOLTA_MULE", 0), CreateCardBlueprint("wstl_todaysShyLookAngry", 0) },
                    new() { CreateCardBlueprint("wstl_scorchedGirl") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_snowWhitesApple") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_burrowingHeaven") },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 4, 7, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData ProspectorAbnormalBossP2
        {
            get
            {
                string name = "ProspectorAbnormalBossP2";
                List<Tribe> tribes = new() { Tribe.Canine };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_ppodae"),
                    CardLoader.GetCardByName("wstl_porccubus")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_willBeBadWolf", 0) },
                    new() { },
                    new() { CreateCardBlueprint("wstl_redHoodedMercenary") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_warmHeartedWoodsman") },
                    new() { },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 4, 7, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData AnglerAbnormalBossP1
        {
            get
            {
                string name = "AnglerAbnormalBossP1";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new();
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_canOfWellCheers") },
                    new() { CreateCardBlueprint("wstl_theFirebird") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_fairyFestival") },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 9, 11, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData AnglerAbnormalBossP2
        {
            get
            {
                string name = "AnglerAbnormalBossP2";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_dellaLuna")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { },
                    new() { CreateCardBlueprint("BaitBucket") },
                    new() { },
                    new() { CreateCardBlueprint("BaitBucket") },
                    new() { },
                    new() { CreateCardBlueprint("BaitBucket") }
                };
                return BuildBlueprint(name, tribes, 9, 11, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData TrapperTraderAbnormalBossP1
        {
            get
            {
                string name = "TrapperTraderAbnormalBossP1";
                List<Tribe> tribes = new() { Tribe.Reptile };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_scorchedGirl"),
                    CardLoader.GetCardByName("wstl_bloodBath"),
                    CardLoader.GetCardByName("wstl_voidDreamRooster"),
                    CardLoader.GetCardByName("wstl_porccubus")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("TrapFrog") },
                    new() { CreateCardBlueprint("wstl_trainingDummy", 30) },
                    new() { },
                    new() { CreateCardBlueprint("wstl_theFirebird") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_meatLantern") },
                    new() { },
                    new() { CreateCardBlueprint("wstl_judgementBird") }
                };
                return BuildBlueprint(name, tribes, 14, 16, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData LeshyAbnormalBossP1
        {
            get
            {
                string name = "LeshyAbnormalBossP1";
                List<Tribe> tribes = new() { Tribe.Canine };
                List<CardInfo> replacements = new()
                {
                    CardLoader.GetCardByName("wstl_derFreischutz"),
                    CardLoader.GetCardByName("wstl_armyInBlack")
                };
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { CreateCardBlueprint("wstl_magicalGirlSpade") },
                    new() { CreateCardBlueprint("wstl_funeralOfButterflies") },
                    new() { CreateCardBlueprint("wstl_mountainOfBodies"), CreateCardBlueprint("wstl_silentOrchestra") },
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_censored", 0) }
                };
                return BuildBlueprint(name, tribes, 20, 20, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData PirateSkullAbnormalBossP1
        {
            get
            {
                string name = "PirateSkullAbnormalBossP1";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new();
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_SKELETON_SHRIMP", 0), CreateCardBlueprint("wstl_theFirebird", 0) },
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_SKELETON_SHRIMP", 0), CreateCardBlueprint("wstl_voidDreamRooster", 0) },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 20, 20, replacements, redundant, turns);
            }
        }
        public static EncounterBlueprintData PirateSkullAbnormalBossP2
        {
            get
            {
                string name = "PirateSkullAbnormalBossP2";
                List<Tribe> tribes = new() { Tribe.Bird };
                List<CardInfo> replacements = new();
                List<Ability> redundant = null;
                List<List<EncounterBlueprintData.CardBlueprint>> turns = new()
                {
                    new() { },
                    new() { CreateCardBlueprint("wstl_yin", 0), CreateCardBlueprint("wstl_yang", 0) },
                    new() { CreateCardBlueprint("wstl_punishingBird", 0), CreateCardBlueprint("wstl_judgementBird", 0) },
                    new() { },
                    new() { },
                    new() { CreateCardBlueprint("wstl_SKELETON_SHRIMP", 0), CreateCardBlueprint("wstl_SKELETON_SHRIMP", 0) },
                    new() { },
                    new() { }
                };
                return BuildBlueprint(name, tribes, 20, 20, replacements, redundant, turns);
            }
        }
    }
}
