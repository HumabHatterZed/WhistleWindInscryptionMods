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
    public static class Scrybes
    {
        internal static bool GrimoraEnabled => Chainloader.PluginInfos.ContainsKey("arackulele.inscryption.grimoramod");
        internal static bool P03Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.p03kayceerun");
    }

    public static class PackAPI
    {
        internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
        internal static void CreateCardPack()
        {
            PackInfo pack = PackManager.GetPackInfo<PackInfo>(pluginPrefix);
            pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            pack.Title = pluginName;
            pack.Description = $"A set of 90 cards based on the abnormalities from Lobotomy Corporation and Library of Ruina.";
            pack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack"));

            /*                EncounterPackInfo encounterPack = PackManager.GetPackInfo<EncounterPackInfo>(pluginPrefix);
                            encounterPack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
                            encounterPack.Title = pluginName;
                            encounterPack.Description = "A set of [summary] exclusively featuring abnormalities and related phenomena.";*/
            //encounterPack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack"));

        }
    }

    public static class AchievementAPI
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

        // ordeals
        internal static Achievement FullMoon;
        internal static Achievement Fruition;

        // other
        internal static Achievement Blessing;
        internal static Achievement Impuritas;

        public static void CreateAchievements()
        {
            ModdedAchievementManager.AchievementGroup grp = ModdedAchievementManager.NewGroup(pluginGuid, "WhistleWind Lobotomy Mod", TextureLoader.LoadTextureFromFile("achievementBox.png")).ID;

            ThroughTheTwilight = ModdedAchievementManager.New(pluginGuid, "Through the Twilight", "Survive the apocalypse and defeat the Beast.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementBossTwilight.png")).ID;

            /*                WhereAllPathsLead = ModdedAchievementManager.New(pluginGuid, "Where All Paths Lead", "Hold on to hope and defeat the Fool.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementBossJester.png")).ID;*/

            /*                EndOfTheRoad = ModdedAchievementManager.New(pluginGuid, "End of the Road", "Keep your wits and defeat the Adult.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementBossEmerald.png")).ID;*/

            /*                ParadiseLost = ModdedAchievementManager.New(pluginGuid, "Paradise Lost", "Reject His gifts and delay the Saviour.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementBossSaviour.png")).ID;*/

            FullMoon = ModdedAchievementManager.New(pluginGuid, "Our Work", "Complete a Midnight Ordeal.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;

            /*                Fruition = ModdedAchievementManager.New(pluginGuid, "The Trials", "Complete the Ordeals of White.",
                                false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;*/

            Impuritas = ModdedAchievementManager.New(pluginGuid, "Impuritas Civitatis", "Meet Angela at a Sephirot choice node.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementImpuritas.png")).ID;

            TheThreeBirds = ModdedAchievementManager.New(pluginGuid, "Three Birds", "Gather the guardians of the Black Forest.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementTwilight.png")).ID;

            MagicalGirls = ModdedAchievementManager.New(pluginGuid, "Full House", "Unite the four magical girls.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementMagicalGirls.png")).ID;

            YellowBrickRoad = ModdedAchievementManager.New(pluginGuid, "Yellow Brick Road", "Reunite five long-lost friends.",
                false, grp, TextureLoader.LoadTextureFromFile("achievementRoadToOz.png")).ID;

            Blessing = ModdedAchievementManager.New(pluginGuid, "Blessing", "You witnessed His coming.",
                true, grp, TextureLoader.LoadTextureFromFile("achievementBlessing.png")).ID;

            LobotomyPlugin.HarmonyInstance.PatchAll(typeof(AchievementPatches));
        }
        internal static void Unlock(bool prerequisite, Achievement achievement)
        {
            if (Enabled && prerequisite)
                AchievementManager.Unlock(achievement);
        }
    }
}