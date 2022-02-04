using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void DerFreischutz_F0169()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.SplitStrike,
                Ability.Sniper
            };

            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                DerFreischutz.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_derFreischutz", "Der Freischütz",
                "A friendly hunter to some, a bloody gunsman to others. His bullet always hits its mark.",
                1, 1, 3, 0,
                Resources.derFreischutz,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}