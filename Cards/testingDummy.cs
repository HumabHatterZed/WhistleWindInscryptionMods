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
                Idol.ability,
                BitterEnemies.ability,
                FlagBearer.ability,
                TeamLeader.ability
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                //NothingThere.specialAbility
            };

            WstlUtils.Add(
                "wstl_testingDummy", "Standard Testing-Dummy Rabbit",
                "You shouldn't see this.",
                1, 10, 0, 0,
                Resources.testingDummy, Resources.blueStar_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}