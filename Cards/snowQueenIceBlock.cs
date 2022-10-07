using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_SnowQueenIceBlock_F0137()
        {
            CardHelper.CreateCard(
                "wstl_snowQueenIceBlock", "Block of Ice",
                "The palace was cold and lonely.",
                0, 1, 0, 0,
                Artwork.snowQueenIceBlock, Artwork.snowQueenIceBlock_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                terrainType: CardHelper.TerrainType.Terrain);
        }
    }
}