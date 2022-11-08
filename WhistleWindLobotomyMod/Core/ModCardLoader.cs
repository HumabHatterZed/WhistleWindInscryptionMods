using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Core
{
    public static class ModCardLoader // Methods specific to getting modded cards
    {
        public static List<CardInfo> GetUnlockedModCards(CardMetaCategory category, CardTemple temple)
        {
            return CardLoader.GetUnlockedCards(category, temple).FindAll((x) => x.name.StartsWith("wstl"));
        }
        public static CardInfo GetRandomChoosableModCard(int randomSeed, string riskLevel, CardTemple temple = CardTemple.Nature)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.ChoiceNode, temple).FindAll((x) => x.GetExtendedProperty("wstl:RiskLevel") == riskLevel);

            if (WstlSaveManager.OwnsApocalypseBird)
            {
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_punishingBird") || x.name.Equals("wstl_bigBird")
                || x.name.Equals("wstl_judgementBird"));
            }
            if (WstlSaveManager.OwnsJesterOfNihil)
            {
                unlockedCards.RemoveAll((x) => x.name.Contains("wstl_magicalGirl"));
            }
            if (WstlSaveManager.OwnsLyingAdult)
            {
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_theRoadHome") || x.name.Equals("wstl_ozma")
                || x.name.Equals("wstl_wisdomScarecrow") || x.name.Equals("wstl_warmHeartedWoodsman"));
            }

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomRareModCard(int randomSeed)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.Rare, CardTemple.Nature);
            if (WstlSaveManager.UsedBackwardClock)
                unlockedCards.RemoveAll((x) => x.name == "wstl_backwardClock");

            if (WstlSaveManager.OwnsJesterOfNihil)
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_magicalGirlSpade"));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
    }
}
