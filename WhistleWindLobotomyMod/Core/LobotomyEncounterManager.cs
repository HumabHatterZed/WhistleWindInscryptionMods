using DiskCardGame;
using InscryptionAPI.Encounters;
using Sirenix.Serialization.Utilities;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using static DiskCardGame.EncounterBlueprintData;
using static InscryptionAPI.Encounters.EncounterManager;

namespace WhistleWindLobotomyMod.Core {
    /// <summary>
    /// Difficulty Ranges (no modifier)
    /// R0: (1,4) R1: (5,10) R2: (11,14) R3: (17,20)[boss region]
    /// Difficulty formula: RunState.Run.regionTier * 6 + (y + 1) / 3 - 1; where y = (0,14) and regionTier = (0,3)[3=bosss]
    /// </summary>
    public static class LobotomyEncounterManager {
        public static void BuildEncounters() {
            CardBlueprint workerBee = NewCardBlueprint("wstl_queenBeeWorker", 25);
            CardBlueprint nakedWorm = NewCardBlueprint(Cards.theNakedWorm, 25);
            CardBlueprint spiderling = NewCardBlueprint("wstl_spiderling", 25);
            CardBlueprint spiderBrood = NewCardBlueprint("wstl_spiderBrood", 25);
            CardBlueprint fairyFestival = NewCardBlueprint(Cards.fairyFestival, 10);

            #region Region 0
            Build(New("StrangePack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards(Cards.burrowingHeaven, Cards.oldLady, Cards.heartOfAspiration, Cards.youMustBeHappy)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.ppodae, 10, true, 4, Cards.ppodaeBuff), EmptyBlueprint(Cards.fairyFestival, 4)),
                    CreateTurn(NewCardBlueprint(Cards.heartOfAspiration, 25)),
                    CreateTurn()
                    ), regions: 0);
            Build(New("BitterPack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards(Cards.happyTeddyBear, Cards.ppodae, Cards.youMustBeHappy)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.willBeBadWolf)),
                    CreateTurn(EmptyBlueprint(Cards.burrowingHeaven, 4)),
                    CreateTurn(NewCardBlueprint(Cards.redHoodedMercenary, 30, true, 4, Cards.ppodaeBuff))
                    ), regions: 0);
            Build(New("StrangeFlock")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards(Cards.forestKeeper_mook, Cards.eyeballChick_mook, Cards.voidDreamRooster)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.punishingBird, 30), NewCardBlueprint(Cards.runawayBird_mook, difficultyReplace: true, difficultyReplaceReq: 3, replacement: Cards.judgementBird)),
                    CreateTurn(EmptyBlueprint(Cards.todaysShyLookHappy, 4)),
                    CreateTurn(NewCardBlueprint(Cards.theFirebird, 30))
                    ), regions: 0);
            Build(New("HelperJuggernaut")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeMechanical)
                .AddRandomReplacementCards(Cards.behaviourAdjustment, Cards.dontTouchMe, Cards.mhz176, Cards.youMustBeHappy)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.allAroundHelper), EmptyBlueprint(Cards.dontTouchMe, 4)),
                    CreateTurn(NewCardBlueprint(Cards.singingMachine, 30)),
                    CreateTurn()
                    ), regions: 0);
            Build(New("StrangeBotanicals")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards(Cards.wisdomScarecrow, Cards.porccubus, Cards.mhz176)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.theLittlePrince), NewCardBlueprint(Cards.graveOfBlossoms, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.snowWhitesApple)),
                    CreateTurn(workerBee, workerBee)
                    ), regions: 0);
            Build(New("FairyFlock")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards(Cards.fairyFestival, Cards.heartOfAspiration, Cards.magicalGirlDiamond)
                .AddTurns(
                    CreateTurn(fairyFestival),
                    CreateTurn(EmptyBlueprint(Cards.fairyFestival, 3)),
                    CreateTurn(fairyFestival, fairyFestival),
                    CreateTurn(NewCardBlueprint(Cards.fairyFestival, 30)),
                    CreateTurn(NewCardBlueprint(Cards.fairyFestival, 30), NewCardBlueprint(Cards.nosferatu, 0, true, 4, Cards.nosferatuBeast)),
                    CreateTurn(),
                    CreateTurn()
                    ), regions: new[] { 0, 1 });
            #endregion

            #region Region 1
            Build(New("StrangeBees")
                .SetDifficulty(5, 10)
                .AddDominantTribes(Tribe.Insect)
                .AddRandomReplacementCards(Cards.meatLantern, Cards.youMustBeHappy)
                .SetRedundantAbilities(QueenNest.ID)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.queenBee), workerBee),
                    CreateTurn(workerBee),
                    CreateTurn(workerBee, EmptyBlueprint(8)),
                    CreateTurn(EmptyBlueprint("wstl_queenBeeWorker", 8)),
                    CreateTurn(workerBee, workerBee),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_queenBeeWorker", 8)),
                    CreateTurn()
                    ), regions: 1);
            Build(New("StrangeCreatures1")
                .SetDifficulty(5, 10)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .SetRedundantAbilities(Ability.Flying, Punisher.ID)
                .AddRandomReplacementCards(Cards.voidDream, Cards.heartOfAspiration, Cards.fairyFestival)
                .AddTurns(
                CreateTurn(EmptyBlueprint(Cards.magicalGirlHeart, 8)),
                    CreateTurn(NewCardBlueprint(Cards.magicalGirlSpade, 0, true, 10, Cards.knightOfDespair)),
                    CreateTurn(NewCardBlueprint(Cards.porccubus, 25), NewCardBlueprint(Cards.wallLady, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.meatLantern, 25)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 1);
            Build(New("WormsNest")
                .SetDifficulty(6, 10)
                .SetRedundantAbilities(SerpentsNest.ID)
                .AddDominantTribes(Tribe.Insect)
                .AddRandomReplacementCards(Cards.voidDream, Cards.burrowingHeaven, Cards.oldLady, Cards.heartOfAspiration)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.theNakedNest), nakedWorm),
                    CreateTurn(nakedWorm, EmptyBlueprint(Cards.theNakedWorm, 8)),
                    CreateTurn(nakedWorm, nakedWorm, NewCardBlueprint(Cards.theNakedWorm, 10)),
                    CreateTurn(nakedWorm, NewCardBlueprint(Cards.theNakedWorm, 9))
                ), regions: 1);
            Build(New("StrangeCreatures2")
                .SetDifficulty(6, 10)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards(Cards.oldLady, Cards.allAroundHelper, Cards.heartOfAspiration)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.snowWhitesApple)),
                    CreateTurn(NewCardBlueprint(Cards.magicalGirlHeart, 25), EmptyBlueprint(Cards.pinocchio, 8)),
                    CreateTurn(NewCardBlueprint(Cards.porccubus)),
                    CreateTurn(NewCardBlueprint(Cards.forsakenMurderer, 25, true, 10))
                ), regions: 1);
            Build(New("StrangeCreatures3")
                .SetDifficulty(6, 10)
                .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
                .AddRandomReplacementCards(Cards.theresia, Cards.voidDream, Cards.silentEnsemble)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.scorchedGirl, 30)),
                    CreateTurn(NewCardBlueprint(Cards.todaysShyLook, 10), EmptyBlueprint(5)),
                    CreateTurn(NewCardBlueprint(Cards.youMustBeHappy), NewCardBlueprint(Cards.worldPortrait, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.dreamingCurrent), NewCardBlueprint(Cards.forsakenMurderer, 25))
                ), regions: 1);
            Build(New("StrangeFish")
                .SetDifficulty(6, 14)
                .AddDominantTribes(Tribe.Bird)
                .SetRedundantAbilities(Ability.Submerge, Ability.WhackAMole, Ability.TailOnHit, Ability.Sharp, Punisher.ID)
                .AddRandomReplacementCards(Cards.voidDream, Cards.punishingBird, Cards.runawayBird_mook)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.canOfWellCheers, 25), NewCardBlueprint(Cards.magicalGirlDiamond)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.yin), EmptyBlueprint(Cards.yang, 14)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(Cards.theFirebird, 14)),
                    CreateTurn()
                ), regions: new[] { 1, 2 });
            Build(New("GreedJuggernaut")
                .SetDifficulty(6, 14)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards(Cards.punishingBird, Cards.voidDream, Cards.fairyFestival, Cards.magicalGirlClover)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.kingOfGreed)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.nosferatu, 25, true, 14, Cards.nosferatuBeast), NewCardBlueprint(Cards.fleshIdol, 10)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(10)),
                    CreateTurn(NewCardBlueprint(Cards.dreamingCurrent, 25), NewCardBlueprint(Cards.yin, 25))
                ), regions: 1);
            #endregion

            #region Region 2
            Build(New("StrangeHerd")
                .SetDifficulty(11, 16)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards(Cards.burrowingHeaven, Cards.trainingDummy, Cards.silentEnsemble)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.voidDream).SetReplacement(Cards.voidDreamRooster, 14), NewCardBlueprint(Cards.beautyAndBeast)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.allAroundHelper, 45), EmptyBlueprint(null, 12)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.rudoltaSleigh)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(Cards.alriune, 16))
                ), regions: 2);
            Build(New("AlriuneJuggernaut")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards(Cards.redShoes, Cards.oldLady, Cards.allAroundHelper, Cards.fleshIdol)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.alriune), NewCardBlueprint(Cards.burrowingHeaven, 10)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(Cards.voidDream, 14)),
                    CreateTurn(NewCardBlueprint(Cards.voidDream, 25).SetReplacement(Cards.dontTouchMe, 14)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.beautyAndBeast, 25)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 2);
            Build(New("SpidersNest")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Insect)
                .SetRedundantAbilities(BroodMother.ID)
                .AddRandomReplacementCards(Cards.redShoes, Cards.fleshIdol, Cards.ppodaeBuff, Cards.voidDream)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.spiderBud), spiderBrood),
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
                .AddRandomReplacementCards(Cards.nosferatu, Cards.shelterFrom27March, Cards.redShoes, Cards.oneSin)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.mirrorOfAdjustment, 10).SetReplacement(Cards.warmHeartedWoodsman, 11), NewCardBlueprint(Cards.fairyFestival)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.magicalGirlClover), NewCardBlueprint(Cards.notesFromResearcher, 25)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.scaredyCat, 25), EmptyBlueprint(25)),
                    CreateTurn()
                ), regions: 2);
            Build(New("StrangeMinions")
                .SetDifficulty(11, 14)
                .SetRedundantAbilities()
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_queenBeeWorker", Cards.snowWhitesApple, Cards.schadenfreude, Cards.burrowingHeaven)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.theLittlePrince, 25), NewCardBlueprint(Cards.queenBee, 25)),
                    CreateTurn(nakedWorm),
                    CreateTurn(workerBee, workerBee, nakedWorm),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_ozmaPumpkinJack", 50), NewCardBlueprint("wstl_ozmaPumpkinJack", 75)),
                    CreateTurn(),
                    CreateTurn(workerBee, nakedWorm)
                ), regions: 2);
            Build(New("SwanJuggernaut")
                .SetDifficulty(11, 14)
                .SetRedundantAbilities(Ability.WhackAMole, Ability.Sharp, Reflector.ID, Nettles.ID)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards(Cards.forestKeeper_mook, Cards.runawayBird_mook, Cards.allAroundHelper)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.dreamOfABlackSwan), NewCardBlueprint("wstl_dreamOfABlackSwanBrother3", 14)),
                    CreateTurn(NewCardBlueprint("wstl_dreamOfABlackSwanBrother5", 50), NewCardBlueprint("wstl_dreamOfABlackSwanBrother2", 50)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.bigBird, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.punishingBird, 25)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: 2);
            #endregion

            #region Region All
            Build(New("StrangeAssortmentAnthropoids")
                .SetDifficulty(1, 20)
                .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
                .AddRandomReplacementCards(Cards.wallLady, Cards.oneSin, Cards.wisdomScarecrow, Cards.forsakenMurderer)
                .SetRedundantAbilities(Ability.Evolve, Ability.ExplodeOnDeath)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.honouredMonk)),
                    CreateTurn(NewCardBlueprint(Cards.oldLady, 40, true, 11, Cards.heartOfAspiration)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.scorchedGirl, 15), EmptyBlueprint(Cards.silentEnsemble, 7)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.wallLady, 40, true, 14, Cards.redHoodedMercenary)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(15))
                    ), 0, 1, 2);
            Build(New("StrangeAssortmentFae")
                .SetDifficulty(1, 20)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards(Cards.fairyFestival, Cards.wisdomScarecrow)
                .SetRedundantAbilities(Bloodfiend.ID, OneSided.ID)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.nosferatu, 10, true, 15, Cards.nosferatuBeast)),
                    CreateTurn(NewCardBlueprint(Cards.magicalGirlDiamond, 40, true, 11, null)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.magicalGirlHeart, 15), EmptyBlueprint(Cards.theRoadHome, 7)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.laetitia, 40, true, 16, Cards.knightOfDespair)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(14))
                    ), 0, 1, 2);
            Build(New("StrangeAssortmentMechanical")
                .SetDifficulty(1, 20)
                .AddDominantTribes(AbnormalPlugin.TribeMechanical)
                .AddRandomReplacementCards(Cards.schadenfreude, Cards.doubtA, Cards.doubtB)
                .SetRedundantAbilities(Ability.Sentry, Ability.Strafe, Punisher.ID, Woodcutter.ID)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.schadenfreude, 10, true, 14, Cards.warmHeartedWoodsman)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.theresia, 40, true, 11, Cards.luminousBracelet)),
                    CreateTurn(NewCardBlueprint(Cards.allAroundHelper, 15), EmptyBlueprint(Cards.luminousBracelet, 7)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.trainingDummy, 40, true, 12, Cards.singingMachine)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(14))
                    ), 0, 1, 2);
            #endregion

            #region Bosses
            ProspectorAbnormalBossP1 = Build(New("ProspectorAbnormalBossP1")
                .SetDifficulty(4, 7)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards(Cards.porccubus, Cards.fragmentOfUniverse, Cards.ppodae)
                .SetRedundantAbilities(Bloodfiend.ID, Roots.ID, Ability.Sharp, Ability.Deathtouch)
                .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.rudoltaSleigh_mule), NewCardBlueprint(Cards.wisdomScarecrow)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.scorchedGirl)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.snowWhitesApple)),
                    CreateTurn(EmptyBlueprint(Cards.canOfWellCheers, 6)),
                    CreateTurn(NewCardBlueprint(Cards.graveOfBlossoms, 25)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(Cards.todaysShyLook, 6))
                ), 3);
            ProspectorAbnormalBossP2 = Build(New("ProspectorAbnormalBossP2")
               .SetDifficulty(4, 7)
               .AddDominantTribes(AbnormalPlugin.TribeBotanic)
               .AddRandomReplacementCards(Cards.beautyAndBeast, Cards.porccubus, Cards.wisdomScarecrow, Cards.fragmentOfUniverse)
               .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.ppodaeBuff)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.redHoodedMercenary, 40)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.warmHeartedWoodsman, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.graveOfBlossoms, 7), EmptyBlueprint(Cards.alriune, 6)),
                    CreateTurn()
                ), 3);
            AnglerAbnormalBossP1 = Build(New("AnglerAbnormalBossP1")
               .SetDifficulty(9, 11)
               .AddDominantTribes(AbnormalPlugin.TribeFae)
               .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.canOfWellCheers)),
                    CreateTurn(NewCardBlueprint(Cards.theFirebird)),
                    CreateTurn(EmptyBlueprint(Cards.bottleOfTears, 10)),
                    CreateTurn(NewCardBlueprint(Cards.fairyFestival)),
                    CreateTurn(NewCardBlueprint(Cards.fairyFestival).SetReplacement(Cards.knightOfDespair, 11)),
                    CreateTurn()
                ), 3);
            AnglerAbnormalBossP2 = Build(New("AnglerAbnormalBossP2")
               .SetDifficulty(9, 11)
               .AddDominantTribes(AbnormalPlugin.TribeFae)
               .AddRandomReplacementCards(Cards.magicalGirlClover)
               .AddTurns(
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket", 25)),
                   CreateTurn(NewCardBlueprint(Cards.bottleOfTears)),
                   CreateTurn(NewCardBlueprint("BaitBucket"), NewCardBlueprint(Cards.theRoadHome, 10)),
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket"), NewCardBlueprint("BaitBucket", 10))
                ), 3);
            TrapperTraderAbnormalBossP1 = Build(New("TrapperTraderAbnormalBossP1")
               .SetDifficulty(14, 16)
               .AddDominantTribes(AbnormalPlugin.TribeMechanical)
               .AddRandomReplacementCards(Cards.mySweetHomeM, Cards.doubtB, Cards.mhz176, Cards.porccubus)
               .AddTurns(
                    CreateTurn(NewCardBlueprint("TrapFrog")),
                    CreateTurn(NewCardBlueprint(Cards.trainingDummy, 30).SetReplacement(Cards.dontTouchMe, 15)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.allAroundHelper, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.schadenfreude, 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.judgementBird, 25))
                ), 3);
            LeshyAbnormalBossP1 = Build(New("LeshyAbnormalBossP1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
               .AddRandomReplacementCards(Cards.blueStar, Cards.armyInPink, Cards.silentEnsemble)
               .AddTurns(
                    CreateTurn(NewCardBlueprint(Cards.silentOrchestra)),
                    CreateTurn(NewCardBlueprint(Cards.censored, 25), NewCardBlueprint(Cards.mountainOfBodies)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.dreamOfABlackSwan, 15)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.derFreischutz, 25))
                ), 3);
            PirateSkullAbnormalBossP1 = Build(New("PirateSkullAbnormalBossP1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(Tribe.Bird)
               .AddTurns(
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.skeletonShrimp), NewCardBlueprint(Cards.theFirebird)),
                    CreateTurn(NewCardBlueprint(Cards.skeletonShrimp)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.skeletonShrimp), NewCardBlueprint(Cards.voidDreamRooster)),
                    CreateTurn(),
                    CreateTurn()
                ), 3);
            PirateSkullAbnormalBossP2 = Build(New("PirateSkullAbnormalBossP2")
               .SetDifficulty(20, 20)
               .AddDominantTribes(Tribe.Bird)
               .AddTurns(
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.yin), NewCardBlueprint(Cards.yang)),
                    CreateTurn(NewCardBlueprint(Cards.punishingBird), NewCardBlueprint(Cards.judgementBird)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint(Cards.skeletonShrimp), NewCardBlueprint(Cards.skeletonShrimp), NewCardBlueprint(Cards.crumpledCan)),
                    CreateTurn(),
                    CreateTurn()
                ), 3);

            #endregion

            #region Rapture
            //Build(New("RaptureEncounter1")
            //   .SetDifficulty(20, 20)
            //   .AddDominantTribes(AbnormalPlugin.TribeDivine)
            //   .AddTurns(
            //        CreateTurn()
            //   ), 3);
            #endregion
        }

        private static CardBlueprint EmptyBlueprint(int replacementChance) => NewCardBlueprint(null, replacementChance);
        private static CardBlueprint EmptyBlueprint(string replacement, int difficultyReplace) => NewCardBlueprint(null, 0, true, difficultyReplace, replacement);
        private static EncounterBlueprintData Build(EncounterBlueprintData encounter, params int[] regions) {
            regions.ForEach(x => ModEncounters[x].Add(encounter));
            return encounter;
        }
        public static readonly Dictionary<int, List<EncounterBlueprintData>> ModEncounters = new()
        {
            { 0, new() },   // region 0
            { 1, new() },   // region 1
            { 2, new() },   // region 2
            { 3, new() }    // bosses
        };
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
