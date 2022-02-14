using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SilentOrchestra_T0131()
        {
            List<Ability> abilities = new()
            {
                Conductor.ability
            };

            WstlUtils.Add(
                "wstl_silentOrchestra", "The Silent Orchestra",
                "A conductor of the apocalypse.",
                2, 5, 3, 0,
                Resources.silentOrchestra,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.silentOrchestra_emission,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}