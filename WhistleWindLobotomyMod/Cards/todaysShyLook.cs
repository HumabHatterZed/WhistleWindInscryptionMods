using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLook, Artwork.todaysShyLook_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}