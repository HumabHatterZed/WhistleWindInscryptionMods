using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheRoadHome_F01114()
        {
            const string theRoadHome = "theRoadHome";

            CardInfo theRoadHomeCard = NewCard(
                theRoadHome,
                "The Road Home",
                "A young girl on a quest to return home with her friends.",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(theRoadHome)
                .AddAbilities(YellowBrickRoad.ability)
                .AddSpecialAbilities(TheHomingInstinct.specialAbility, YellowBrick.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(TraitEmeraldCity)
                .SetOnePerDeck();

            CreateCard(theRoadHomeCard, CardHelper.ChoiceType.Common, RiskLevel.He, ModCardType.Ruina);
        }
    }
}