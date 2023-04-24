using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomyCardLoader // Methods specific to getting modded cards
    {
        public static CardInfo GetRandomRareModCard(int randomSeed)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.Rare);
            if (LobotomySaveManager.UsedBackwardClock)
                unlockedCards.RemoveAll((x) => x.name == "wstl_backwardClock");

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomChoosableModCard(int randomSeed, string riskLevel)
        {
            List<CardInfo> unlockedCards = GetUnlockedModCards(CardMetaCategory.ChoiceNode).FindAll((x) => x.GetExtendedProperty("wstl:RiskLevel") == riskLevel);

            if (LobotomySaveManager.OwnsApocalypseBird)
                unlockedCards.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitBlackForest));

            if (LobotomySaveManager.OwnsJesterOfNihil)
                unlockedCards.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitMagicalGirl));

            if (LobotomySaveManager.OwnsLyingAdult)
                unlockedCards.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitEmeraldCity));

            return CardLoader.Clone(unlockedCards[SeededRandom.Range(0, unlockedCards.Count, randomSeed)]);
        }
        public static CardInfo GetRandomSephirahCard(int randomSeed)
        {
            List<CardInfo> sephirahCards = GetSephirahCards();

            // if the player has obtained all Sefirot + Angela, get a random modded death card
            if (sephirahCards.Count == 0)
                return GetRandomModDeathCard(randomSeed);

            // remove Angela if the player hasn't gotten her before and there are other Sephirah to get
            if (!LobotomySaveManager.UnlockedAngela && sephirahCards.Count > 1)
                sephirahCards.RemoveAll(x => x.name.Equals("wstl_angela"));

            return CardLoader.Clone(sephirahCards[SeededRandom.Range(0, sephirahCards.Count, randomSeed)]);
        }
        private static List<CardInfo> CreateUniqueModDeathcards()
        {
            List<CardInfo> allDeathCards = new();
            List<CardInfo> uniqueDeathCard = new();

            foreach (CardModificationInfo mod in SaveFile.IsAscension ? DefaultDeathCards.CreateAscensionCardMods() : SaveManager.SaveFile.deathCardMods)
            {
                if (mod.singletonId.StartsWith("wstl"))
                    allDeathCards.Add(CardLoader.CreateDeathCard(mod));
            }
            uniqueDeathCard.AddRange(allDeathCards.Where(x => !RunState.DeckList.Contains(x)));

            return uniqueDeathCard.Count > 0 ? uniqueDeathCard : allDeathCards;
        }
        public static CardInfo GetRandomModDeathCard(int randomSeed)
        {
            List<CardInfo> deathCards = CreateUniqueModDeathcards();
            return CardLoader.Clone(deathCards[SeededRandom.Range(0, deathCards.Count, randomSeed)]);
        }
        public static List<CardInfo> GetSephirahCards() => CardHelper.RemoveOwnedSingletons().FindAll(x => x.HasTrait(LobotomyCardManager.TraitSephirah));
        public static List<CardInfo> GetUnlockedModCards(CardMetaCategory category) => CardHelper.RemoveOwnedSingletons().FindAll(x => x.name.StartsWith("wstl") && x.HasCardMetaCategory(category) && ConceptProgressionTree.Tree.CardUnlocked(x));
    }
}