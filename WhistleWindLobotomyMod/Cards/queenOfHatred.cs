using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_QueenOfHatred_O0104()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying
            };
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
                "wstl_queenOfHatred", "The Queen of Hatred",
                "Heroes exist to fight evil. In its absence, they must create it.",
                atk: 6, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.queenOfHatred, Artwork.queenOfHatred_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}