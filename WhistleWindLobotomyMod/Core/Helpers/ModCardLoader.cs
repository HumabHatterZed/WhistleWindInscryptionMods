using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod
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
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_punishingBird") || x.name.Equals("wstl_bigBird")
                || x.name.Equals("wstl_judgementBird"));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomRareModCard(int randomSeed)
        {
            List<CardInfo> unlockedCards = ModCardLoader.GetUnlockedModCards(CardMetaCategory.Rare, CardTemple.Nature);
            if (WstlSaveManager.HasUsedBackwardClock)
            {
                unlockedCards.RemoveAll((CardInfo x) => x.name == "wstl_backwardClock");
            }
            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
    }
}
