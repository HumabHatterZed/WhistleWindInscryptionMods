using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_OzmaPumpkinJack_F04116()
        {
            CardHelper.CreateCard(
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                1, 3, 1, 0,
                Artwork.ozmaPumpkinJack, Artwork.ozmaPumpkinJack_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), appearances: new(),
                terrainType: CardHelper.TerrainType.TerrainAttack);
        }
    }
}