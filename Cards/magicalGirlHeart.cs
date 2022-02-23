using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MagicalGirlHeart_O0104()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                MagicalGirlHeart.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_magicalGirlHeart", "Magical Girl",
                "A hero of love and justice, she will aid you on your journey.",
                2, 2, 1, 0,
                Resources.magicalGirlHeart,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.magicalGirlHeart_emission,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}