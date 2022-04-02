using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BloodBath1_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                BloodBath.specialAbility
            };

            WstlUtils.Add(
                "wstl_bloodBath1", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 3, 1, 0,
                Resources.bloodBath1, Resources.bloodBath1_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}