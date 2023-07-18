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
        private void Card_TheNakedNest_O0274()
        {
            const string theNakedNest = "theNakedNest";

            CardInfo theNakedNestCard = NewCard(
                theNakedNest,
                "The Naked Nest",
                "They can enter your body through any aperture.",
                attack: 0, health: 3, bones: 4)
                .SetPortraits(theNakedNest)
                .AddAbilities(SerpentsNest.ability)
                .AddTraits(Trait.KillsSurvivors)
                .SetEvolveInfo("[name]The Elder Naked Nest");

            CreateCard(theNakedNestCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}