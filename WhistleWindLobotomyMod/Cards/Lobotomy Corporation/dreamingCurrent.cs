using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DreamingCurrent_T0271()
        {
            const string dreamingCurrent = "dreamingCurrent";

            CardManager.New(pluginPrefix, dreamingCurrent, "The Dreaming Current",
                attack: 4, health: 2, "A sickly child that was fed candy that let it see the ocean.")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, dreamingCurrent)
                .AddAbilities(Ability.Submerge, Ability.StrafeSwap)
                .SetDefaultEvolutionName("The Elder Dreaming Current")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}