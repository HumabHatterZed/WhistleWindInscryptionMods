using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheNakedWorm_O0274()
        {
            const string theNakedWorm = "theNakedWorm";

            CardInfo theNakedWormCard = NewCard(
                theNakedWorm,
                "Naked Worm",
                "It can enter your body through any aperture.",
                attack: 1, health: 1)
                .SetPortraits(theNakedWorm)
                .AddTribes(Tribe.Insect)
                .AddTraits(NakedSerpent);

            CreateCard(theNakedWormCard);
        }
    }
}