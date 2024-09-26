using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
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
            List<CardInfo> result = new(__result);
            __result.AddRange(LobotomyCardManager.ObtainableLobotomyCards.Where(x => x.HasCardMetaCategory(category) && x.temple != temple && !result.Contains(x)));
            
            if (LobotomySaveManager.UsedBackwardClock)
                __result.RemoveAll(x => x.name.Equals("wstl_backwardClock"));

            if (LobotomySaveManager.OwnsApocalypseBird)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.BlackForest));

            if (LobotomySaveManager.OwnsJesterOfNihil)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.MagicalGirl));

            if (LobotomySaveManager.OwnsLyingAdult)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.EmeraldCity));

            __result = CardLoader.RemoveDeckSingletonsIfInDeck(__result);
        }
    }
}
