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
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialTriggeredAbility.Mirror
            };

            CardHelper.CreateCard(
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                1, 3, 1, 0,
                Artwork.ozmaPumpkinJack, Artwork.ozmaPumpkinJack_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), appearances: new(),
                statIcon: SpecialStatIcon.Mirror, terrainType: CardHelper.TerrainType.TerrainAttack);
        }
    }
}