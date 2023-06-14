using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardLoader))]
    internal class CardLoaderPatch
    {
        // Removes select cards following specific sequences
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        private static void RemoveUniqueCards(ref List<CardInfo> __result)
        {
            if (LobotomySaveManager.UsedBackwardClock)
                __result.RemoveAll(x => x.name.Equals("wstl_backwardClock"));

            if (LobotomySaveManager.OwnsApocalypseBird)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitBlackForest));

            if (LobotomySaveManager.OwnsJesterOfNihil)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitMagicalGirl));

            if (LobotomySaveManager.OwnsLyingAdult)
                __result.RemoveAll(x => x.HasTrait(LobotomyCardManager.TraitEmeraldCity));
        }
    }
}
