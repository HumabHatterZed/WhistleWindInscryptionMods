using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BloodBath3_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood,
                Ability.QuadrupleBones
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialTriggeredAbility.SacrificesThisTurn
            };
            CardHelper.CreateCard(
                "wstl_bloodBath3", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                1, 3, 3, 0,
                Resources.bloodBath3, Resources.bloodBath3_emission,
                abilities: abilities, specialAbilities: specialAbilities, statIcon: SpecialStatIcon.SacrificesThisTurn,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}