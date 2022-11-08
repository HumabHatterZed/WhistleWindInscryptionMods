using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DontTouchMe_O0547()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability,
                Ability.WhackAMole
            };
            CardHelper.CreateCard(
                "wstl_dontTouchMe", "Don't Touch Me",
                "Don't touch it.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 5,
                Artwork.dontTouchMe, Artwork.dontTouchMe_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic,
                riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}