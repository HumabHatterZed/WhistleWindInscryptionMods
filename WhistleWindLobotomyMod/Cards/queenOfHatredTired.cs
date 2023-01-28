using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void QueenOfHatredTired_O0104()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomFledgling.specialAbility,
                LoveAndHate.specialAbility
            };
            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };
            CardHelper.CreateCard(
                "wstl_queenOfHatredTired", "The Queen of Hatred",
                "Exhaustion: the cost of an all-out attack.",
                0, 2, 1, 0,
                Resources.queenOfHatredTired, Resources.queenOfHatredTired_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}