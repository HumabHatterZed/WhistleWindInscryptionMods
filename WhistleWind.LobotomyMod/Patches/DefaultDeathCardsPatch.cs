using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(DefaultDeathCards))]
    public static class DefaultDeathCardsPatch
    {
        // Adds custom death cards
        [HarmonyPostfix, HarmonyPatch(nameof(DefaultDeathCards.CreateDefaultCardMods))]
        public static void AddDeathCards(ref List<CardModificationInfo> __result)
        {
            __result.Add(new CardModificationInfo(3, 2)
            {
                nameReplacement = "Mirabelle",
                singletonId = "wstl_mirabelleDeathCard",
                abilities = { Ability.GuardDog },
                bloodCostAdjustment = 2,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 5, 1)
            });
            __result.Add(new CardModificationInfo(2, 1)
            {
                nameReplacement = "Poussey",
                singletonId = "wstl_posseyDeathCard",
                abilities = { Ability.Strafe, Ability.Flying },
                bonesCostAdjustment = 4,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerMan, 5, 5)
            });
            __result.Add(new CardModificationInfo(1, 2)
            {
                nameReplacement = "Stemcell-642",
                singletonId = "wstl_stemCell642DeathCard",
                abilities = { Ability.SplitStrike },
                bloodCostAdjustment = 1,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Chief, 5, 2)
            });
            __result.Add(new CardModificationInfo(1, 3)
            {
                nameReplacement = "Noah",
                singletonId = "wstl_noahDeathCard",
                abilities = { Bloodfiend.ability },
                bonesCostAdjustment = 4,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Prospector, 1, 4)
            });
        }

        [HarmonyPostfix, HarmonyPatch(nameof(DefaultDeathCards.CreateAscensionCardMods))]
        public static void AddAscensionDeathCards(ref List<CardModificationInfo> __result)
        {
            __result.Add(new CardModificationInfo(3, 1)
            {
                nameReplacement = "Yumi",
                singletonId = "wstl_yumiDeathCard",
                abilities = { Ability.DebuffEnemy },
                bonesCostAdjustment = 6,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 2, 3)
            });
            __result.Add(new CardModificationInfo(1, 1)
            {
                nameReplacement = "Summer",
                singletonId = "wstl_summerDeathCard",
                abilities = { Marksman.ability },
                bloodCostAdjustment = 1,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Wildling, 3, 0)
            });
            __result.Add(new CardModificationInfo(2, 4)
            {
                nameReplacement = "Currince",
                singletonId = "wstl_currinceDeathCard",
                abilities = { Ability.StrafePush },
                bloodCostAdjustment = 2,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Enchantress, 4, 2)
            });
            __result.Add(new CardModificationInfo(1, 2)
            {
                nameReplacement = "Igoree",
                singletonId = "wstl_igoreeDeathCard",
                abilities = { Ability.BeesOnHit },
                bloodCostAdjustment = 1,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 2, 2)
            });
            __result.Add(new CardModificationInfo(2, 5)
            {
                nameReplacement = "Genie",
                singletonId = "wstl_genieDeathCard",
                abilities = { Assimilator.ability },
                bloodCostAdjustment = 3,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Robot, 2, 5)
            });
            __result.Add(new CardModificationInfo(4, 2)
            {
                nameReplacement = "Mao",
                singletonId = "wstl_maoDeathCard",
                abilities = { Woodcutter.ability },
                bloodCostAdjustment = 3,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Gravedigger, 0, 3)
            });
            __result.Add(new CardModificationInfo(1, 2)
            {
                nameReplacement = "Evangeline",
                singletonId = "wstl_evangelineDeathCard",
                abilities = { Ability.Evolve },
                bonesCostAdjustment = 2,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Wildling, 3, 0)
            });
            __result.Add(new CardModificationInfo(1, 1)
            {
                nameReplacement = "Ttungsil",
                singletonId = "wstl_ttungsilDeathCard",
                abilities = { BitterEnemies.ability },
                bonesCostAdjustment = 2,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerMan, 4, 4)
            });
            __result.Add(new CardModificationInfo(2, 2)
            {
                nameReplacement = "Mabel",
                singletonId = "wstl_mabelDeathCard",
                abilities = { Ability.SplitStrike },
                bonesCostAdjustment = 2,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 5, 2)
            });
        }
    }
}
