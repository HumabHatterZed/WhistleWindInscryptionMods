using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MagicalGirlSpade_O0173()
        {
            List<Ability> abilities = new()
            {
                Protector.ability
            };

            CardHelper.CreateCard(
                "wstl_magicalGirlSpade", "Magical Girl",
                "A loyal knight fighting to protect those close to her.",
                1, 4, 1, 0,
                Artwork.magicalGirlSpade, Artwork.magicalGirlSpade_emission, pixelTexture: Artwork.magicalGirlSpade_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}