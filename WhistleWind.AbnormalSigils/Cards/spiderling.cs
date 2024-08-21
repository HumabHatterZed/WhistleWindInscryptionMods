using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_Spiderling_O0243()
        {
            const string spiderling = "spiderling";
            const string spiderBrood = "spiderBrood";
            Tribe[] tribes = new[] { Tribe.Insect };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { CardAppearanceBehaviour.Appearance.RedEmission };

            CardInfo brood = CardManager.New(pluginPrefix, spiderBrood, "Spider Brood", 1, 3)
                .SetBonesCost(3)
                .SetPortraits(Assembly, spiderBrood)
                .AddAppearances(appearances)
                .AddTribes(tribes)
                .SetDefaultEvolutionName("Spider Buff");

            CardManager.New(pluginPrefix, spiderling, "Spiderling", 0, 1)
                .SetBonesCost(3)
                .SetPortraits(Assembly, spiderling)
                .AddAppearances(appearances)
                .AddTribes(tribes)
                .SetEvolve(brood, 1)
                .SetAffectedByTidalLock();
        }
    }
}