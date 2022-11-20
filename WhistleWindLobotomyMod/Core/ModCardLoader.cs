using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Core
{
    public static class ModCardLoader // Methods specific to getting modded cards
    {
        public static CardInfo GetRandomRareModCard(int randomSeed)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.Rare, CardTemple.Nature);
            if (WstlSaveManager.UsedBackwardClock)
                unlockedCards.RemoveAll((x) => x.name == "wstl_backwardClock");

            if (WstlSaveManager.OwnsJesterOfNihil)
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_magicalGirlSpade"));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomChoosableModCard(int randomSeed, string riskLevel, CardTemple temple = CardTemple.Nature)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.ChoiceNode, temple).FindAll((x) => x.GetExtendedProperty("wstl:RiskLevel") == riskLevel);

            if (WstlSaveManager.OwnsApocalypseBird)
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_punishingBird") || x.name.Equals("wstl_bigBird")
                || x.name.Equals("wstl_judgementBird"));

            if (WstlSaveManager.OwnsJesterOfNihil)
                unlockedCards.RemoveAll((x) => x.name.Contains("wstl_magicalGirl"));

            if (WstlSaveManager.OwnsLyingAdult)
                unlockedCards.RemoveAll((x) => x.name.Equals("wstl_theRoadHome") || x.name.Equals("wstl_ozma")
                || x.name.Equals("wstl_wisdomScarecrow") || x.name.Equals("wstl_warmHeartedWoodsman"));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomSephirahCard(int randomSeed)
        {
            List<CardInfo> sephirahCards = GetSephirahCards();
            bool hasGottenAngelaBefore = WstlSaveManager.HasGottenAngelaOnce;

            // if the player has obtained all Sefirot + Angela, get a random modded death card
            if (sephirahCards.Count == 0)
                return GetRandomModDeathCard(randomSeed);

            // remove Angela if the player hasn't gotten her before and there are other Sephirah to get
            if (!hasGottenAngelaBefore && sephirahCards.Count > 1)
                sephirahCards.RemoveAll(x => x.name.Equals("wstl_angela"));

            return CardLoader.Clone(sephirahCards[SeededRandom.Range(0, sephirahCards.Count, randomSeed)]);
        }
        private static List<CardInfo> CreateUniqueModDeathcards()
        {
            List<CardInfo> list = new();
            List<CardInfo> list2 = new();
            // get a list of all mod death cards
            foreach (CardModificationInfo item in SaveFile.IsAscension ? DefaultDeathCards.CreateAscensionCardMods() : SaveManager.SaveFile.deathCardMods)
            {
                if (item.singletonId.StartsWith("wstl"))
                {
                    CardInfo item2 = CardLoader.CreateDeathCard(item);
                    list.Add(item2);
                }
            }
            // foreach mod death card, if it's not in the player's deck, add it to list2
            foreach (CardInfo item2 in list)
            {
                if (!RunState.DeckList.Contains(item2))
                    list2.Add(item2);
            }
            // return a list of unique death cards; otherwise return a list of all death cards
            return list2.Count > 0 ? list2 : list;
        }
        public static CardInfo GetRandomModDeathCard(int randomSeed)
        {
            List<CardInfo> deathCards = CreateUniqueModDeathcards();
            return CardLoader.Clone(deathCards[SeededRandom.Range(0, deathCards.Count, randomSeed)]);
        }
        private static List<CardInfo> GetSephirahCards() =>
            GetUnlockedModCards(CardHelper.SEPHIRAH_CARD, CardTemple.Nature);
        private static List<CardInfo> GetUnlockedModCards(CardMetaCategory category, CardTemple temple) =>
            CardLoader.GetUnlockedCards(category, temple).FindAll((x) => x.name.StartsWith("wstl"));
    }
}
