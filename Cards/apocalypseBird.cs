using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ApocalypseBird_O0263()
        {
            List<Ability> abilities = new()
            {
                Ability.TriStrike,
                Ability.AllStrike
            };

            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            List <CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };

            CardHelper.CreateCard(
                "wstl_apocalypseBird", "Apocalypse Bird",
                "There was no moon, no stars. Just a bird, alone in the Black Forest.",
                5, 3, 4, 0,
                Resources.apocalypseBird, Resources.apocalypseBird_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances, onePerDeck: true);
        }
    }
}