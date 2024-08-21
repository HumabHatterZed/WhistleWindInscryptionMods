using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_TheLittlePrinceMinion_O0466()
        {
            const string theLittlePrinceMinion = "theLittlePrinceMinion";
            CardManager.New(pluginPrefix, theLittlePrinceMinion, "Spore Mold Beast", 0, 0)
                .SetPortraits(Assembly, theLittlePrinceMinion)
                .AddTribes(TribeBotanic)
                .AddTraits(SporeFriend);
        }
    }
}