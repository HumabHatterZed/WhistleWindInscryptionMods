using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

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
            LobotomyCardHelper.CreateCard(
                "wstl_knightOfDespair", "The Knight of Despair",
                "",
                atk: 2, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.knightOfDespair, Artwork.knightOfDespair_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice);
        }
    }
}