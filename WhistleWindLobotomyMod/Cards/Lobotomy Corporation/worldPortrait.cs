using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WorldPortrait_O0991()
        {
            const string worldPortrait = "worldPortrait";

            CardManager.New(pluginPrefix, worldPortrait, "Portrait of Another World",
                attack: 0, health: 4, "This portrait captures a moment, one we're destined to lose.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, worldPortrait)
                .AddAbilities(Reflector.ability)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}