using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MagicalGirlDiamond_O0164()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                SpecialEvolve.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_magicalGirlDiamond", "Magical Girl",
                "Greed hardened into golden amber, trapping the happiness inside.",
                3, 0, 2, 0,
                Resources.magicalGirlDiamond,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                onePerDeck: true);
        }
    }
}