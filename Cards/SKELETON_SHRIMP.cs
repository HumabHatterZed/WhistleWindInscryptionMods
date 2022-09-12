using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SkeletonShrimp_F0552()
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
                Resources.skeleton_shrimp, Resources.skeleton_shrimp_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, iceCubeName: "Snelk_Neck", riskLevel: 1);
        }
    }
}