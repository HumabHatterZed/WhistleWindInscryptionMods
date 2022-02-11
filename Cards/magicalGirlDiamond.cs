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
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                _AbilityDialogueHelper.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_magicalGirlDiamond", "Magical Girl",
                "Greed hardened into golden amber, trapping the happiness inside.",
                3, 0, 2, 0,
                Resources.magicalGirlDiamond,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                evolveId: new EvolveIdentifier("wstl_kingOfGreed", 1), onePerDeck: true);
        }
    }
}