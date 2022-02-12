using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BloodBath1_T0551()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.TripleBlood
            };

            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                BloodBath.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_bloodBath1", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                3, 0, 1, 0,
                Resources.bloodBath1,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), emissionTexture: Resources.bloodBath1_emission);
        }
    }
}