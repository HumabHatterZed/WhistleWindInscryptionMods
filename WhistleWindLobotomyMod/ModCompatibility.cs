using BepInEx.Bootstrap;
using DiskCardGame;
using Infiniscryption.Achievements;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Patches;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        internal static class PackAPI
        {
            internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            internal static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed.");
                PackInfo pack = PackManager.GetPackInfo("wstl");
                pack.Title = "WhistleWind Lobotomy Mod";
                pack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack"));
                pack.Description = $"A set of {ObtainableLobotomyCards.Count} abnormal cards hailing from the world of Lobotomy Corporation.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }

        internal static class AchievementAPI
        {
            internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.achievements");

            // bosses
            internal static Achievement ThroughTheTwilight;
            internal static Achievement WhereAllPathsLead;
            internal static Achievement EndOfTheRoad;
            internal static Achievement ParadiseLost;

            // event decks
            internal static Achievement TheThreeBirds;
            internal static Achievement MagicalGirls;
            internal static Achievement YellowBrickRoad;

            // other
            internal static Achievement Blessing;
            internal static Achievement Impuritas;

            internal static void CreateAchievements()
            {
                Log.LogDebug("Achievements API is installed.");
                ModdedAchievementManager.AchievementGroup grp = ModdedAchievementManager.NewGroup(pluginGuid, "WhistleWind Lobotomy Mod", TextureLoader.LoadTextureFromFile("achievementBox.png")).ID;

                ThroughTheTwilight = ModdedAchievementManager.New(pluginGuid, "Through the Twilight", "Survive the apocalypse and defeat the Beast.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossTwilight.png")).ID;

                /*                WhereAllPathsLead = ModdedAchievementManager.New(pluginGuid, "Where All Paths Lead", "Hold on to hope and defeat the Fool.",
                                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossJester.png")).ID;

                                EndOfTheRoad = ModdedAchievementManager.New(pluginGuid, "End of the Road", "Keep your wits and defeat the Adult.",
                                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossEmerald.png")).ID;

                                ParadiseLost = ModdedAchievementManager.New(pluginGuid, "Paradise Lost", "Reject His gifts and delay the Saviour.",
                                    false, grp, TextureLoader.LoadTextureFromFile("achievementBossSaviour.png")).ID;*/

                Impuritas = ModdedAchievementManager.New(pluginGuid, "Impuritas Civitatis", "Meet Angela while looking for a third Sephirah to join you.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;

                TheThreeBirds = ModdedAchievementManager.New(pluginGuid, "The Three Birds", "You heard the story of the Black Forest.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementTwilight.png")).ID;

                MagicalGirls = ModdedAchievementManager.New(pluginGuid, "Magical Girls", "You walked the paths of all four magical girls.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementMagicalGirls.png")).ID;

                YellowBrickRoad = ModdedAchievementManager.New(pluginGuid, "Yellow Brick Road", "You visited the Emerald City.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementRoadToOz.png")).ID;

                Blessing = ModdedAchievementManager.New(pluginGuid, "Blessing", "You witnessed His coming.",
                    true, grp, TextureLoader.LoadTextureFromFile("achievementBlessing.png")).ID;

                HarmonyInstance.PatchAll(typeof(AchievementPatches));
            }
            internal static void Unlock(bool prerequisite, Achievement achievement)
            {
                if (Enabled && prerequisite)
                    AchievementManager.Unlock(achievement);
            }
        }
    }
}