using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BloodBath2_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                WristCutter.specialAbility,
                SpecialTriggeredAbility.SacrificesThisTurn
            };

            CardHelper.CreateCard(
                "wstl_bloodBath2", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 2, 2, 0,
                Artwork.bloodBath2, Artwork.bloodBath2_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                statIcon: SpecialStatIcon.SacrificesThisTurn);
        }
    }
}