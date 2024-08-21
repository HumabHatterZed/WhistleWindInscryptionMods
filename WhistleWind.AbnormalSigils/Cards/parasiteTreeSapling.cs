using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_ParasiteTreeSapling_D04108()
        {
            const string parasiteTreeSapling = "parasiteTreeSapling";
            CardManager.New(pluginPrefix, parasiteTreeSapling, "Sapling", 0, 2)
                .SetBonesCost(1)
                .SetPortraits(Assembly, parasiteTreeSapling)
                .AddAbilities(Ability.BoneDigger)
                .AddTribes(TribeBotanic)
                .SetTerrain();
        }
    }
}