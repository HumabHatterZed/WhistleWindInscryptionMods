﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BackwardClock_D09104()
        {
            const string backwardClock = "backwardClock";

            NewCard(backwardClock, "Backward Clock", "A clock to rewind your wasted time. Will you pay the toll?",
                attack: 0, health: 1, energy: 2, temple: CardTemple.Tech)
                .SetPortraits(backwardClock)
                .AddAbilities(TimeMachine.ability)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetTerrain()
                .SetNodeRestrictions(true, true, true, true)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Waw, ModCardType.Donator);
        }
    }
}