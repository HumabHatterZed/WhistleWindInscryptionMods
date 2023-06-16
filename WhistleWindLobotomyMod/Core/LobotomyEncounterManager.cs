using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using static DiskCardGame.EncounterBlueprintData;
using static InscryptionAPI.Encounters.EncounterManager;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomyEncounterManager
    {
        public static readonly Dictionary<int, List<EncounterBlueprintData>> ModEncounters = new()
        {
            { 0, new() },   // region 0
            { 1, new() },   // region 1
            { 2, new() },   // region 2
            { 3, new() }    // bosses
        };

        private static EncounterBlueprintData Build(EncounterBlueprintData encounter, params int[] regions)
        {
            foreach (int region in regions)
                ModEncounters[region].Add(encounter);

            return encounter;
        }

        public static void BuildEncounters()
        {
            CardBlueprint workerBee = NewCardBlueprint("wstl_queenBeeWorker", 25);
            CardBlueprint nakedWorm = NewCardBlueprint("wstl_theNakedWorm", 25);
            CardBlueprint spiderling = NewCardBlueprint("wstl_spiderling", 25);
            CardBlueprint spiderBrood = NewCardBlueprint("wstl_spiderBrood", 25);
            CardBlueprint fairyFestival = NewCardBlueprint("wstl_fairyFestival", 10);

            #region Region 0
            Build(New("StrangePack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards("wstl_burrowingHeaven", "wstl_oldLady", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_ppodae", 10, true, 3, "wstl_ppodaeBuff")),
                    CreateTurn(NewCardBlueprint("wstl_heartOfAspiration", 25)),
                    CreateTurn()
                    ), regions: 0);
            Build(New("BitterPack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards("wstl_canOfWellCheers", "wstl_ppodae", "wstl_redShoes", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_willBeBadWolf")),
                    CreateTurn(NewCardBlueprint("wstl_redHoodedMercenary")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_yang"))
                    ), regions: 0);
            Build(New("StrangeFlock")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards("wstl_oldLady", "wstl_willBeBadWolf", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_punishingBird", 30), NewCardBlueprint("wstl_judgementBird", 30)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_theFireBird", 30))
                    ), regions: 0);
            Build(New("HelperJuggernaut")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeMechanical)
                .AddRandomReplacementCards("wstl_behaviourAdjustment", "wstl_dontTouchMe", "wstl_mhz176")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_allAroundHelper")),
                    CreateTurn(NewCardBlueprint("wstl_singingMachine", 30)),
                    CreateTurn()
                    ), regions: 0);
            Build(New("StrangeBotanicals")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_wisdomScarecrow", "wstl_porccubus", "wstl_mhz176")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_theLittlePrince"), NewCardBlueprint("wstl_graveOfBlossoms", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_snowWhitesApple")),
                    CreateTurn(workerBee, workerBee)
                    ), regions: 0);
            Build(New("FairyFlock")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards("wstl_wisdomScarecrow", "wstl_heartOfAspiration", "wstl_magicalGirlDiamond")
                .AddTurns(
                    CreateTurn(fairyFestival),
                    CreateTurn(),
                    CreateTurn(fairyFestival, fairyFestival),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival", 30)),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival", 30), NewCardBlueprint("wstl_nosferatu", 4)),
                    CreateTurn(),
                    CreateTurn()
                    ), regions: new[] { 0, 1 });
            #endregion

            #region Region 1
            Build(New("StrangeBees")
                .SetDifficulty(5, 10)
                .AddDominantTribes(Tribe.Insect)
                .AddRandomReplacementCards("wstl_meatLantern")
                .SetRedundantAbilities(QueenNest.ability)
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_queenBee"), workerBee),
                    CreateTurn(workerBee),
                    CreateTurn(workerBee),
                    CreateTurn(),
                    CreateTurn(workerBee, workerBee),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_workerBee", 8)),
                    CreateTurn()
                    ), regions: 1);
            Build(New("StrangeCreatures1")
                .SetDifficulty(5, 10)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .SetRedundantAbilities(Ability.Flying, Punisher.ability)
                .AddRandomReplacementCards("wstl_voidDream", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlHeart")),
                    CreateTurn(NewCardBlueprint("wstl_porccubus", 25), NewCardBlueprint("wstl_wallLady", 25)),
                    CreateTurn(NewCardBlueprint("wstl_meatLantern", 25)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 1);
            Build(New("WormsNest")
                .SetDifficulty(6, 10)
                .SetRedundantAbilities(SerpentsNest.ability)
                .AddDominantTribes(Tribe.Insect)
                .AddRandomReplacementCards("wstl_voidDream", "wstl_burrowingHeaven", "wstl_oldLady", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_theNakedNest"), nakedWorm),
                    CreateTurn(nakedWorm),
                    CreateTurn(nakedWorm, nakedWorm, NewCardBlueprint("wstl_theNakedWorm", 10)),
                    CreateTurn(nakedWorm, NewCardBlueprint("wstl_theNakedWorm", 9))
                ), regions: 1);
            Build(New("StrangeCreatures2")
                .SetDifficulty(6, 10)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_oldLady", "wstl_allAroundHelper", "wstl_mhz176")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlHeart")),
                    CreateTurn(NewCardBlueprint("wstl_schadenfreude", 25)),
                    CreateTurn(NewCardBlueprint("wstl_forsakenMurderer")),
                    CreateTurn(NewCardBlueprint("wstl_porccubus", 25))
                ), regions: 1);
            Build(New("StrangeCreatures3")
                .SetDifficulty(6, 10)
                .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
                .AddRandomReplacementCards("wstl_theresia", "wstl_voidDream", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_todaysShyLook", 10)),
                    CreateTurn(NewCardBlueprint("wstl_youMustBeHappy"), NewCardBlueprint("wstl_worldPortrait", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_dreamingCurrent"), NewCardBlueprint("wstl_canOfWellCheers", 25))
                ), regions: 1);
            Build(New("StrangeFish")
                .SetDifficulty(6, 14)
                .AddDominantTribes(Tribe.Bird)
                .SetRedundantAbilities(Ability.Submerge, Ability.WhackAMole, Ability.TailOnHit, Ability.Sharp, Punisher.ability)
                .AddRandomReplacementCards("wstl_voidDream", "wstl_punishingBird", "wstl_yang")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_canOfWellCheers", 25), NewCardBlueprint("wstl_magicalGirlDiamond")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_yin")),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn()
                ), regions: new[] { 1, 2 });
            Build(New("GreedJuggernaut")
                .SetDifficulty(6, 14)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards("wstl_punishingBird", "wstl_voidDream", "wstl_theFirebird", "wstl_meatLantern")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_kingOfGreed")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_bigBird", 25), NewCardBlueprint("wstl_fleshIdol", 10)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_dreamingCurrent", 25), NewCardBlueprint("wstl_yin", 25))
                ), regions: 1);
            #endregion

            #region Region 2
            Build(New("StrangeHerd")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards("wstl_burrowingHeaven", "wstl_trainingDummy", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_voidDream").SetReplacement("wstl_voidDreamRooster", 14), NewCardBlueprint("wstl_beautyAndBeast")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_allAroundHelper", 75)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_rudoltaSleigh")),
                    CreateTurn()
                ), regions: 2);
            Build(New("AlriuneJuggernaut")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards("wstl_redShoes", "wstl_oldLady", "wstl_allAroundHelper", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_alriune"), NewCardBlueprint("wstl_burrowingHeaven", 10)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_voidDream", 25).SetReplacement("wstl_voidDreamRooster", 30)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_beautyAndBeast", 25)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 2);
            Build(New("SpidersNest")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Insect)
                .SetRedundantAbilities(BroodMother.ability)
                .AddRandomReplacementCards("wstl_redShoes", "wstl_heartOfAspiration", "wstl_ppodaeBuff", "wstl_voidDream")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_spiderBud"), spiderBrood),
                    CreateTurn(spiderling.SetReplacement("wstl_spiderBrood", 14), spiderBrood),
                    CreateTurn(spiderBrood),
                    CreateTurn(NewCardBlueprint("wstl_spiderling", 30)),
                    CreateTurn(),
                    CreateTurn(spiderling.SetReplacement("wstl_spiderBrood", 14))
                ), regions: 2);
            Build(New("StrangeCreatures4")
                .SetDifficulty(11, 14)
                .SetRedundantAbilities()
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards("wstl_nosferatu", "wstl_shelterFrom27March", "wstl_redShoes", "wstl_oneSin")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_warmHeartedWoodsman", 10), NewCardBlueprint("wstl_magicalGirlSpade")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_notesFromResearcher", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_scaredyCat", 25), NewCardBlueprint("wstl_oldLady", 25)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 2);
            Build(New("StrangeMinions")
                .SetDifficulty(11, 14)
                .SetRedundantAbilities()
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_queenBeeWorker", "wstl_snowWhitesApple", "wstl_schadenfreude", "wstl_burrowingHeaven")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_parasiteTree", 25), NewCardBlueprint("wstl_queenBee", 25)),
                    CreateTurn(nakedWorm),
                    CreateTurn(workerBee, workerBee, nakedWorm),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_ozmaPumpkinJack", 50), NewCardBlueprint("wstl_ozmaPumpkinJack", 75)),
                    CreateTurn(),
                    CreateTurn(workerBee, nakedWorm)
                ), regions: 2);
            Build(New("SwanJuggernaut")
                .SetDifficulty(11, 14)
                .SetRedundantAbilities(Ability.WhackAMole, Ability.Sharp, Reflector.ability, Nettles.ability)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards("wstl_willBeBadWolf", "wstl_dreamingCurrent", "wstl_allAroundHelper")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_dreamOfABlackSwan"), NewCardBlueprint("wstl_dreamOfABlackSwanBrother3", 14)),
                    CreateTurn(NewCardBlueprint("wstl_dreamOfABlackSwanBrother5", 50), NewCardBlueprint("wstl_dreamOfABlackSwanBrother2", 50)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_bigBird", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_punishingBird", 25)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 2);
            #endregion

            #region Bosses
            ProspectorAbnormalBossP1 = Build(New("ProspectorAbnormalBossP1")
                .SetDifficulty(4, 7)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_porccubus", "wstl_graveOfBlossoms")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_RUDOLTA_MULE"), NewCardBlueprint("wstl_todaysShyLookAngry")),
                    CreateTurn(NewCardBlueprint("wstl_scorchedGirl", 20)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_snowWhitesApple", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_burrowingHeaven", 25)),
                    CreateTurn()
                ), 3);
            ProspectorAbnormalBossP2 = Build(New("ProspectorAbnormalBossP2")
               .SetDifficulty(4, 7)
               .AddDominantTribes(AbnormalPlugin.TribeBotanic)
               .AddRandomReplacementCards("wstl_ppodae", "wstl_porccubus")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_willBeBadWolf")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_redHoodedMercenary", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_warmHeartedWoodsman", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_graveOfBlossoms", 7)),
                    CreateTurn()
                ), 3);
            AnglerAbnormalBossP1 = Build(New("AnglerAbnormalBossP1")
               .SetDifficulty(9, 11)
               .AddDominantTribes(Tribe.Bird)
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_canOfWellCheers")),
                    CreateTurn(NewCardBlueprint("wstl_theFirebird")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival")),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival", 11)),
                    CreateTurn()
                ), 3);
            AnglerAbnormalBossP2 = Build(New("AnglerAbnormalBossP2")
               .SetDifficulty(9, 11)
               .AddDominantTribes(Tribe.Bird)
               .AddRandomReplacementCards("wstl_dellaLuna")
               .AddTurns(
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket", 25)),
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket"), NewCardBlueprint("wstl_punishingBird", 10)),
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket"), NewCardBlueprint("BaitBucket", 10))
                ), 3);
            TrapperTraderAbnormalBossP1 = Build(New("TrapperTraderAbnormalBossP1")
               .SetDifficulty(14, 16)
               .AddDominantTribes(AbnormalPlugin.TribeMechanical)
               .AddRandomReplacementCards("wstl_scorchedGirl", "wstl_bloodBath", "wstl_voidDreamRooster", "wstl_porccubus")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("TrapFrog")),
                    CreateTurn(NewCardBlueprint("wstl_trainingDummy", 30).SetReplacement("wstl_mhz176")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_theFirebird", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_meatLantern", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_judgementBird", 25))
                ), 3);
            LeshyAbnormalBossP1 = Build(New("LeshyAbnormalBossP1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(AbnormalPlugin.TribeFae)
               .AddRandomReplacementCards("wstl_derFreischutz", "wstl_armyInPink", "wstl_expressHellTrain")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_dreamOfABlackSwan")),
                    CreateTurn(NewCardBlueprint("wstl_funeralOfButterflies", 25)),
                    CreateTurn(NewCardBlueprint("wstl_mountainOfBodies", 25), NewCardBlueprint("wstl_silentOrchestra", 10)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_censored"))
                ), 3);
            PirateSkullAbnormalBossP1 = Build(New("PirateSkullAbnormalBossP1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(Tribe.Bird)
               .AddTurns(
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_theFirebird")),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_voidDreamRooster")),
                    CreateTurn(),
                    CreateTurn()
                ), 3);
            PirateSkullAbnormalBossP2 = Build(New("PirateSkullAbnormalBossP2")
               .SetDifficulty(20, 20)
               .AddDominantTribes(Tribe.Bird)
               .AddTurns(
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_yin"), NewCardBlueprint("wstl_yang")),
                    CreateTurn(NewCardBlueprint("wstl_punishingBird"), NewCardBlueprint("wstl_judgementBird")),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_CRUMPLED_CAN")),
                    CreateTurn(),
                    CreateTurn()
                ), 3);
            #endregion
        }

        public static EncounterBlueprintData ProspectorAbnormalBossP1 { get; private set; }
        public static EncounterBlueprintData ProspectorAbnormalBossP2 { get; private set; }
        public static EncounterBlueprintData AnglerAbnormalBossP1 { get; private set; }
        public static EncounterBlueprintData AnglerAbnormalBossP2 { get; private set; }
        public static EncounterBlueprintData TrapperTraderAbnormalBossP1 { get; private set; }
        public static EncounterBlueprintData LeshyAbnormalBossP1 { get; private set; }
        public static EncounterBlueprintData PirateSkullAbnormalBossP1 { get; private set; }
        public static EncounterBlueprintData PirateSkullAbnormalBossP2 { get; private set; }
    }
}
