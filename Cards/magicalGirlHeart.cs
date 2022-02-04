using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MagicalGirlHeart_O0104()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
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
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}