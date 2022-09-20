using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TodaysShyLook_O0192()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysShyLook.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_todaysShyLook", "Today's Shy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                1, 2, 1, 0,
                Resources.todaysShyLook, Resources.todaysShyLook_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}