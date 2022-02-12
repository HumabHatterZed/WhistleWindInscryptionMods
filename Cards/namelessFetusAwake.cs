using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NamelessFetusAwake_O0115()
        {
            List<Ability> abilities = new List<Ability>
            {
                Aggravating.ability,
                Ability.PreventAttack
            };

            WstlUtils.Add(
                "wstl_namelessFetusAwake", "Nameless Fetus",
                "Only a sacrifice will stop its piercing wails.",
                6, 0, 0, 5,
                Resources.namelessFetusAwake,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), emissionTexture: Resources.namelessFetusAwake_emission);
        }
    }
}