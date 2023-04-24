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
        private void Card_MagicalGirlDiamond_O0164()
        {
            List<Ability> abilities = new() { Cycler.ability };
            List<Tribe> tribes = new() { TribeFae };
            List<Trait> traits = new() { TraitMagicalGirl };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                MagicalGirls.specialAbility
            };
            CreateCard(
                "wstl_kingOfGreed", "The King of Greed",
                "",
                atk: 2, hp: 5,
                blood: 1, bones: 0, energy: 0,
                Artwork.kingOfGreed, Artwork.kingOfGreed_emission, Artwork.kingOfGreed_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits, onePerDeck: true);

            specialAbilities.Add(CustomEvolveHelper.specialAbility);

            CreateCard(
                "wstl_magicalGirlDiamond", "Magical Girl",
                "A girl encased in hardened amber. Happiness trapped by greed.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlDiamond, Artwork.magicalGirlDiamond_emission, pixelTexture: Artwork.magicalGirlDiamond_pixel,
                abilities: new() { Ability.Evolve }, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw, evolveName: "wstl_kingOfGreed");
        }
    }
}