using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using InscryptionAPI.Card;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ScorchedGirl_F0102()
        {
            const string scorchedGirl = "scorchedGirl";

            CardInfo scorchedGirlCard = NewCard(
                scorchedGirl,
                "Scorched Girl",
                "Though there's nothing left to burn, the fire won't go out.",
                attack: 1, health: 1, bones: 2)
                .SetPortraits(scorchedGirl)
                .AddAbilities(Volatile.ability)
                .AddTribes(TribeAnthropoid);

            CreateCard(scorchedGirlCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}