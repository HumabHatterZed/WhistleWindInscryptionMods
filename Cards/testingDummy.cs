using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TestingDummy_XXXXX()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood
            };

            WstlUtils.Add(
                "wstl_testingDummy", "Standard Testing-Dummy Rabbit",
                "You shouldn't see this.",
                1, 10, 0, 0,
                Resources.testingDummy,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), evolveId: new EvolveIdentifier("wstl_trainingDummy", 1));
        }
    }
}