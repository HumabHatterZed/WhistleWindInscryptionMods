using DiskCardGame;
using HarmonyLib;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomySaveManager;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Patches
{
    internal static class AchievementPatches
    {
        // this runs at the end of the run and whenever the KCM menu loads
        [HarmonyPostfix, HarmonyPatch(typeof(AscensionMenuScreens), nameof(AscensionMenuScreens.TryUnlockAchievements))]
        private static void UnlockAchievements()
        {
            // RulebookPatches.ApocalypseEnding = null; // if I make a harder version of the boss, we'll want to reset the string here
            // bosses    
            AchievementAPI.Unlock(DefeatedApocalypseBoss, AchievementAPI.ThroughTheTwilight);
            //AchievementAPI.Unlock(DefeatedJesterBoss, AchievementAPI.WhereAllPathsLead);
            //AchievementAPI.Unlock(DefeatedEmeraldBoss, AchievementAPI.EndOfTheRoad);
            //AchievementAPI.Unlock(DefeatedSaviourBoss, AchievementAPI.ParadiseLost);

            // event cards
            AchievementAPI.Unlock(UnlockedApocalypseBird, AchievementAPI.TheThreeBirds);
            AchievementAPI.Unlock(UnlockedJesterOfNihil, AchievementAPI.MagicalGirls);
            AchievementAPI.Unlock(UnlockedLyingAdult, AchievementAPI.YellowBrickRoad);
            AchievementAPI.Unlock(UnlockedAngela, AchievementAPI.Impuritas);

            // other
            AchievementAPI.Unlock(LobotomyConfigManager.Instance.HasSeenHim, AchievementAPI.Blessing);
        }
    }
}
