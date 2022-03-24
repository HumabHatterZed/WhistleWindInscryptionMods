using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void HeartOfAspiration_O0977()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours
            };

            WstlUtils.Add(
                "wstl_heartOfAspiration", "The Heart of Aspiration",
                "A heart without an owner.",
                1, 2, 1, 0,
                Resources.heartOfAspiration, Resources.heartOfAspiration_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}