using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            CardInfo can = NewCard(
                "CRUMPLED_CAN", "Crumpled Can of WellCheers",
                attack: 0, health: 1)
                .SetPortraits("skeleton_can")
                .SetTerrain()
                .SetDefaultEvolutionName("Rusted Can of WellCheers")
                .Build();

            CardInfo skeleton = NewCard(
                "SKELETON_SHRIMP", "Skeleton Shrimp",
                attack: 2, health: 1)
                .SetPortraits("skeleton_shrimp")
                .AddAbilities(Ability.Brittle, Ability.IceCube)
                .SetIceCube(can)
                .Build();

            NewCard(canOfWellCheers, "Opened Can of WellCheers", "A vending machine dispensing ocean soda.",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(canOfWellCheers)
                .AddAbilities(Ability.Strafe, Ability.Submerge)
                .AddTribes(TribeMechanical)
                .SetIceCube(skeleton)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}