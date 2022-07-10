using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Yang_O07103()
        {
            List<Ability> abilities = new()
            {
                Regenerator.ability
            };
            WstlUtils.Add(
                "wstl_yang", "Yin",
                "The white carp swims towards [c:bR]chaos[c:].",
                0, 3, 1, 0,
                Resources.yang, Resources.yang_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true);
        }
    }
}