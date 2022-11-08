using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.mirrorOfAdjustment, Artwork.mirrorOfAdjustment_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, statIcon: SpecialStatIcon.Mirror,
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}