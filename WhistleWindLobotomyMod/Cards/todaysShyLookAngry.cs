using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TodaysShyLookAngry_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysExpression.specialAbility
            };

            LobotomyCardHelper.CreateCard(
                "wstl_todaysShyLookAngry", "Today's Angry Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 2, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLookAngry, Artwork.todaysShyLookAngry_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}