﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WillBeBadWolf_F0258()
        {
            const string willBeBadWolf = "willBeBadWolf";

            NewCard(willBeBadWolf, "Big and Will Be Bad Wolf", "It's the fate of all wolves to be the villains of fairy tales.",
                attack: 3, health: 3, blood: 3)
                .SetPortraits(willBeBadWolf)
                .AddAbilities(Assimilator.ability)
                .AddSpecialAbilities(CrimsonScar.specialAbility)
                .AddTribes(Tribe.Canine)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}