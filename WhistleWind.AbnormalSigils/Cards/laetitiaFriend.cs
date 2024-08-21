using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_LaetitiaFriend_O0167()
        {
            const string laetitiaFriend = "laetitiaFriend";

            CardManager.New(pluginPrefix, laetitiaFriend, "Wee Witch's Friend", 2, 2)
                .SetBonesCost(4)
                .SetPortraits(Assembly, laetitiaFriend)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("Wee Witch's Big Friend");
        }
    }
}