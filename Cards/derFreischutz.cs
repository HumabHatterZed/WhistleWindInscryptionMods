using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_DerFreischutz_F0169()
        {
            List<Ability> abilities = new()
            {
                Marksman.ability,
                Ability.SplitStrike
            };

            CardHelper.CreateCard(
                "wstl_derFreischutz", "Der Freischütz",
                "A friendly hunter to some, a cruel gunsman to others. His bullets always hit their mark.",
                1, 1, 2, 0,
                Artwork.derFreischutz, Artwork.derFreischutz_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}