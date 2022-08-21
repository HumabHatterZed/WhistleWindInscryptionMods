using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BloodBath2_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                BloodBath.specialAbility,
                SpecialTriggeredAbility.SacrificesThisTurn
            };

            CardHelper.CreateCard(
                "wstl_bloodBath2", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 2, 2, 0,
                Resources.bloodBath2, Resources.bloodBath2_emission,
                abilities: abilities, specialAbilities: specialAbilities, statIcon: SpecialStatIcon.SacrificesThisTurn,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}