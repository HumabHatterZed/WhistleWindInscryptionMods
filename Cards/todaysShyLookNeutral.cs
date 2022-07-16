using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TodaysShyLookNeutral_O0192()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopyOnDeath
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysShyLook.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_todaysShyLookNeutral", "Today's Shy Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                1, 2, 1, 0,
                Resources.todaysShyLook, Resources.todaysShyLook_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 2);
        }
    }
}