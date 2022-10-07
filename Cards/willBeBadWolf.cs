using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_WillBeBadWolf_F0258()
        {
            List<Ability> abilities = new()
            {
                BitterEnemies.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };

            CardHelper.CreateCard(
                "wstl_willBeBadWolf", "Big and Will be Bad Wolf",
                "It is no coincidence that wolves are the villains of so many tales.",
                3, 2, 2, 0,
                Artwork.willBeBadWolf, Artwork.willBeBadWolf_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}