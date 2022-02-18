using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void CENSORED_O0389()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };

            WstlUtils.Add(
                "wstl_censored", "CENSORED",
                "It's best you never learn what it looks like.",
                6, 2, 4, 0,
                Resources.censored,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.censored_emission,
                //titleTexture: Resources.censored_title,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}