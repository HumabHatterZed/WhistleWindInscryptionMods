using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Pumpkin_F04116()
        {
            CardHelper.CreateCard(
                "wstl_ozmaPumpkin", "Pumpkin",
                "An orange gourd.",
                0, 1, 0, 0,
                Artwork.ozmaPumpkin, Artwork.ozmaPumpkin_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                terrainType: CardHelper.TerrainType.Terrain,
                evolveName: "wstl_ozmaPumpkinJack");
        }
    }
}