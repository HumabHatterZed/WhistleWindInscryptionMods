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
        private void Card_MagicalGirlSpade_O0173()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Piercing.ability
            };
            List<Tribe> tribes = new() { TribeFae };
            List<Trait> traits = new() { TraitMagicalGirl };

            List<SpecialTriggeredAbility> specialAbilties = new()
            {
                SwordWithTears.specialAbility
            };
            CreateCard(
                "wstl_knightOfDespair", "The Knight of Despair",
                "",
                atk: 2, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.knightOfDespair, Artwork.knightOfDespair_emission, Artwork.knightOfDespair_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: new(), onePerDeck: true);

            abilities = new() { Protector.ability };

            CreateCard(
                "wstl_magicalGirlSpade", "The Knight of Despair",
                "A loyal knight fighting to protect those close to her.",
                atk: 1, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.magicalGirlSpade, Artwork.magicalGirlSpade_emission, pixelTexture: Artwork.magicalGirlSpade_pixel,
                abilities: abilities, specialAbilities: specialAbilties,
                metaCategories: new(), tribes: tribes, traits: traits, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}