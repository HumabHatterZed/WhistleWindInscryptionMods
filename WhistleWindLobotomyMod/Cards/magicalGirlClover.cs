using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlClover_O01111()
        {
            List<Tribe> tribes = new() { TribeFae, Tribe.Reptile };
            List<Trait> traits = new() { TraitMagicalGirl };

            CreateCard(
                "wstl_servantOfWrath", "Servant of Wrath",
                "",
                atk: 3, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.servantOfWrath, Artwork.servantOfWrath_emission, Artwork.servantOfWrath_pixel,
                abilities: new() { Ability.DoubleStrike }, specialAbilities: new() { BlindRage.specialAbility },
                metaCategories: new(), tribes: tribes, traits: traits, onePerDeck: true);

            CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                atk: 2, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.magicalGirlClover, Artwork.magicalGirlClover_emission, pixelTexture: Artwork.magicalGirlClover_pixel,
                abilities: new() { Scorching.ability }, specialAbilities: new() { SwordWithTears.specialAbility },
                metaCategories: new(), tribes: new() { TribeFae }, traits: traits, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                modTypes: ModCardType.Ruina);
        }
    }
}