using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void DerFreischutz_F0169()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Marksman.ability
            };

            CardHelper.CreateCard(
                "wstl_derFreischutz", "Der Freischütz",
                "A friendly hunter to some, a bloody gunsman to others. His bullets always hit their mark.",
                1, 1, 2, 0,
                Resources.derFreischutz, Resources.derFreischutz_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true, riskLevel: 3);
        }
    }
}