using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_QueenBeeWorker_T0450()
        {
            const string queenBeeWorker = "queenBeeWorker";
            CardManager.New(pluginPrefix, queenBeeWorker, "Worker Bee", 1, 1)
                .SetBonesCost(1)
                .SetPortraits(Assembly, queenBeeWorker)
                .AddTribes(Tribe.Insect);
        }
    }
}