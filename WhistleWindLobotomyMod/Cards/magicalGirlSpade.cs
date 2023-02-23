using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlSpade_O0173()
        {
            Tribe customTribe = TribeFae;
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Piercing.ability
            };
            List<Ability> abilities2 = new()
            {
                Protector.ability
            };
            List<SpecialTriggeredAbility> specialAbilties = new()
            {
                PinkTears.specialAbility
            };
            CreateCard(
                "wstl_knightOfDespair", "The Knight of Despair",
                "",
                atk: 2, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.knightOfDespair, Artwork.knightOfDespair_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                customTribe: customTribe);
            CreateCard(
                "wstl_magicalGirlSpade", "Magical Girl",
                "A loyal knight fighting to protect those close to her.",
                atk: 1, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlSpade, Artwork.magicalGirlSpade_emission, pixelTexture: Artwork.magicalGirlSpade_pixel,
                abilities: abilities2, specialAbilities: specialAbilties,
                metaCategories: new(), tribes: new(), traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Waw,
                customTribe: customTribe);
        }
    }
}