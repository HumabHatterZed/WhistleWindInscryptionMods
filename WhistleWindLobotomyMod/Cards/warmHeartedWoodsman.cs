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
        private void Card_WarmHeartedWoodsman_F0532()
        {
            const string warmHeartedWoodsman = "warmHeartedWoodsman";

            CardInfo warmHeartedWoodsmanCard = NewCard(
                warmHeartedWoodsman,
                "Warm-Hearted Woodsman",
                "A tin woodsman on the search for a heart. Perhaps you can give him yours.",
                attack: 2, health: 3, blood: 2)
                .SetPortraits(warmHeartedWoodsman)
                .AddAbilities(Woodcutter.ability)
                .AddTribes(TribeMechanical)
                .AddTraits(TraitEmeraldCity)
                .SetOnePerDeck();

            CreateCard(warmHeartedWoodsmanCard, CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}