using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void PunishingBird_O0256()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Flying,
                Punisher.ability
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_punishingBird", "Punishing Bird",
                "A small bird on a mission to punish evildoers.",
                2, 1, 1, 0,
                Resources.punishingBird,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                onePerDeck: true);
        }
    }
}