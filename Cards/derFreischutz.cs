using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void DerFreischutz_F0169()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Hunter.ability
            };

            WstlUtils.Add(
                "wstl_derFreischutz", "Der Freischütz",
                "A friendly hunter to some, a bloody gunsman to others. His bullets always hit their mark.",
                1, 1, 2, 0,
                Resources.derFreischutz,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}