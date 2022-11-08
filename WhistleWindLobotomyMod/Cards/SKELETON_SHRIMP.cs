using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SkeletonShrimp_F0552()
        {
            List<Ability> abilities = new()
            {
                Ability.Brittle,
                Ability.IceCube
            };

            CardHelper.CreateCard(
                "wstl_SKELETON_SHRIMP", "Skeleton Shrimp",
                "A dead shrimp man craving for a final drop of soda.",
                atk: 2, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.skeleton_shrimp, Artwork.skeleton_shrimp_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                iceCubeName: "wstl_CRUMPLED_CAN");
        }
    }
}