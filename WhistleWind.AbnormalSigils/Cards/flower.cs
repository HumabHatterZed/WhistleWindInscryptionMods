using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_Flower()
        {
            const string flower = "flower";

            CardManager.New(pluginPrefix, flower, "Flower", 0, 1)
                .SetPortraits(Assembly, flower)
                .AddTribes(TribeBotanic)
                .AddTraits(BloomingFlower)
                .SetBoneless()
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);
        }
    }
}