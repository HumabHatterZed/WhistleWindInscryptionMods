using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TodaysShyLookHappy_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysExpression.specialAbility
            };

            LobotomyCardHelper.CreateCard(
                "wstl_todaysShyLookHappy", "Today's Happy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 1, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLookHappy, Artwork.todaysShyLookHappy_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}