using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MirrorOfAdjustment_O0981()
        {
            const string mirrorOfAdjustment = "mirrorOfAdjustment";

            CardManager.New(pluginPrefix, mirrorOfAdjustment, "The Mirror of Adjustment",
                attack: 0, health: 1, "A mirror that reflects nothing on its surface.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, mirrorOfAdjustment)
                .AddAbilities(Woodcutter.ability)
                .SetStatIcon(SpecialStatIcon.Mirror)
                .SetTerrain(false)
                .SetDefaultEvolutionName("The Grand Mirror of Adjustment")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}