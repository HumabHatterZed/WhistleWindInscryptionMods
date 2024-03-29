﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BigBird_O0240()
        {
            const string bigBird = "bigBird";

            NewCard(bigBird, "Big Bird", "Its eyes light up the darkness like stars.",
                attack: 2, health: 4, blood: 2)
                .SetPortraits(bigBird)
                .AddAbilities(Cycler.ability)
                .AddSpecialAbilities(ThreeBirds.specialAbility)
                .AddTribes(Tribe.Bird)
                .AddTraits(BlackForest)
                .SetDefaultEvolutionName("Bigger Bird")
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}