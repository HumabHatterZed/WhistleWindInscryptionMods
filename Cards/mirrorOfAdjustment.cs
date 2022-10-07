using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MirrorOfAdjustment_O0981()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
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
                "wstl_mirrorOfAdjustment", "The Mirror of Adjustment",
                "A mirror that reflects nothing on its surface.",
                0, 1, 1, 0,
                Artwork.mirrorOfAdjustment, Artwork.mirrorOfAdjustment_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, statIcon: SpecialStatIcon.Mirror,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin,
                terrainType: CardHelper.TerrainType.TerrainAttack);
        }
    }
}