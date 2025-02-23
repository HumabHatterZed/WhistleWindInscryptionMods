using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string dreamingCurrent = "wstl_dreamingCurrent";
        private static void DreamingCurrent_T0271()
        {
            string textureName = "dreamingCurrent";
            CardManager.New(LobotomyPlugin.pluginPrefix, dreamingCurrent, "The Dreaming Current",
                attack: 4, health: 2, "A sickly child that was fed candy that let it see the ocean.")
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Submerge, Ability.StrafeSwap)
                .SetDefaultEvolutionName("The Elder Dreaming Current")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}