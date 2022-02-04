using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SilentOrchestra_T0131()
        {
            List<Ability> abilities = new List<Ability>
            {
                Conductor.ability
            };

            WstlUtils.Add(
                "wstl_silentOrchestra", "The Silent Orchestra",
                "A conductor of the apocalypse.",
                5, 2, 3, 0,
                Resources.silentOrchestra,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.silentOrchestra_emission,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}