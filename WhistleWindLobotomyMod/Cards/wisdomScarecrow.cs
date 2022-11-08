using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WisdomScarecrow_F0187()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };
            CardHelper.CreateCard(
                "wstl_wisdomScarecrow", "Scarecrow Searching for Wisdom",
                "A hollow-headed scarecrow. Blood soaks its straw limbs.",
                atk: 1, hp: 3,
                blood: 0, bones: 5, energy: 0,
                Artwork.wisdomScarecrow, Artwork.wisdomScarecrow_emission, pixelTexture: Artwork.wisdomScarecrow_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He); ;
        }
    }
}