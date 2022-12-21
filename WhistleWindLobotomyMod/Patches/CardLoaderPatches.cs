using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardLoader))]
    public static class CardLoaderPatch
    {
        // Removes select cards following specific sequences
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        public static void RemoveUniqueCards(ref List<CardInfo> __result)
        {
            if (WstlSaveManager.UsedBackwardClock)
            {
                __result.RemoveAll((x) => x.name.Equals("wstl_backwardClock"));
            }
            if (WstlSaveManager.OwnsApocalypseBird)
            {
                __result.RemoveAll((x) => x.name.Equals("wstl_punishingBird") || x.name.Equals("wstl_bigBird")
                || x.name.Equals("wstl_judgementBird"));
            }
            if (WstlSaveManager.OwnsJesterOfNihil)
            {
                __result.RemoveAll((x) => x.name.Contains("wstl_magicalGirl"));
            }
            if (WstlSaveManager.OwnsLyingAdult)
            {
                __result.RemoveAll((x) => x.name.Equals("wstl_theRoadHome") || x.name.Equals("wstl_ozma")
                || x.name.Equals("wstl_wisdomScarecrow") || x.name.Equals("wstl_warmHeartedWoodsman"));
            }
        }
    }
}
