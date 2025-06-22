using BepInEx.Bootstrap;
using Infiniscryption.Achievements;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Patches;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public static class Scrybes {
        internal static bool GrimoraEnabled => Chainloader.PluginInfos.ContainsKey("arackulele.inscryption.grimoramod");
        internal static bool P03Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.p03kayceerun");
    }

    public static class PackAPI {
        internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
        internal static void CreateCardPack() {
            PackManager.AddProtectedMetacategory(EventCard);
            PackManager.AddProtectedMetacategory(RuinaCard);
            PackManager.AddProtectedMetacategory(DonatorCard);

            PackInfo pack = PackManager.GetPackInfo<PackInfo>(LobotomyPlugin.pluginPrefix);
            pack.ValidFor.Clear();
            pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            pack.Title = "World of L Corp HQ";
            pack.Description = $"A set of [count] abnormalities hailing from L Corp HQ and the Library.";
            pack.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack.png"));

            PackInfo packPixel = PackManager.GetPackInfo<PackInfo>(LobotomyPlugin.pixelPrefix);
            packPixel.ValidFor.Remove(PackInfo.PackMetacategory.LeshyPack);
            packPixel.Title = "World of L Corp HQ";
            packPixel.Description = $"A set of [count] abnormalities hailing from L Corp HQ and the Library.";
            packPixel.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack.png"));
            packPixel.SplitPackByCardTemple = true;

            PackInfo pack2 = PackManager.GetPackInfo<PackInfo>(LobotomyPlugin.wonderlabPrefix);
            pack2.ValidFor.Clear();
            pack2.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            pack2.Title = "World of WonderLab";
            pack2.Description = $"A set of [count] abnormalities from Branch O-5681 and related branches.";
            pack2.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack_wl.png"));

            /*PackInfo pack3 = PackManager.GetPackInfo<PackInfo>(LobotomyPlugin.limbusPrefix);
            pack3.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            pack3.Title = "World of Limbus Company";
            pack3.Description = $"A set of {LimbusCards.Count} abnormalities encountered by Limbus Company's LCB team.";
            pack3.SetTexture(TextureLoader.LoadTextureFromFile("wstl_pack_lc.png"));*/

            EncounterPackInfo encounterPack = PackManager.GetPackInfo<EncounterPackInfo>(LobotomyPlugin.pluginPrefix);
            encounterPack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            encounterPack.Title = "Receptions of L Corp HQ";
            encounterPack.Description = "A set of [summary] encounters exclusively featuring abnormalities and related phenomena.";
        }
    }
}