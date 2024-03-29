﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_VoidDream_T0299()
        {
            const string dreamName = "Void Dream";
            const string voidDream = "voidDream";
            const string voidDreamRooster = "voidDreamRooster";

            CardInfo voidDreamRoosterCard = NewCard(
                voidDreamRooster,
                dreamName,
                attack: 2, health: 3, blood: 2)
                .SetPortraits(voidDreamRooster)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(Tribe.Hooved, Tribe.Bird)
                .Build();

            NewCard(voidDream, dreamName, "A sleeping goat. Or is it a sheep?",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(voidDream)
                .AddAbilities(Ability.Flying, Ability.Evolve)
                .AddTribes(Tribe.Hooved)
                .SetEvolve(voidDreamRoosterCard, 1)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}