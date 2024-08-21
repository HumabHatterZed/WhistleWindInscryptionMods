using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowWhitesVine_F0442()
        {
            const string snowWhitesVine = "snowWhitesVine";
            CardManager.New(pluginPrefix, snowWhitesVine, "Thorny Vines", 0, 1)
                .SetPortraits(Assembly, snowWhitesVine)
                .AddTribes(TribeBotanic)
                .SetTerrain();
        }
    }
}