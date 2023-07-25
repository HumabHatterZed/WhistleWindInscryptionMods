using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

using WhistleWind.Core.Helpers;
using static DiskCardGame.CardAppearanceBehaviour;

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
                attack: 2, health: 2, bones: 2)
                .SetPortraits(laetitiaFriend)
                .AddTribes(Tribe.Insect)
                .SetDefaultEvolutionName("Little Witch's Big Friend"));
        }
    }
}