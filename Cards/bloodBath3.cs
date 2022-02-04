using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BloodBath3_T0551()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.TripleBlood,
                Ability.QuadrupleBones
            };

            WstlUtils.Add(
                "wstl_bloodBath3", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                3, 3, 0, 0,
                Resources.bloodBath3,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), emissionTexture: Resources.bloodBath3_emission);
        }
    }
}