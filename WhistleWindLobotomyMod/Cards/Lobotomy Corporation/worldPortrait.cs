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

            NewCard(worldPortrait, "Portrait of Another World", "This portrait captures a moment, one we're destined to lose.",
                attack: 0, health: 4, blood: 1)
                .SetPortraits(worldPortrait)
                .AddAbilities(Reflector.ability)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}