using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TodaysShyLookHappy_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysExpression.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_todaysShyLookHappy", "Today's Happy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                1, 3, 1, 0,
                Artwork.todaysShyLookHappy, Artwork.todaysShyLookHappy_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}