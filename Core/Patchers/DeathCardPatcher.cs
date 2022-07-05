using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    // Adds custom deaths cards to the death card pool
    public class DeathCardPatcher
    {
        [HarmonyPatch(typeof(DefaultDeathCards), nameof(DefaultDeathCards.CreateDefaultCardMods))]
        [HarmonyPostfix]
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
        }
    }
}
