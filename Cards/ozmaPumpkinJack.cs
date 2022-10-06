using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

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
            List<Trait> traits = new()
            {
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainBackground
            };
            CardHelper.CreateCard(
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                1, 3, 1, 0,
                Resources.ozmaPumpkinJack, Resources.ozmaPumpkinJack_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits, appearances: appearances,
                statIcon: SpecialStatIcon.Mirror, isChoice: false);
        }
    }
}