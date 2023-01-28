using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWind.LobotomyMod.Core;

namespace WhistleWind.LobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardLoader))]
    public static class CardLoaderPatch
    {
        // Removes select cards following specific sequences
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        public static void RemoveUniqueCards(ref List<CardInfo> __result)
        {
            if (LobotomySaveManager.UsedBackwardClock)
            {
                __result.RemoveAll((x) => x.name.Equals("wstl_backwardClock"));
            }
            if (LobotomySaveManager.OwnsApocalypseBird)
            {
                __result.RemoveAll((x) => x.name.Equals("wstl_punishingBird") || x.name.Equals("wstl_bigBird")
                || x.name.Equals("wstl_judgementBird"));
            }
            if (LobotomySaveManager.OwnsJesterOfNihil)
            {
                __result.RemoveAll((x) => x.name.Contains("wstl_magicalGirl"));
            }
            if (LobotomySaveManager.OwnsLyingAdult)
            {
                __result.RemoveAll((x) => x.name.Equals("wstl_theRoadHome") || x.name.Equals("wstl_ozma")
                || x.name.Equals("wstl_wisdomScarecrow") || x.name.Equals("wstl_warmHeartedWoodsman"));
            }
        }
    }
}
