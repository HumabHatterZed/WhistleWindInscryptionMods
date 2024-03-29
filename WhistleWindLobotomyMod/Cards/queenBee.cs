﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_QueenBee_T0450()
        {
            const string queenBee = "queenBee";

            NewCard(queenBee, "Queen Bee", "A monstrous amalgam of a hive and a bee.",
                attack: 0, health: 4, blood: 2)
                .SetPortraits(queenBee)
                .AddAbilities(QueenNest.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}