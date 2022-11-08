using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Theresia_T0909()
        {
            List<Ability> abilities = new()
            {
                Healer.ability
            };
            CardHelper.CreateCard(
                "wstl_theresia", "Theresia",
                "An old music box. It plays a familiar melody.",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 3,
                Artwork.theresia, Artwork.theresia_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}