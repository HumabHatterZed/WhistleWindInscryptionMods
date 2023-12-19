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
            CreateCard(MakeCard(
                parasiteTreeSapling,
                "Sapling",
                attack: 0, health: 2, bones: 1)
                .SetPortraits(parasiteTreeSapling)
                .AddAbilities(Ability.BoneDigger)
                .AddTribes(TribeBotanic)
                .SetTerrain());
        }
    }
}