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
        private void Card_MagicalGirlHeart_O0104()
        {
            List<Ability> abilities = new()
            {
                OneSided.ability
            };
            List<Ability> abilities2 = new()
            {
                Piercing.ability,
                Ability.Flying
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                LoveAndHate.specialAbility
            };
            List<SpecialTriggeredAbility> specialAbilities2 = new()
            {
                CustomEvolveHelper.specialAbility,
                LoveAndHate.specialAbility
            };
            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };
            LobotomyCardHelper.CreateCard(
                "wstl_queenOfHatred", "The Queen of Hatred",
                "Heroes exist to fight evil. In its absence, they must create it.",
                atk: 6, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.queenOfHatred, Artwork.queenOfHatred_emission,
                abilities: abilities2, specialAbilities: specialAbilities2,
                metaCategories: new(), tribes: tribes, traits: new());
            LobotomyCardHelper.CreateCard(
                "wstl_queenOfHatredTired", "The Queen of Hatred",
                "Exhaustion: the cost of an all-out attack.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.queenOfHatredTired, Artwork.queenOfHatredTired_emission,
                abilities: new(), specialAbilities: specialAbilities2,
                metaCategories: new(), tribes: tribes, traits: new());
            LobotomyCardHelper.CreateCard(
                "wstl_magicalGirlHeart", "Magical Girl",
                "A hero of love and justice. She will aid you on your journey.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlHeart, Artwork.magicalGirlHeart_emission, pixelTexture: Artwork.magicalGirlHeart_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                customTribe: TribeFae);
        }
    }
}