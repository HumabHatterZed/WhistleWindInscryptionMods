using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void RedHoodedMercenary_F0157()
        {
            List <Ability> abilities = new List<Ability>
            {
                Ability.Sniper,
                BitterEnemies.ability
            };

            WstlUtils.Add(
                "wstl_redHoodedMercenary", "Little Red Riding Hooded Mercenary",
                "A skilled mercenary. She'll help you on your journey if you help her find the Wolf.",
                3, 2, 2, 0,
                Resources.redHoodedMercenary,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}