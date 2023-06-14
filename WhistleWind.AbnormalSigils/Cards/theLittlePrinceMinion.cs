using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

using WhistleWind.Core.Helpers;
using static DiskCardGame.CardAppearanceBehaviour;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_TheLittlePrinceMinion_O0466()
        {
            const string theLittlePrinceMinion = "theLittlePrinceMinion";

            CreateCard(MakeCard(
                cardName: theLittlePrinceMinion,
                "Spore Mold Creature")
                .SetPortraits(theLittlePrinceMinion)
                .AddTribes(TribeBotanic)
                .AddTraits(SporeFriend));
        }
    }
}