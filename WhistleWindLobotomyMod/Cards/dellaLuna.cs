using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.CardHelper;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DellaLuna_D01105()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability
            };
            CardHelper.CreateCard(
                "wstl_dellaLuna", "Il Pianto della Luna",
                "In reality, man despairs at [c:bR]the moon[c:].",
                atk: 2, hp: 6,
                blood: 3, bones: 0, energy: 0,
                Artwork.dellaLuna, Artwork.dellaLuna_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: MetaType.Donator);
        }
    }
}