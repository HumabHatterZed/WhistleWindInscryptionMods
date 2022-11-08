using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_QueenOfHatredTired_O0104()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility,
                LoveAndHate.specialAbility
            };
            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };
            CardHelper.CreateCard(
                "wstl_queenOfHatredTired", "The Queen of Hatred",
                "Exhaustion: the cost of an all-out attack.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.queenOfHatredTired, Artwork.queenOfHatredTired_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}