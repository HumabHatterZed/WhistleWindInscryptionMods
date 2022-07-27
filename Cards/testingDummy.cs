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
                QuickDraw.ability,
                Ability.Deathtouch
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            List<Tribe> tribes = new()
            {
                Tribe.Squirrel
            };
            CardHelper.CreateCard(
                "wstl_testingDummy", "Standard Testing-Dummy Rabbit",
                "You shouldn't see this.",
                1, 1, 0, 0,
                Resources.testingDummy, Resources.blueStar_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}