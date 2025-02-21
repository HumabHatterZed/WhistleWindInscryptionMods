using BepInEx.Bootstrap;
using Infiniscryption.Achievements;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Patches;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public static class AchievementAPI
    {
        // bosses
        public static Achievement ThroughTheTwilight;
        public static Achievement WhereAllPathsLead;
        public static Achievement EndOfTheRoad;
        public static Achievement ParadiseLost;

        // event decks
        public static Achievement TheThreeBirds;
        public static Achievement MagicalGirls;
        public static Achievement YellowBrickRoad;

        // ordeals
        public static Achievement FullMoon;
        public static Achievement Fruition;

        // other
        public static Achievement Blessing;
        public static Achievement Impuritas;

        public static void CreateAchievements()
        {
            ModdedAchievementManager.AchievementGroup grp = ModdedAchievementManager.NewGroup(LobotomyPlugin.pluginGuid, "WhistleWind Lobotomy Mod", TextureLoader.LoadTextureFromFile("achievementBox.png")).ID;

            ThroughTheTwilight = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Through the Twilight", "Survive the apocalypse and defeat the Beast.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementBossTwilight.png")).ID;

            /*                WhereAllPathsLead = ModdedAchievementManager.New(pluginGuid, "Where All Paths Lead", "Hold on to hope and defeat the Fool.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementBossJester.png")).ID;*/

            /*                EndOfTheRoad = ModdedAchievementManager.New(pluginGuid, "End of the Road", "Keep your wits and defeat the Adult.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementBossEmerald.png")).ID;*/

            /*                ParadiseLost = ModdedAchievementManager.New(pluginGuid, "Paradise Denied", "Reject His gifts and delay the Saviour.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementBossSaviour.png")).ID;*/

            FullMoon = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Our Work", "Complete a Midnight Ordeal.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;

            /*                Fruition = ModdedAchievementManager.New(pluginGuid, "The Trials", "Complete the Ordeals of White.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;*/

            Impuritas = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Impuritas Civitatis", "Meet Angela at a Sephirot choice node.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;

            TheThreeBirds = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Three Birds", "Gather the guardians of the Black Forest.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementTwilight.png")).ID;

            MagicalGirls = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Darkened Kingdom", "Unite the four magical girls.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementMagicalGirls.png")).ID;

            YellowBrickRoad = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Yellow Brick Road", "Reunite five long-lost friends.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementRoadToOz.png")).ID;

            Blessing = ModdedAchievementManager.New(LobotomyPlugin.pluginGuid, "Blessing", "You witnessed His coming.",
                true, grp, TextureLoader.LoadTextureFromFile("achievementBlessing.png")).ID;

            LobotomyPlugin.HarmonyInstance.PatchAll(typeof(AchievementPatches));
        }
        internal static void Unlock(bool prerequisite, Achievement achievement)
        {
            if (prerequisite)
                AchievementManager.Unlock(achievement);
        }
    }
}