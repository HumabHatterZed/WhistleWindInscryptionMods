using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DreamingCurrent_T0271()
        {
            const string dreamingCurrent = "dreamingCurrent";

            NewCard(dreamingCurrent, "The Dreaming Current", "A sickly child. Everyday it was fed candy that let it see the ocean.",
                attack: 4, health: 2, blood: 3)
                .SetPortraits(dreamingCurrent)
                .AddAbilities(Ability.Submerge, Ability.StrafeSwap)
                .SetDefaultEvolutionName("The Elder Dreaming Current")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}