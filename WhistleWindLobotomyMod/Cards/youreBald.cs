﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YoureBald_BaldIsAwesome()
        {
            const string youreBald = "youreBald";

            NewCard(youreBald, "You're Bald...", "I've always wondered what it's like to be bald.",
                attack: 0, health: 2, energy: 2)
                .SetPortraits(youreBald)
                .AddAbilities(Ability.DrawCopy)
                .SetDefaultEvolutionName("You're Extra Bald...")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}