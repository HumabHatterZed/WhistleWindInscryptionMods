using BepInEx.Bootstrap;
using DiskCardGame;
using Infiniscryption.Achievements;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Localizing;
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
                PackInfo pack = PackManager.GetPackInfo<PackInfo>(pluginPrefix);
                pack.Title = pluginName;
                pack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack"));
                pack.Description = $"A set of {ObtainableLobotomyCards.Count} cards based on the abnormalities from Lobotomy Corporation and Library of Ruina.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);

                EncounterPackInfo encounterPack = PackManager.GetPackInfo<EncounterPackInfo>(pluginPrefix);
                encounterPack.Title = pluginName;
                //encounterPack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack"));
                encounterPack.Description = "A set of [summary] exclusively featuring abnormalities and related phenomena.";
                encounterPack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
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

                Impuritas = ModdedAchievementManager.New(pluginGuid, "Impuritas Civitatis", "Meet Angela at a Sephirot choice node.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;

                const string ThreeBirds = "Three Birds";
                const string ThreeBirdsDesc = "Gather the guardians of the Black Forest.";
                TheThreeBirds = ModdedAchievementManager.New(pluginGuid, ThreeBirds, ThreeBirdsDesc, false, grp, TextureLoader.LoadTextureFromFile("achievementTwilight.png")).ID;

                //LobotomyTranslator.TranslateToFrench("ThreeBirdsAchievement", ThreeBirds, "Trois Oiseaux");
                //LobotomyTranslator.TranslateToFrench("ThreeBirdsAchievementDesc", ThreeBirdsDesc, "Rassembler les gardiens de la Forêt Noire.");

                MagicalGirls = ModdedAchievementManager.New(pluginGuid, "Full House", "Bring all of the magical girls together.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementMagicalGirls.png")).ID;

                YellowBrickRoad = ModdedAchievementManager.New(pluginGuid, "Yellow Brick Road", "Reunite a group of long-lost friends.",
                    false, grp, TextureLoader.LoadTextureFromFile("achievementRoadToOz.png")).ID;

                const string BlessingName = "Blessing";
                const string BlessingNameDesc = "You witnessed His coming.";
                Blessing = ModdedAchievementManager.New(pluginGuid, BlessingName, BlessingNameDesc, true, grp, TextureLoader.LoadTextureFromFile("achievementBlessing.png")).ID;

                //LobotomyTranslator.TranslateToFrench("BlessingAchievement", BlessingName, "Bénédiction");
                //LobotomyTranslator.TranslateToFrench("BlessingAchievementDesc", BlessingNameDesc, "Vous avez été témoin de Sa venue.");

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