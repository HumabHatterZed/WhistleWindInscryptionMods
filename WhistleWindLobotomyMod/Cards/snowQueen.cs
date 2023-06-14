using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowQueen_F0137()
        {
            const string snowQueen = "snowQueen";

            CardInfo snowQueenCard = NewCard(
                snowQueen,
                "The Snow Queen",
                "A queen from far away. Those who enter her palace never leave.",
                attack: 1, health: 2, bones: 5)
                .SetPortraits(snowQueen)
                .AddAbilities(FrostRuler.ability)
                .AddTribes(TribeFae)
                .SetEvolveInfo("[name]The Snow Empress");

            CreateCard(snowQueenCard, CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}