using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheNakedNest_O0274()
        {
            List<Ability> abilities = new()
            {
                SerpentsNest.ability
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            CardHelper.CreateCard(
                "wstl_theNakedNest", "The Naked Nest",
                "They can enter your body through any aperture.",
                0, 2, 0, 4,
                Resources.theNakedNest, Resources.theNakedNest_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                isChoice: true, riskLevel: 4);
        }
    }
}