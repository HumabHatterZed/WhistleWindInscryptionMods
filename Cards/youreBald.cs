using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void YoureBald_BaldIsAwesome()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DrawCopy,
                Ability.CorpseEater
            };

            WstlUtils.Add(
                "wstl_youreBald", "You're Bald...",
                "I've always wondered what it was like to be bald.",
                1, 1, 4, 0,
                Resources.youreBald,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}