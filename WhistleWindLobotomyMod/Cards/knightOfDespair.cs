using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 2, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.knightOfDespair, Artwork.knightOfDespair_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                cardType: CardHelper.CardType.Rare, metaTypes: CardHelper.MetaType.NonChoice);
        }
    }
}