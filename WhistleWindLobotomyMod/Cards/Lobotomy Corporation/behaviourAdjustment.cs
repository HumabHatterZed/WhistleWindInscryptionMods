using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string behaviourAdjustment = "wstl_behaviourAdjustment";
        private static void BehaviourAdjustment_O0996()
        {
            string textureName = "behaviourAdjustment";
            CardManager.New(LobotomyPlugin.pluginPrefix, behaviourAdjustment, "Behaviour Adjustment",
                attack: 0, health: 1, "A device that corrects errant beasts, though not always how you expect.")
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Corrector.ability)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, "Behaviour Adjustment",
                attack: 0, health: 1, "A device that corrects errant beasts, though not always how you expect.")
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Corrector.ability)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}