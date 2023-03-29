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
        private void Card_MagicalGirlHeart_O0104()
        {
            List<Ability> abilities = new()
            {
                OneSided.ability,
                Piercing.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility,
                LoveAndHate.specialAbility,
                MagicalGirls.specialAbility
            };
            List<Tribe> tribes = new() { TribeFae, Tribe.Reptile };

            CreateCard(
                "wstl_queenOfHatred", "The Queen of Hatred",
                "Heroes exist to fight evil. In its absence, they must create it.",
                atk: 8, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.queenOfHatred, Artwork.queenOfHatred_emission, Artwork.queenOfHatred_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new());
            CreateCard(
                "wstl_queenOfHatredTired", "The Queen of Hatred",
                "Exhaustion: the cost of an all-out attack.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.queenOfHatredTired, Artwork.queenOfHatredTired_emission, Artwork.queenOfHatredTired_pixel,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new());

            abilities.Remove(Piercing.ability);
            specialAbilities.Remove(CustomEvolveHelper.specialAbility);
            tribes = new() { TribeFae };

            CreateCard(
                "wstl_magicalGirlHeart", "Magical Girl",
                "A hero of love and justice. She will aid you on your journey.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlHeart, Artwork.magicalGirlHeart_emission, pixelTexture: Artwork.magicalGirlHeart_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}