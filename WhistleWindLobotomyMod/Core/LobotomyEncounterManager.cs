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
        public static Dictionary<int, List<EncounterBlueprintData>> ModEncounters = new()
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
            CardBlueprint nullCard = NewCardBlueprint(null, 25).SetReplacement(null);
            CardBlueprint workerBee = NewCardBlueprint("wstl_queenBeeWorker", 25);
            CardBlueprint nakedWorm = NewCardBlueprint("wstl_theNakedWorm", 25);
            CardBlueprint spiderling = NewCardBlueprint("wstl_spiderling", 25);
            CardBlueprint spiderBrood = NewCardBlueprint("wstl_spiderBrood", 25);

            #region Region 0
            StrangePack = Build(New("StrangePack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards("wstl_burrowingHeaven", "wstl_oldLady", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_ppodae", 10, true, 3, "wstl_ppodaeBuff")),
                    CreateTurn(NewCardBlueprint("wstl_heartOfAspiration", 25)),
                    CreateTurn()
                    ), 0);
            BitterPack = Build(New("BitterPack")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Canine)
                .AddRandomReplacementCards("wstl_canOfWellCheers", "wstl_ppodae", "wstl_redShoes", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_willBeBadWolf")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_redHoodedMercenary", 40)),
                    CreateTurn()
                    ), 0);
            StrangeFlock = Build(New("StrangeFlock")
                .SetDifficulty(1, 4)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards("wstl_oldLady", "wstl_willBeBadWolf", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_punishingBird", 30), NewCardBlueprint("wstl_judgementBird", 30)),
                    CreateTurn(),
                    CreateTurn(nullCard.SetReplacement("wstl_theFireBird", 4))
                    ), 0);
            HelperJuggernaut = Build(New("HelperJuggernaut")
                .SetDifficulty(1, 4)
                .AddDominantTribes(LobotomyCardManager.TribeMechanical)
                .AddRandomReplacementCards("wstl_behaviourAdjustment", "wstl_dontTouchMe", "wstl_mhz176")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_allAroundHelper")),
                    CreateTurn(),
                    CreateTurn(nullCard.SetReplacement("wstl_singingMachine", 4))
                    ), 0);
            #endregion

            #region Region 1
            StrangeBees = Build(New("StrangeBees")
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
                    CreateTurn(nullCard.SetReplacement("wstl_workerBee", 8)),
                    CreateTurn()
                    ), 1);
            StrangeCreatures1 = Build(New("StrangeCreatures1")
                .SetDifficulty(5, 10)
                .AddDominantTribes(LobotomyCardManager.TribeFae)
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
                ), 1);
            WormsNest = Build(New("WormsNest")
                .SetDifficulty(6, 10)
                .SetRedundantAbilities(SerpentsNest.ability)
                .AddDominantTribes(Tribe.Insect)
                .AddRandomReplacementCards("wstl_voidDream", "wstl_burrowingHeaven", "wstl_oldLady", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_theNakedNest"), nakedWorm),
                    CreateTurn(nakedWorm),
                    CreateTurn(nakedWorm, nakedWorm, nullCard.SetReplacement("wstl_theNakedWorm", 10)),
                    CreateTurn(nakedWorm, nullCard.SetReplacement("wstl_theNakedWorm", 9))
                ), 1);
            StrangeCreatures2 = Build(New("StrangeCreatures2")
                .SetDifficulty(6, 10)
                .AddDominantTribes(LobotomyCardManager.TribeBotanic)
                .AddRandomReplacementCards("wstl_oldLady", "wstl_allAroundHelper", "wstl_mhz176")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlHeart")),
                    CreateTurn(NewCardBlueprint("wstl-schadenfreude", 25)),
                    CreateTurn(NewCardBlueprint("wstl_forsakenMurderer")),
                    CreateTurn(NewCardBlueprint("wstl_porccubus", 25))
                ), 1);
            StrangeFish = Build(New("StrangeFish")
                .SetDifficulty(6, 14)
                .AddDominantTribes(Tribe.Bird)
                .SetRedundantAbilities(Ability.Submerge, Ability.WhackAMole, Ability.TailOnHit, Ability.Sharp, Punisher.ability)
                .AddRandomReplacementCards("wstl_voidDream", "wstl_punishingBird", "wstl_yang")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_canOfWellCheers", 25), nullCard.SetReplacement("wstl_magicalGirlDiamond")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_yin")),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn()
                ), 1, 2);
            #endregion

            #region Region 2
            StrangeHerd = Build(New("StrangeHerd")
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
                ), 2);
            AlriuneJuggernaut = Build(New("AlriuneJuggernaut")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Hooved)
                .AddRandomReplacementCards("wstl_redShoes", "wstl_oldLady", "wstl_allAroundHelper", "wstl_heartOfAspiration")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_alriune"), NewCardBlueprint("wstl_burrowingHeaven", 10)),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_voidDream", 25).SetReplacement("wstl_voidDreamRooster", 13)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_beautyAndBeast", 25)),
                    CreateTurn(),
                    CreateTurn()
                ), 2);
            SpidersNest = Build(New("SpidersNest")
                .SetDifficulty(11, 14)
                .AddDominantTribes(Tribe.Insect)
                .SetRedundantAbilities(BroodMother.ability)
                .AddRandomReplacementCards("wstl_redShoes", "wstl_heartOfAspiration", "wstl_ppodaeBuff", "wstl_voidDrem")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_spiderBud"), spiderBrood),
                    CreateTurn(spiderling.SetReplacement("wstl_spiderBrood", 14), spiderBrood),
                    CreateTurn(spiderBrood),
                    CreateTurn(nullCard.SetReplacement("wstl_spiderling", 13)),
                    CreateTurn(),
                    CreateTurn(spiderling.SetReplacement("wstl_spiderBrood", 14))
                ), 2);
            SwanJuggernaut = Build(New("SwanJuggernaut")
                .SetDifficulty(11, 14)
                .SetRedundantAbilities(Ability.WhackAMole, Ability.Sharp, Reflector.ability, Nettles.ability)
                .AddDominantTribes(Tribe.Bird)
                .AddRandomReplacementCards("wstl_willBeBadWolf", "wstl_dreamingCurrent", "wstl_allAroundHelper")
                .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_dreamOfABlackSwan"), nullCard.SetReplacement("wstl_dreamOfABlackSwanBrother3", 14)),
                    CreateTurn(NewCardBlueprint("wstl_dreamOfABlackSwanBrother5", 50), NewCardBlueprint("wstl_dreamOfABlackSwanBrother2", 50)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_bigBird", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_punishingBird", 25)),
                    CreateTurn(),
                    CreateTurn()
                ), 2);
            #endregion

            #region Bosses
            ProspectorAbnormalBossP1 = Build(New("ProspectorAbnormalBossP1")
                .SetDifficulty(4, 7)
                .AddDominantTribes(LobotomyCardManager.TribeBotanic)
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
               .AddDominantTribes(LobotomyCardManager.TribeBotanic)
               .AddRandomReplacementCards("wstl_ppodae", "wstl_porccubus")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_willBeBadWolf")),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_redHoodedMercenary", 25)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_warmHeartedWoodsman", 25)),
                    CreateTurn(),
                    CreateTurn(nullCard.SetReplacement("wstl_graveOfBlossoms", 7)),
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
                    CreateTurn(nullCard.SetReplacement("wstl_fairyFestival", 11)),
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
                   CreateTurn(NewCardBlueprint("BaitBucket"), nullCard.SetReplacement("wstl_punishingBird", 10)),
                   CreateTurn(),
                   CreateTurn(NewCardBlueprint("BaitBucket"), nullCard.SetReplacement("BaitBucket", 11))
                ), 3);
            TrapperTraderAbnormalBossP1 = Build(New("TrapperTraderAbnormalBossP1")
               .SetDifficulty(14, 16)
               .AddDominantTribes(LobotomyCardManager.TribeMechanical)
               .AddRandomReplacementCards("wstl_scorchedGirl", "wstl_bloodBath", "wstl_voidDreamRooster", "wst_porccubus")
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
               .AddDominantTribes(LobotomyCardManager.TribeFae)
               .AddRandomReplacementCards("wstl_derFreischutz", "wstl_armyInPink")
               .AddTurns(
                    CreateTurn(NewCardBlueprint("wstl_magicalGirlSpade")),
                    CreateTurn(NewCardBlueprint("wstl_funeralOfButterflies", 25)),
                    CreateTurn(NewCardBlueprint("wstl_mountainOfBodies", 25), NewCardBlueprint("wstl_silentOrchestra", 10)),
                    CreateTurn(nullCard.SetReplacement(null)),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_censored"))
                ), 3);
            PirateSkullAbnormalBossP1 = Build(New("PirateSkullAbnormalBossP1")
               .SetDifficulty(20, 20)
               .AddDominantTribes(Tribe.Bird)
               .AddRandomReplacementCards("wstl_SKELETON_SHRIMP")
               .AddTurns(
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_theFirebird")),
                    CreateTurn(nullCard),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_voidDreamRooster")),
                    CreateTurn(),
                    CreateTurn()
                ), 3);
            PirateSkullAbnormalBossP2 = Build(New("PirateSkullAbnormalBossP2")
               .SetDifficulty(20, 20)
               .AddDominantTribes(Tribe.Bird)
               .AddRandomReplacementCards("wstl_CRUMPLED_CAN")
               .AddTurns(
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_yin"), NewCardBlueprint("wstl_yang")),
                    CreateTurn(NewCardBlueprint("wstl_punishingBird"), NewCardBlueprint("wstl_judgementBird")),
                    CreateTurn(),
                    CreateTurn(),
                    CreateTurn(NewCardBlueprint("wstl_SKELETON_SHRIMP"), NewCardBlueprint("wstl_SKELETON_SHRIMP"), nullCard),
                    CreateTurn(),
                    CreateTurn()
                ), 3);
            #endregion
        }

        public static EncounterBlueprintData StrangePack { get; private set; }
        public static EncounterBlueprintData BitterPack { get; private set; }
        public static EncounterBlueprintData StrangeFlock { get; private set; }
        public static EncounterBlueprintData HelperJuggernaut { get; private set; }
        public static EncounterBlueprintData StrangeBees { get; private set; }
        public static EncounterBlueprintData StrangeCreatures1 { get; private set; }
        public static EncounterBlueprintData WormsNest { get; private set; }
        public static EncounterBlueprintData StrangeCreatures2 { get; private set; }
        public static EncounterBlueprintData StrangeFish { get; private set; }
        public static EncounterBlueprintData StrangeHerd { get; private set; }
        public static EncounterBlueprintData AlriuneJuggernaut { get; private set; }
        public static EncounterBlueprintData SpidersNest { get; private set; }
        public static EncounterBlueprintData SwanJuggernaut { get; private set; }
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
