using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheLittlePrince_O0466()
        {
            List<Ability> abilities = new()
            {
                Spores.ability
            };

            WstlUtils.Add(
                "wstl_theLittlePrince", "The Little Prince",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                1, 4, 2, 0,
                Resources.theLittlePrince, Resources.theLittlePrince_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}