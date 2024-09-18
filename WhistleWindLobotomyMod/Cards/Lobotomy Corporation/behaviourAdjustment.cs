using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BehaviourAdjustment_O0996()
        {
            const string behaviourAdjustment = "behaviourAdjustment";

            CardManager.New(pluginPrefix, behaviourAdjustment, "Behaviour Adjustment",
                attack: 0, health: 1, "A device that corrects errant beasts, though not always how you expect.")
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, behaviourAdjustment)
                .AddAbilities(Corrector.ability)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}