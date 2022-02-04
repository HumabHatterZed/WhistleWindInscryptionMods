using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void FairyFestival_F0483()
        {
            List<Ability> abilities = new List<Ability>
            {
                Bloodfiend.ability
            };

            WstlUtils.Add(
                "wstl_fairyFestival", "Fairy Festival",
                "Everything will be peaceful while you're under the fairies' care.",
                1, 1, 1, 0,
                Resources.fairyFestival,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}