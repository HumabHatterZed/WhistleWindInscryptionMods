using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BloodBath1_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood
            };

            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                BloodBath.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_bloodBath1", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 3, 1, 0,
                Resources.bloodBath1,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), emissionTexture: Resources.bloodBath1_emission);
        }
    }
}