using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void YoureBald_BaldIsAwesome()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopy
            };

            WstlUtils.Add(
                "wstl_youreBald", "You're Bald...",
                "I've always wondered what it was like to be bald.",
                1, 1, 0, 3,
                Resources.youreBald,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                onePerDeck: true);
        }
    }
}