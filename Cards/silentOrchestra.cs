using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.silentOrchestra, Resources.silentOrchestra_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true);
        }
    }
}