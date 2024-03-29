﻿using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheLittlePrince_O0466()
        {
            const string theLittlePrince = "theLittlePrince";

            NewCard(theLittlePrince, "The Little Prince", "A giant mushroom chunk. A mist of spores surrounds it.",
                attack: 1, health: 4, blood: 2)
                .SetPortraits(theLittlePrince)
                .AddAbilities(Sporogenic.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(SporeFriend)
                .SetDefaultEvolutionName("The Little King")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}