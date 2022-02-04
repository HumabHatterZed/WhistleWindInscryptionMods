using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void LuminousBracelet_O0995()
        {
            List<Ability> abilities = new()
            {
                Regenerator.ability
            };

            WstlUtils.Add(
                "wstl_luminousBracelet", "Luminous Bracelet",
                "A bracelet that will heal those nearby. It does not forgive the greedy.",
                1, 0, 0, 5,
                Resources.luminousBracelet,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}