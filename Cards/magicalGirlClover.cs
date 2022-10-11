using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MagicalGirlClover_O01111()
        {
            List<Ability> abilities = new()
            {
                Burning.ability
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard
            };

            CardHelper.CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                1, 2, 1, 0,
                Artwork.magicalGirlClover, Artwork.magicalGirlClover_emission, pixelTexture: Artwork.magicalGirlClover_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: metaTypes);
        }
    }
}