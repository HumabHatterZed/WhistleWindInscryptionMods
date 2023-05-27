using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomyCardLoader // Methods specific to getting modded cards
    {
        public static CardInfo GetRandomRareModCard(int randomSeed)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.Rare);
            if (LobotomySaveManager.UsedBackwardClock)
                unlockedCards.RemoveAll((x) => x.name == "wstl_backwardClock");

            if (unlockedCards.Count == 0)
                return CardLoader.Clone(CardLoader.GetCardByName("wstl_trainingDummy"));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomChoosableModCard(int randomSeed, string riskLevel)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.ChoiceNode).FindAll(x => x.GetExtendedProperty("wstl:RiskLevel") == riskLevel);

            if (LobotomySaveManager.OwnsApocalypseBird)
                unlockedCards.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitBlackForest));

            if (LobotomySaveManager.OwnsJesterOfNihil)
                unlockedCards.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitMagicalGirl));

            if (LobotomySaveManager.OwnsLyingAdult)
                unlockedCards.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitEmeraldCity));

            if (unlockedCards.Count == 0)
                return CardLoader.Clone(CardLoader.GetCardByName("wstl_trainingDummy"));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomModDeathCard(int randomSeed)
        {
            List<CardInfo> deathCards = CreateUniqueModDeathcards();
            return deathCards[SeededRandom.Range(0, deathCards.Count, randomSeed)];
        }
        private static List<CardInfo> CreateUniqueModDeathcards()
        {
            List<CardInfo> allDeathCards = new();

            foreach (CardModificationInfo mod in SaveFile.IsAscension ? DefaultDeathCards.CreateAscensionCardMods() : SaveManager.SaveFile.deathCardMods)
            {
                if (mod.singletonId != null && mod.singletonId.StartsWith("wstl"))
                    allDeathCards.Add(CardLoader.CreateDeathCard(mod));
            }
            List<CardInfo> uniqueDeathCard = allDeathCards.FindAll(x => !RunState.DeckList.Contains(x));

            return uniqueDeathCard.Count > 0 ? uniqueDeathCard : allDeathCards;
        }
        public static List<CardInfo> GetSephirahCards()
        {
            List<CardInfo> obtainableCards = new(LobotomyCardManager.AllLobotomyCards);
            obtainableCards.RemoveAll(x => x.LacksTrait(LobotomyCardManager.TraitSephirah) || x.name == "wstl_sephirahTipherethB");

            return CardLoader.RemoveDeckSingletonsIfInDeck(obtainableCards);
        }
        public static List<CardInfo> GetUnlockedModCards(CardMetaCategory category)
        {
            List<CardInfo> obtainableCards = new(LobotomyCardManager.ObtainableLobotomyCards);
            obtainableCards.RemoveAll(x => x.LacksCardMetaCategory(category) || !ConceptProgressionTree.Tree.CardUnlocked(x));

            return CardLoader.RemoveDeckSingletonsIfInDeck(obtainableCards);
        }
    }
}