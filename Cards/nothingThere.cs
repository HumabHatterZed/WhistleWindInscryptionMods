using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThere_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Reach
            };
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                NothingThere.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_nothingThere", "Yumi",
                "A rare death card?",
                3, 2, 3, 0,
                Resources.nothingThere,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.nothingThere_emission,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}