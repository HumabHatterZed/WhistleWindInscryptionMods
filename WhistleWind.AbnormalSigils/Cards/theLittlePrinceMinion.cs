using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

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