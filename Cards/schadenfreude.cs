using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Schadenfreude_O0576()
        {
            List<Ability> abilities = new()
            {
                QuickDraw.ability,
                Ability.Deathtouch
            };

            WstlUtils.Add(
                "wstl_schadenfreude", "SchadenFreude",
                "A strange machine. You can feel someone's persistent gaze through the keyhole.",
                0, 1, 0, 4,
                Resources.schadenfreude,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}