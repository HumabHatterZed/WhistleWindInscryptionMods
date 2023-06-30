using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardLoader))]
    internal class CardLoaderPatch
    {
        // Corrects the possible chooseable cards to exclude certain cards and to include non-Nature Temple cards
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        private static void RemoveUniqueCards(ref List<CardInfo> __result, CardMetaCategory category, CardTemple temple)
        {
            // since some of the cards aren't of the Nature temple, we need to manually add those
            List<CardInfo> nonTempleCards = LobotomyCardManager.ObtainableLobotomyCards.FindAll(x => x.HasCardMetaCategory(category) && x.temple != temple);
            __result.AddRange(nonTempleCards);

            if (LobotomySaveManager.UsedBackwardClock)
                __result.RemoveAll(x => x.name.Equals("wstl_backwardClock"));

            if (LobotomySaveManager.OwnsApocalypseBird)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitBlackForest));

            if (LobotomySaveManager.OwnsJesterOfNihil)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitMagicalGirl));

            if (LobotomySaveManager.OwnsLyingAdult)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitEmeraldCity));

            __result = CardLoader.RemoveDeckSingletonsIfInDeck(__result);
        }
    }
}
