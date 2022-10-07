using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_FairyFestival_F0483()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };

            CardHelper.CreateCard(
                "wstl_fairyFestival", "Fairy Festival",
                "Everything will be peaceful while you're under the fairies' care.",
                1, 1, 1, 0,
                Artwork.fairyFestival, Artwork.fairyFestival_emission, pixelTexture: Artwork.fairyFestival_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}