using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using DiskCardGame;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod.Core
{
    public static class ModCardLoader // Methods specific to getting modded cards
    {
        public static List<CardInfo> GetUnlockedModCards(CardMetaCategory category, CardTemple temple)
        {
            return CardLoader.GetUnlockedCards(category, temple).FindAll((CardInfo x) => x.name.StartsWith("wstl"));
        }
        public static CardInfo GetRandomChoosableModCard(int randomSeed, string riskLevel, CardTemple temple = CardTemple.Nature)
        {
            List<CardInfo> unlockedCards = ModCardLoader.GetUnlockedModCards(CardMetaCategory.ChoiceNode, temple).FindAll((CardInfo x) => x.GetExtendedProperty("wstl:RiskLevel") == riskLevel);
            
            if (WstlSaveManager.HasApocalypse)
            {
                unlockedCards.RemoveAll((CardInfo x) => x.name == "wstl_punishingBird"
                || x.name == "wstl_bigBird" || x.name == "wstl_judgementBird");
            }
            if (WstlSaveManager.HasJester)
            {
                unlockedCards.RemoveAll((CardInfo x) => x.name.Contains("wstl_magicalGirl"));
            }

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomRareModCard(int randomSeed)
        {
            List<CardInfo> unlockedCards = ModCardLoader.GetUnlockedModCards(CardMetaCategory.Rare, CardTemple.Nature);
            if (WstlSaveManager.HasUsedBackwardClock)
            {
                unlockedCards.RemoveAll((CardInfo x) => x.name == "wstl_backwardClock");
            }
            if (WstlSaveManager.HasJester)
            {
                unlockedCards.RemoveAll((CardInfo x) => x.name == "wstl_magicalGirlSpade");
            }
            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
    }
}
