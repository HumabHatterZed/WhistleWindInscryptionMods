using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ozma_F04116()
        {
            const string ozma = "ozma";

            CardInfo ozmaCard = NewCard(
                ozma,
                "Ozma",
                "The former ruler of a far away land, now reduced to this.",
                attack: 1, health: 2, blood: 1)
                .SetPortraits(ozma)
                .AddAbilities(RightfulHeir.ability)
                .AddTribes(TribeFae)
                .AddTraits(TraitEmeraldCity)
                .SetOnePerDeck();

            CreateCard(ozmaCard, CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Ruina);
        }
    }
}