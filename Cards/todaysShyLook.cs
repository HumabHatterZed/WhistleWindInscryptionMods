using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TodaysShyLook_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysExpression.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_todaysShyLook", "Today's Shy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                1, 2, 1, 0,
                Artwork.todaysShyLook, Artwork.todaysShyLook_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}