using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new()
            {
                Ability.PreventAttack,
                Aggravating.ability
            };
            CardHelper.CreateCard(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "It makes itself the safest place in the world by altering the reality around it.",
                0, 1, 0, 3,
                Resources.shelterFrom27March, Resources.shelterFrom27March_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}