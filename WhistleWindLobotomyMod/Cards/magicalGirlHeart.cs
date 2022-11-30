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
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                LoveAndHate.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_magicalGirlHeart", "Magical Girl",
                "A hero of love and justice. She will aid you on your journey.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlHeart, Artwork.magicalGirlHeart_emission, pixelTexture: Artwork.magicalGirlHeart_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}