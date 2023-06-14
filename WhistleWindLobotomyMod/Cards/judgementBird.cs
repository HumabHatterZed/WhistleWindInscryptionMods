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
        private void Card_JudgementBird_O0262()
        {
            const string judgementBird = "judgementBird";

            CardInfo judgementBirdCard = NewCard(
                judgementBird,
                "Judgement Bird",
                "A long bird that judges sinners with swift efficiency. It alone is above consequences.",
                attack: 1, health: 1, blood: 2)
                .SetPortraits(judgementBird)
                .AddAbilities(Ability.Sniper)
                .AddTribes(Tribe.Bird)
                .AddTraits(TraitBlackForest, AbnormalPlugin.Executioner)
                .SetOnePerDeck();

            CreateCard(judgementBirdCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}