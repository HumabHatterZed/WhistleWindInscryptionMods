using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SingingMachine_O0530()
        {
            List<Ability> abilities = new List<Ability>
            {
                TeamLeader.ability,
                Aggravating.ability
            };

            WstlUtils.Add(
                "wstl_singingMachine", "Singing Machine",
                "A wind-up music machine. The song it plays is to die for.",
                5, 0, 2, 0,
                Resources.singingMachine,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.singingMachine_emission);
        }
    }
}