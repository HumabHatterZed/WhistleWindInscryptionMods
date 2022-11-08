using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TodaysShyLookNeutral_O0192()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopyOnDeath
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysExpression.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_todaysShyLookNeutral", "Today's Shy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLook, Artwork.todaysShyLook_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}