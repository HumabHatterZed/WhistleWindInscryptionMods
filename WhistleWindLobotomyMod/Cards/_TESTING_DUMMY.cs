using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void TestingDummy_XXXXX()
        {
            List<Ability> abilities = new()
            {
                Nettles.ability
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
                atk: 2, hp: 10,
                blood: 0, bones: 0, energy: 0,
                Artwork.trainingDummy, Artwork.trainingDummy_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(), appearances: appearances);
        }
    }
}
