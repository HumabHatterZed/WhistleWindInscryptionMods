using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApocalypseBird_O0263()
        {
            List<Ability> abilities = new()
            {
                Ability.AllStrike,
                Ability.SplitStrike
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                ThreeBirds.specialAbility
            };
            List<CardMetaCategory> metaCategories = new()
            {
                CardHelper.CANNOT_GIVE_SIGILS
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            List <CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance,
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };

            CardHelper.CreateCard(
                "wstl_apocalypseBird", "Apocalypse Bird",
                "There was no moon, no stars. Just a bird, alone in the Black Forest.",
                3, 12, 4, 0,
                Resources.apocalypseBird, Resources.apocalypseBird_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: metaCategories, tribes: tribes, traits: new(),
                appearances: appearances, onePerDeck: true);
        }
    }
}