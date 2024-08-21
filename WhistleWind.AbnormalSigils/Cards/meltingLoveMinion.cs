using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_MeltingLoveMinion_D03109()
        {
            const string meltingLoveMinion = "meltingLoveMinion";
            CardManager.New(pluginPrefix, meltingLoveMinion, "Slime", 0, 2)
                .SetBonesCost(3)
                .SetPortraits(Assembly, meltingLoveMinion)
                .SetStatIcon(SlimeIcon.Icon)
                .AddAbilities(Slime.ability)
                .AddTraits(LovingSlime);
        }
    }
}
