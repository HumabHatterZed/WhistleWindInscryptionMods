using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
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

            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                CENSORED.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_censored", "CENSORED",
                "It's best you never learn what it looks like.",
                2, 6, 4, 0,
                Resources.censored,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.censored_emission,
                titleTexture: Resources.censored_title,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}