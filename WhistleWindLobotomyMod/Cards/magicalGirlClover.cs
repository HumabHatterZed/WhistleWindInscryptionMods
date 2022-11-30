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
        private void Card_MagicalGirlClover_O01111()
        {
            List<Ability> abilities = new()
            {
                Burning.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            LobotomyCardHelper.CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlClover, Artwork.magicalGirlClover_emission, pixelTexture: Artwork.magicalGirlClover_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                modTypes: LobotomyCardHelper.ModCardType.Ruina);
        }
    }
}