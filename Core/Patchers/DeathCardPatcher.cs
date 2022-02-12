using System.IO;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using HarmonyLib;

namespace WhistleWindLobotomyMod
{
    public class DeathCardPatcher
    {
        private static readonly string mirabelle = "wstl_mirabelleDeathCard";
        private static readonly string poussey = "wstl_posseyDeathCard";
        private static readonly string stemcell642 = "wstl_stemCell642DeathCard";

        public static CardInfo GetDeathCard()
        {
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            List<CardModificationInfo> choosableDeathcardMods = SaveManager.SaveFile.GetChoosableDeathcardMods();

            CardInfo cardInfo = null;

            if (choosableDeathcardMods.Count > 0)
            {
                CardModificationInfo cardModificationInfo = choosableDeathcardMods[SeededRandom.Range(0, choosableDeathcardMods.Count, randomSeed)];
                cardInfo = CardLoader.CreateDeathCard(cardModificationInfo);
            }
            if (cardInfo == null)
            {
                CardModificationInfo cardModificationInfo2 = new CardModificationInfo();
                cardModificationInfo2.nameReplacement = "Guinevere";
                cardModificationInfo2.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Wildling, 0, 4);
                cardModificationInfo2.attackAdjustment = 1;
                cardModificationInfo2.healthAdjustment = 2;
                cardInfo = CardLoader.CreateDeathCard(cardModificationInfo2);
            }
            return cardInfo;
        }

        [HarmonyPatch(typeof(RunState), "Initialize")]
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

            CardModificationInfo cardModInfo = new CardModificationInfo(3, 2);
            cardModInfo.nameReplacement = "Mirabelle";
            cardModInfo.singletonId = mirabelle;
            cardModInfo.abilities.Add(Ability.GuardDog);
            cardModInfo.bloodCostAdjustment = 2;
            cardModInfo.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 5, 1);
            SaveManager.SaveFile.deathCardMods.Add(cardModInfo);

            CardModificationInfo cardModInfo2 = new CardModificationInfo(2, 1);
            cardModInfo2.nameReplacement = "Poussey";
            cardModInfo2.singletonId = poussey;
            cardModInfo2.abilities.Add(Ability.Strafe);
            cardModInfo2.abilities.Add(Ability.Flying);
            cardModInfo2.bonesCostAdjustment = 4;
            cardModInfo2.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerMan, 5, 5);
            SaveManager.SaveFile.deathCardMods.Add(cardModInfo2);

            CardModificationInfo cardModInfo3 = new CardModificationInfo(1, 2);
            cardModInfo3.nameReplacement = "Stemcell-642";
            cardModInfo3.singletonId = stemcell642;
            cardModInfo3.abilities.Add(Ability.SplitStrike);
            cardModInfo3.bloodCostAdjustment = 1;
            cardModInfo3.deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Chief, 5, 2);
            SaveManager.SaveFile.deathCardMods.Add(cardModInfo3);

            Plugin.Log.LogInfo($"Added custom deathcards!");
        }
    }
}
