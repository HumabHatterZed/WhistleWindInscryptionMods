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
        private void Card_TheFirebird_O02101()
        {
            const string theFirebird = "theFirebird";

            CardInfo theFirebirdCard = NewCard(
                theFirebird,
                "The Firebird",
                "A bird that longs for the thrill of being hunted.",
                attack: 2, health: 3, blood: 2)
                .SetPortraits(theFirebird)
                .AddAbilities(Scorching.ability, Ability.Flying)
                .AddTribes(Tribe.Bird)
                .SetEvolveInfo("[name]The Grand Firebird");

            CreateCard(theFirebirdCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}