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
        private void Card_PunishingBird_O0256()
        {
            const string punishingBird = "punishingBird";

            CardInfo punishingBirdCard = NewCard(
                punishingBird,
                "Punishing Bird",
                "A small bird on a mission to punish evildoers.",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(punishingBird)
                .AddAbilities(Ability.Flying, Punisher.ability)
                .AddTribes(Tribe.Bird)
                .AddTraits(TraitBlackForest)
                .SetOnePerDeck();

            CreateCard(punishingBirdCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}