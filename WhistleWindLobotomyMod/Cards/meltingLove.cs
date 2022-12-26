using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MeltingLove_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            CardHelper.CreateCard(
                "wstl_meltingLove", "Melting Love",
                "Don't let your beasts get too close now.",
                4, 2, 3, 0,
                Resources.meltingLove, Resources.meltingLove_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                isRare: true, isDonator: true, riskLevel: 5);
        }
    }
}