using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_QueenBeeWorker_T0450()
        {
            const string queenBeeWorker = "queenBeeWorker";
            CreateCard(MakeCard(
                queenBeeWorker,
                "Worker Bee",
                attack: 1, health: 1, bones: 1)
                .SetPortraits(queenBeeWorker)
                .AddTribes(Tribe.Insect));
        }
    }
}