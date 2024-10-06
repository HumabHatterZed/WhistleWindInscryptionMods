using DiskCardGame;
using InscryptionAPI.Encounters;
using Sirenix.Serialization.Utilities;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using static DiskCardGame.EncounterBlueprintData;
using static InscryptionAPI.Encounters.EncounterManager;

namespace WhistleWindLobotomyMod.Core
{
    /// <summary>
    /// Difficulty Ranges (no modifier)
    /// R0: (1,4) R1: (5,10) R2: (11,14) R3: (17,20)[boss region]
    /// Difficulty formula: RunState.Run.regionTier * 6 + (y + 1) / 3 - 1; where y = (0,14) and regionTier = (0,3)[3=bosss]
    /// </summary>
    public static class LobotomyEncounterManager
    {
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
                .AddRandomReplacementCards("wstl_burrowingHeaven", "wstl_oldLady", "wstl_heartOfAspiration", "wstl_youMustBeHappy")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_ppodae", 10, true, 4, "wstl_ppodaeBuff"), EmptyBlueprint("wstl_fairyFestival", 4)),
                    CreateTurn(NewCardBlueprint("wstl_heartOfAspiration", 25)),
                    CreateTurn()
                    ), regions: 0);
            Build(New("BitterPack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards("wstl_happyTeddyBear", "wstl_ppodae", "wstl_youMustBeHappy")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_willBeBadWolf")),
                    CreateTurn(EmptyBlueprint("wstl_burrowingHeaven", 4)),
                    CreateTurn(NewCardBlueprint("wstl_redHoodedMercenary", 30, true, 4, "wstl_ppodaeBuff"))
                    ), regions: 0);
            Build(New("StrangeFlock")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards("wstl_oldLady", "wstl_eyeballChick", "wstl_voidDreamRooster")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_punishingBird", 30), NewCardBlueprint("wstl_judgementBird", 30)),
                    CreateTurn(EmptyBlueprint("wstl_todaysShyLookHappy", 4)),
                    CreateTurn(NewCardBlueprint("wstl_theFireBird", 30))
                    ), regions: 0);
            Build(New("HelperJuggernaut")
                .SetDifficulty(1, 4)
                .AddDominantTribes(AbnormalPlugin.TribeMechanical)
                .AddRandomReplacementCards("wstl_behaviourAdjustment", "wstl_dontTouchMe", "wstl_mhz176", "wstl_youMustBeHappy")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_allAroundHelper"), EmptyBlueprint("wstl_dontTouchMe", 4)),
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
                .AddRandomReplacementCards("wstl_theRoadHome", "wstl_heartOfAspiration", "wstl_magicalGirlDiamond")
                .AddTurns(
                    CreateTurn(fairyFestival),
                    CreateTurn(EmptyBlueprint("wstl_fairyFestival", 3)),
                    CreateTurn(fairyFestival, fairyFestival),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival", 30)),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival", 30), NewCardBlueprint("wstl_nosferatu", 0, true, 4, "wstl_nosferatuBeast")),
                    CreateTurn(),
                    CreateTurn()
                    ), regions: new[] { 0, 1 });
            #endregion

            #region Region 1
            Build(New("StrangeBees")
                .SetDifficulty(5, 10)
                .AddDominantTribes(Tribe.Insect)
                .AddRandomReplacementCards("wstl_meatLantern", "wstl_youMustBeHappy")
                .SetRedundantAbilities(QueenNest.ability)
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_queenBee"), workerBee),
                    CreateTurn(workerBee),
                    CreateTurn(workerBee, EmptyBlueprint(8)),
                    CreateTurn(EmptyBlueprint("wstl_workerBee", 8)),
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
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlSpade", 0, true, 10, "wstl_knightOfDespair")),
                    CreateTurn(NewCardBlueprint("wstl_porccubus", 25), NewCardBlueprint("wstl_wallLady", 25)),
                    CreateTurn(NewCardBlueprint("wstl_meatLantern", 25)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint("wstl_magicGirlHeart", 8)),
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
                    CreateTurn(nakedWorm, EmptyBlueprint("wstl_theNakedWorm", 8)),
                    CreateTurn(nakedWorm, nakedWorm, NewCardBlueprint("wstl_theNakedWorm", 10)),
                    CreateTurn(nakedWorm, NewCardBlueprint("wstl_theNakedWorm", 9))
                ), regions: 1);
            Build(New("StrangeCreatures2")
                .SetDifficulty(6, 10)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_oldLady", "wstl_allAroundHelper", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_snowWhitesApple")),
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlHeart", 25), EmptyBlueprint("wstl_pinocchio", 8)),
                    CreateTurn(NewCardBlueprint("wstl_porccubus")),
                    CreateTurn(NewCardBlueprint("wstl_forsakenMurderer", 25, true, 10))
                ), regions: 1);
            Build(New("StrangeCreatures3")
                .SetDifficulty(6, 10)
                .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
                .AddRandomReplacementCards("wstl_theresia", "wstl_voidDream", "wstl_silentEnsemble")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_scorchedGirl", 30)),
                    CreateTurn(NewCardBlueprint("wstl_todaysShyLook", 10), EmptyBlueprint(5)),
                    CreateTurn(NewCardBlueprint("wstl_youMustBeHappy"), NewCardBlueprint("wstl_worldPortrait", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_dreamingCurrent"), NewCardBlueprint("wstl_forsakenMurderer", 25))
                ), regions: 1);
            Build(New("StrangeFish")
                .SetDifficulty(6, 14)
                .AddDominantTribes(Tribe.Bird)
                .SetRedundantAbilities(Ability.Submerge, Ability.WhackAMole, Ability.TailOnHit, Ability.Sharp, Punisher.ability)
                .AddRandomReplacementCards("wstl_voidDream", "wstl_punishingBird", "wstl_bigBird")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_canOfWellCheers", 25), NewCardBlueprint("wstl_magicalGirlDiamond")),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint("wstl_theFirebird", 14)),
                    CreateTurn(NewCardBlueprint("wstl_yin"), EmptyBlueprint("wstl_yang", 14)),
                    CreateTurn(),
                    CreateTurn()
                ), regions: new[] { 1, 2 });
            Build(New("GreedJuggernaut")
                .SetDifficulty(6, 14)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards("wstl_punishingBird", "wstl_voidDream", "wstl_fairyFestival", "wstl_magicalGirlClover")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_kingOfGreed")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_nosferatu", 25, true, 14, "wstl_nosferatuBeast"), NewCardBlueprint("wstl_fleshIdol", 10)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(10)),
                    CreateTurn(NewCardBlueprint("wstl_dreamingCurrent", 25), NewCardBlueprint("wstl_yin", 25))
                ), regions: 1);
            #endregion

            #region Region 2
            Build(New("StrangeHerd")
                .SetDifficulty(11, 16)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards("wstl_burrowingHeaven", "wstl_trainingDummy", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_voidDream").SetReplacement("wstl_voidDreamRooster", 14), NewCardBlueprint("wstl_beautyAndBeast")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_allAroundHelper", 45), EmptyBlueprint(null, 12)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_rudoltaSleigh")),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint("wstl_alriune", 16))
                ), regions: 2);
            Build(New("AlriuneJuggernaut")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards("wstl_redShoes", "wstl_oldLady", "wstl_allAroundHelper", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_alriune"), NewCardBlueprint("wstl_burrowingHeaven", 10)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint("wstl_voidDream", 14)),
                    CreateTurn(NewCardBlueprint("wstl_voidDream", 25).SetReplacement("wstl_dontTouchMe", 14)),
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
                    CreateTurn(NewCardBlueprint("wstl_mirrorOfAdjustment", 10).SetReplacement("wstl_warmHeartedWoodsman", 11), NewCardBlueprint("wstl_magicalGirlSpade")),
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
                    CreateTurn(NewCardBlueprint("wstl_theLittlePrince", 25), NewCardBlueprint("wstl_queenBee", 25)),
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
                .AddRandomReplacementCards("wstl_forestKeeper", "wstl_runawayBird", "wstl_allAroundHelper")
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

            #region Region All
            Build(New("StrangeAssortmentAnthropoids")
                .SetDifficulty(1, 20)
                .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
                .AddRandomReplacementCards("wstl_wallLady", "wstl_mhz176", "wstl_wisdomScarecrow", "wstl_forsakenMurderer")
                .SetRedundantAbilities(Ability.Evolve, Ability.ExplodeOnDeath)
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_honouredMonk", 10, true, 15, "wstl_cloudedMonk")),
                    CreateTurn(NewCardBlueprint("wstl_oldLady", 40, true, 11, null)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_scorchedGirl", 15), EmptyBlueprint("wstl_silentEnsemble", 7)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_todaysShyLookAngry", 40, true, 14, "wstl_redHoodedMercenary")),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(15))
                    ), 0, 1, 2);
            Build(New("StrangeAssortmentFae")
                .SetDifficulty(1, 20)
                .AddDominantTribes(AbnormalPlugin.TribeFae)
                .AddRandomReplacementCards("wstl_fairyFestival", "wstl_wisdomScarecrow")
                .SetRedundantAbilities(Bloodfiend.ability, OneSided.ability)
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_nosferatu", 10, true, 15, "wstl_nosferatuBeast")),
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlDiamond", 40, true, 11, null)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlHeart", 15), EmptyBlueprint("wstl_theRoadHome", 7)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_laetitia", 40, true, 16, "wstl_knightOfDespair")),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(14))
                    ), 0, 1, 2);
            Build(New("StrangeAssortmentMechanical")
                .SetDifficulty(1, 20)
                .AddDominantTribes(AbnormalPlugin.TribeMechanical)
                .AddRandomReplacementCards("wstl_schadenfreude", "wstl_dontTouchMe")
                .SetRedundantAbilities(Ability.Sentry, Ability.Strafe, Punisher.ability, Woodcutter.ability)
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_schadenfreude", 10, true, 14, "wstl_warmHeartedWoodsman")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_theresia", 40, true, 11, "wstl_luminousBracelet")),
                    CreateTurn(NewCardBlueprint("wstl_allAroundHelper", 15), EmptyBlueprint("wstl_luminousBracelet", 7)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_trainingDummy", 40, true, 12, "wstl_singingMachine")),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint(14))
                    ), 0, 1, 2);
            #endregion

            #region Bosses
            ProspectorAbnormalBossP1 = Build(New("ProspectorAbnormalBossP1")
                .SetDifficulty(4, 7)
                .AddDominantTribes(AbnormalPlugin.TribeBotanic)
                .AddRandomReplacementCards("wstl_porccubus", "wstl_fragmentOfUniverse", "wstl_ppodae")
                .SetRedundantAbilities(Bloodfiend.ability, Roots.ability, Ability.Sharp, Ability.Deathtouch)
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_RUDOLTA_MULE"), NewCardBlueprint("wstl_wisdomScarecrow")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_scorchedGirl")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_snowWhitesApple")),
                    CreateTurn(EmptyBlueprint("wstl_canOfWellCheers", 6)),
                    CreateTurn(NewCardBlueprint("wstl_graveOfBlossoms", 25)),
                    CreateTurn(),
                    CreateTurn(EmptyBlueprint("wstl_todaysShyLook", 6))
                ), 3);
            ProspectorAbnormalBossP2 = Build(New("ProspectorAbnormalBossP2")
               .SetDifficulty(4, 7)
               .AddDominantTribes(AbnormalPlugin.TribeBotanic)
               .AddRandomReplacementCards("wstl_beautyAndBeast", "wstl_porccubus", "wstl_wisdomScarecrow")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_willBeBadWolf")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_redHoodedMercenary", 40)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_warmHeartedWoodsman", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_graveOfBlossoms", 7), EmptyBlueprint("wstl_fragmentOfUniverse", 6)),
                    CreateTurn()
                ), 3);
            AnglerAbnormalBossP1 = Build(New("AnglerAbnormalBossP1")
               .SetDifficulty(9, 11)
               .AddDominantTribes(AbnormalPlugin.TribeFae)
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_canOfWellCheers")),
                    CreateTurn(NewCardBlueprint("wstl_theFirebird")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival")),
                    CreateTurn(NewCardBlueprint("wstl_fairyFestival").SetReplacement("wstl_nosferatu", 11)),
                    CreateTurn()
                ), 3);
            AnglerAbnormalBossP2 = Build(New("AnglerAbnormalBossP2")
               .SetDifficulty(9, 11)
               .AddDominantTribes(AbnormalPlugin.TribeFae)
               .AddRandomReplacementCards("wstl_magicalGirlClover")
               .AddTurns(
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket", 25)),
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket"), NewCardBlueprint("wstl_theRoadHome", 10)),
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket"), NewCardBlueprint("BaitBucket", 10))
                ), 3);
            TrapperTraderAbnormalBossP1 = Build(New("TrapperTraderAbnormalBossP1")
               .SetDifficulty(14, 16)
               .AddDominantTribes(Tribe.Bird)
               .AddRandomReplacementCards("wstl_bigBird", "wstl_forestKeeper", "wstl_voidDreamRooster", "wstl_porccubus")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("TrapFrog")),
                    CreateTurn(NewCardBlueprint("wstl_trainingDummy", 30).SetReplacement("wstl_eyeballChick", 15)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_theFirebird", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_meatLantern", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_judgementBird", 25))
                ), 3);
            LeshyAbnormalBossP1 = Build(New("LeshyAbnormalBossP1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
               .AddRandomReplacementCards("wstl_derFreischutz", "wstl_armyInPink", "wstl_silentEnsemble")
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

            return;
            #endregion

            #region Rapture
            Build(New("RaptureEncounter1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(AbnormalPlugin.TribeDivine)
               .AddTurns(
                    CreateTurn()
               ), 3);
            #endregion
        }

        private static CardBlueprint EmptyBlueprint(int replacementChance) => NewCardBlueprint(null, replacementChance);
        private static CardBlueprint EmptyBlueprint(string replacement, int difficultyReplace) => NewCardBlueprint(null, 0, true, difficultyReplace, replacement);
        private static EncounterBlueprintData Build(EncounterBlueprintData encounter, params int[] regions)
        {
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
