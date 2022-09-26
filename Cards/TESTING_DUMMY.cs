using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
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
                Test.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {

            };
            List<Tribe> tribes = new()
            {
                Tribe.Squirrel
            };
            CardHelper.CreateCard(
                "wstl_testingDummy", "Standard Testing-Dummy Rabbit",
                "You shouldn't see this.",
                0, 10, 0, 0,
                Resources.testingDummy, Resources.blueStar_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
        }
    }
}
