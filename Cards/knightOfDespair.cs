using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_KnightOfDespair_O0173()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Piercing.ability
            };

            CardHelper.CreateCard(
                "wstl_knightOfDespair", "The Knight of Despair",
                "",
                2, 4, 1, 0,
                Artwork.knightOfDespair, Artwork.knightOfDespair_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Rare, metaType: CardHelper.MetaType.NonChoice);
        }
    }
}