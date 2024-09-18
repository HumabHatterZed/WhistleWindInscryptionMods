using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CanOfWellCheers_F0552()
        {
            const string canOfWellCheers = "canOfWellCheers";

            CardInfo can = CardManager.New(pluginPrefix, 
                "CRUMPLED_CAN", "Crumpled Can of WellCheers",
                attack: 0, health: 1)
                .SetPortraits(ModAssembly, "skeleton_can")
                .SetTerrain()
                .SetDefaultEvolutionName("Rusted Can of WellCheers")
                .Build();

            CardInfo skeleton = CardManager.New(pluginPrefix, 
                "SKELETON_SHRIMP", "Skeleton Shrimp",
                attack: 2, health: 1)
                .SetBonesCost(5)
                .SetPortraits(ModAssembly, "skeleton_shrimp")
                .AddAbilities(Ability.IceCube, Ability.Brittle)
                .SetIceCube(can)
                .Build();

            CardManager.New(pluginPrefix, canOfWellCheers, "Opened Can of WellCheers",
                attack: 1, health: 1, "A vending machine dispensing ocean soda.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, canOfWellCheers)
                .AddAbilities(Ability.Strafe, Ability.Submerge)
                .AddTribes(TribeMechanical)
                .SetIceCube(skeleton)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}