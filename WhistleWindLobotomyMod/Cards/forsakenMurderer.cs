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
        private void Card_ForsakenMurderer_T0154()
        {
            const string forsakenMurderer = "forsakenMurderer";

            CardInfo forsakenMurdererCard = NewCard(
                forsakenMurderer,
                "Forsaken Murderer",
                "Experimented on then forgotten. What was anger has become abhorrence.",
                attack: 4, health: 1, bones: 8)
                .SetPortraits(forsakenMurderer)
                .AddTribes(TribeAnthropoid);

            CreateCard(forsakenMurdererCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}