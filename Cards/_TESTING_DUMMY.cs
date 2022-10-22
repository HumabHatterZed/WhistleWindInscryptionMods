using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TestingDummy_XXXXX()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood,
                Ability.Sentry
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                //  EventBackground.appearance
            };
            List<Tribe> tribes = new()
            {
                Tribe.Squirrel
            };

            CardHelper.CreateCard(
                "wstl_testingDummy", "Standard Testing-Dummy Rabbit",
                "You shouldn't see this.",
                5, 10, 0, 0,
                Artwork.testingDummy, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(), appearances: appearances);
        }
    }
}
