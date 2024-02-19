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
            CreateCard(MakeCard(
                cardName: laetitiaFriend,
                "Little Witch's Friend",
                attack: 1, health: 2, bones: 3)
                .SetPortraits(laetitiaFriend)
                .AddTribes(AbnormalPlugin.TribeFae, Tribe.Insect)
                .SetDefaultEvolutionName("Little Witch's Big Friend"));
        }
    }
}