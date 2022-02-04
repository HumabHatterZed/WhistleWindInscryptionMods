using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Schadenfreude_O0576()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Sentry,
                Ability.Deathtouch
            };

            WstlUtils.Add(
                "wstl_schadenfreude", "SchadenFreude",
                "A strange machine. You can feel someone's persistent gaze through the keyhole.",
                1, 0, 0, 4,
                Resources.schadenfreude,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}