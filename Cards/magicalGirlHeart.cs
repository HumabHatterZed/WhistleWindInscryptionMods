using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MagicalGirlHeart_O0104()
        {
            List<Ability> abilities = new()
            {
                OneSided.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                LoveAndHate.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_magicalGirlHeart", "Magical Girl",
                "A hero of love and justice. She will aid you on your journey.",
                2, 2, 1, 0,
                Artwork.magicalGirlHeart, Artwork.magicalGirlHeart_emission, pixelTexture: Artwork.magicalGirlHeart_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}