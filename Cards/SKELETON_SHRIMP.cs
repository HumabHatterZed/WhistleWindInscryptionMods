using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                2, 1, 0, 0,
                Artwork.skeleton_shrimp, Artwork.skeleton_shrimp_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                iceCubeName: "wstl_CRUMPLED_CAN");
        }
    }
}