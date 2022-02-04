using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThere_O0620()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                NothingThere.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_nothingThere", "Bullfrog",
                "A rare Bullfrog? How odd...",
                2, 1, 3, 0,
                Resources.nothingThere,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}