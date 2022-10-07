using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_DellaLuna_D01105()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability
            };

            CardHelper.CreateCard(
                "wstl_dellaLuna", "Il Pianto della Luna",
                "In reality, man despairs at [c:bR]the moon[c:].",
                2, 6, 3, 0,
                Artwork.dellaLuna, Artwork.dellaLuna_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isDonator: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}