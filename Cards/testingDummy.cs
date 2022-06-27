using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TestingDummy_XXXXX()
        {
            List<Ability> abilities = new()
            {
                TheTrain.ability
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                //NothingThere.specialAbility
            };

            WstlUtils.Add(
                "wstl_testingDummy", "Standard Testing-Dummy Rabbit",
                "You shouldn't see this.",
                1, 10, 0, 0,
                Resources.bigBird, Resources.bigBird_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}