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
        private void Card_BeautyAndBeast_O0244()
        {
            const string beautyAndBeast = "beautyAndBeast";

            CardInfo beast = NewCard(
                beautyAndBeast,
                "Beauty and the Beast",
                "A pitiable creature. Death would be a mercy for it.",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(beautyAndBeast)
                .AddAbilities(Cursed.ability)
                .AddTribes(Tribe.Hooved, Tribe.Insect);

            CreateCard(beast, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}