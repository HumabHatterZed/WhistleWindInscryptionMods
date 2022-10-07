using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MountainOfBodies_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };

            CardHelper.CreateCard(
                "wstl_mountainOfBodies", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                2, 1, 2, 0,
                Artwork.mountainOfBodies, Artwork.mountainOfBodies_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Aleph);
        }
    }
}