using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_MeltingLoveMinion_D03109()
        {
            const string meltingLoveMinion = "meltingLoveMinion";
            CreateCard(MakeCard(
                cardName: meltingLoveMinion,
                "Slime", attack: 0, health: 1)
                .SetPortraits(meltingLoveMinion)
                .SetStatIcon(SlimeIcon.Icon)
                .AddAbilities(Slime.ability)
                .AddTraits(LovingSlime));
        }
    }
}
