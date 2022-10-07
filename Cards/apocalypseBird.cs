using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

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
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            List <CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };

            CardHelper.CreateCard(
                "wstl_apocalypseBird", "Apocalypse Bird",
                "There was no moon, no stars. Just a bird, alone in the Black Forest.",
                3, 12, 4, 0,
                Artwork.apocalypseBird, Artwork.apocalypseBird_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Rare, metaType: CardHelper.MetaType.Event);
        }
    }
}