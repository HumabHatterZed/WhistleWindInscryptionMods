using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TodaysShyLookAngry_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysShyLook.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_todaysShyLookAngry", "Today's Angry Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                2, 1, 1, 0,
                Resources.todaysShyLookAngry, Resources.todaysShyLookAngry_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 2);
        }
    }
}