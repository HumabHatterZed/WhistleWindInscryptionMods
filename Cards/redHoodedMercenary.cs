using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void RedHoodedMercenary_F0157()
        {
            List<Ability> abilities = new()
            {
                Hunter.ability,
                BitterEnemies.ability
            };

            WstlUtils.Add(
                "wstl_redHoodedMercenary", "Little Red Riding Hooded Mercenary",
                "A skilled mercenary with a bloody vendetta. Perhaps you'll help her sate it.",
                2, 3, 2, 0,
                Resources.redHoodedMercenary,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.redHoodedMercenary_emission);
        }
    }
}