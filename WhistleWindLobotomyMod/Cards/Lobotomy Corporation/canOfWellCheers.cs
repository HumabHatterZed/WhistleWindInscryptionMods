using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// <c>(1/1) (<see cref="Submerge"/>, <see cref="Strafe"/>) (1 Blood)</c>
        /// </summary>
        public const string canOfWellCheers = "wstl_canOfWellCheers";
        /// <summary>
        /// <c>(2/1) (<see cref="Brittle"/>, <see cref="IceCube"/>) (5 Bones)</c>
        /// </summary>
        public const string skeletonShrimp = "wstl_SKELETON_SHRIMP";
        /// <summary>
        /// <c>(0/1) (Terrain)</c>
        /// </summary>
        public const string crumpledCan = "wstl_CRUMPLED_CAN";
        private static void CanOfWellCheers_F0552() {
            string textureName = "skeleton_can";
            string textureName2 = "skeleton_shrimp";
            string textureName3 = "canOfWellCheers";
            CardInfo can = CardManager.New(LobotomyPlugin.pluginPrefix, crumpledCan, "Crumpled Can of WellCheers",
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetTerrain()
                .AddTraits(AbnormalPlugin.SodaLover)
                .SetDefaultEvolutionName("Still Crumpled Can of WellCheers")
                .Build();

            CardInfo skeleton = CardManager.New(LobotomyPlugin.pluginPrefix, skeletonShrimp, "Skeleton Shrimp",
                attack: 2, health: 1)
                .SetBonesCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.IceCube, Ability.Brittle)
                .AddTraits(AbnormalPlugin.SodaLover)
                .SetIceCube(can)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, canOfWellCheers, "Opened Can of WellCheers",
                attack: 1, health: 1, "A vending machine dispensing ocean soda.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Ability.Strafe, Ability.Submerge)
                .AddTraits(AbnormalPlugin.SodaLover)
                .AddTribes(TribeMechanical)
                .SetIceCube(skeleton)
                .SetGiftGiverId(Cards.skeletonShrimp)
                .SetDefaultEvolutionName("Opened Can of Elder WellCheers")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}