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
        private static readonly string mirabelle = "wstl_mirabelleDeathCard";
        private static readonly string poussey = "wstl_posseyDeathCard";
        private static readonly string stemcell642 = "wstl_stemCell642DeathCard";

        [HarmonyPatch(typeof(RunState), nameof(RunState.Initialize))]
        [HarmonyPostfix]
        public static void AddDeathCards()
        {
            int i = 0;
            while (i < SaveManager.SaveFile.deathCardMods.Count)
            {
                if (SaveManager.SaveFile.deathCardMods[i].singletonId != null)
                {
                    if (SaveManager.SaveFile.deathCardMods[i].singletonId.StartsWith("wstl"))
                    {
                        SaveManager.SaveFile.deathCardMods.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    i++;
                }
            }

            CardModificationInfo cardModInfo = new(3, 2);
            cardModInfo.nameReplacement = "Mirabelle";
            cardModInfo.singletonId = mirabelle;
            cardModInfo.abilities.Add(Ability.GuardDog);
            cardModInfo.bloodCostAdjustment = 2;
            cardModInfo.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 5, 1);
            SaveManager.SaveFile.deathCardMods.Add(cardModInfo);

            CardModificationInfo cardModInfo2 = new(2, 1);
            cardModInfo2.nameReplacement = "Poussey";
            cardModInfo2.singletonId = poussey;
            cardModInfo2.abilities.Add(Ability.Strafe);
            cardModInfo2.abilities.Add(Ability.Flying);
            cardModInfo2.bonesCostAdjustment = 4;
            cardModInfo2.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerMan, 5, 5);
            SaveManager.SaveFile.deathCardMods.Add(cardModInfo2);

            CardModificationInfo cardModInfo3 = new(1, 2);
            cardModInfo3.nameReplacement = "Stemcell-642";
            cardModInfo3.singletonId = stemcell642;
            cardModInfo3.abilities.Add(Ability.SplitStrike);
            cardModInfo3.bloodCostAdjustment = 1;
            cardModInfo3.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Chief, 5, 2);
            SaveManager.SaveFile.deathCardMods.Add(cardModInfo3);
        }
    }
}
