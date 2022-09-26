using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ScorchedGirl_F0102()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability
            };
            CardHelper.CreateCard(
                "wstl_scorchedGirl", "Scorched Girl",
                "Though there's nothing left to burn, the fire won't go out.",
                1, 1, 0, 3,
                Resources.scorchedGirl, Resources.scorchedGirl_emission, gbcTexture: Resources.scorchedGirl_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}