using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlSpade_O0173()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Piercing.ability
            };
            List<Tribe> tribes = new() { TribeFae };

            List<SpecialTriggeredAbility> specialAbilties = new()
            {
                PinkTears.specialAbility
            };
            CreateCard(
                "wstl_knightOfDespair", "The Knight of Despair",
                "",
                atk: 2, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.knightOfDespair, Artwork.knightOfDespair_emission, Artwork.knightOfDespair_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: new(), onePerDeck: true);

            abilities = new() { Protector.ability };

            CreateCard(
                "wstl_magicalGirlSpade", "Magical Girl",
                "A loyal knight fighting to protect those close to her.",
                atk: 1, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.magicalGirlSpade, Artwork.magicalGirlSpade_emission, pixelTexture: Artwork.magicalGirlSpade_pixel,
                abilities: abilities, specialAbilities: specialAbilties,
                metaCategories: new(), tribes: tribes, traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}