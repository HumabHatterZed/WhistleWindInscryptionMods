using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BlueStar2_O0393()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability,
                Ability.AllStrike
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                Forced.appearance
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.NonChoice
            };

            CardHelper.CreateCard(
                "wstl_blueStar2", "Blue Star",
                "When this is over, let's meet again as stars.",
                2, 6, 4, 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances,
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}