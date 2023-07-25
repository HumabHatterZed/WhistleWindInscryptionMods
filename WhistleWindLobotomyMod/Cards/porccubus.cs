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
        private void Card_Porccubus_O0298()
        {
            const string porccubus = "porccubus";

            NewCard(porccubus, "Porccubus", "A prick from one of its quills creates a deadly euphoria.",
                attack: 1, health: 1, bones: 5)
                .SetPortraits(porccubus)
                .AddAbilities(Ability.Deathtouch)
                .AddTribes(TribeBotanic)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}