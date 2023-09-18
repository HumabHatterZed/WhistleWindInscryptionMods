using DiskCardGame;
using HarmonyLib;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Patches
{
    internal static class AchievementPatches
    {
        // this runs at the end of the run and whenever the KCM menu loads
        [HarmonyPostfix, HarmonyPatch(typeof(AscensionMenuScreens), nameof(AscensionMenuScreens.TryUnlockAchievements))]
        private static void UnlockAchievements()
        {
            if (LobotomySaveManager.UnlockedApocalypseBird)
                AchievementAPI.Unlock(AchievementAPI.TheThreeBirds);

            if (LobotomySaveManager.UnlockedJesterOfNihil)
                AchievementAPI.Unlock(AchievementAPI.MagicalGirls);

            if (LobotomySaveManager.UnlockedLyingAdult)
                AchievementAPI.Unlock(AchievementAPI.YellowBrickRoad);

            if (LobotomySaveManager.UnlockedAngela)
                AchievementAPI.Unlock(AchievementAPI.ParadiseLost);

            if (LobotomySaveManager.DefeatedApocalypseBoss)
                AchievementAPI.Unlock(AchievementAPI.ThroughTheTwilight);

            if (LobotomySaveManager.DefeatedJesterBoss)
                AchievementAPI.Unlock(AchievementAPI.WhereAllPathsLead);

            if (LobotomySaveManager.DefeatedEmeraldBoss)
                AchievementAPI.Unlock(AchievementAPI.EndOfTheRoad);

            if (LobotomySaveManager.DefeatedSaviourBoss)
                AchievementAPI.Unlock(AchievementAPI.ParadiseLost);
        }
    }
}
