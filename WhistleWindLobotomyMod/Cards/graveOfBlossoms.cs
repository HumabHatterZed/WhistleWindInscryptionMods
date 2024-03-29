﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_GraveOfBlossoms_O04100()
        {
            const string graveOfBlossoms = "graveOfBlossoms";

            NewCard(graveOfBlossoms, "Grave of Cherry Blossoms", "A blooming cherry tree. The more blood it has, the more beautiful it becomes.",
                attack: 0, health: 3, blood: 1)
                .SetPortraits(graveOfBlossoms)
                .AddAbilities(Bloodletter.ability)
                .AddTribes(TribeBotanic)
                .SetDefaultEvolutionName("Mass Grave of Cherry Blossoms")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}