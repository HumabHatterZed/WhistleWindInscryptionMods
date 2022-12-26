using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void GraveOfBlossoms_O04100()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Bloodfiend.ability
            };

            CardHelper.CreateCard(
                "wstl_graveOfBlossoms", "Grave of Cherry Blossoms",
                "A blooming cherry tree. The more blood it has, the more beautiful it becomes.",
                0, 3, 1, 0,
                Resources.graveOfBlossoms, Resources.graveOfBlossoms_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}