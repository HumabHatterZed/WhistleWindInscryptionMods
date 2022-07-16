using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TodaysShyLookHappy_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysShyLook.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_todaysShyLookHappy", "Today's Happy Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                1, 3, 1, 0,
                Resources.todaysShyLookHappy, Resources.todaysShyLookHappy_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 2);
        }
    }
}