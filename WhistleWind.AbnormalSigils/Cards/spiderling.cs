using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

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

            CreateCard(MakeCard(
                cardName: spiderBrood,
                "Spider Brood",
                attack: 1, health: 3, bones: 3)
                .SetPortraits(spiderBrood)
                .AddTribes(tribes)
                .AddAppearances(appearances)
                .SetDefaultEvolutionName("Spider Buff"));

            CreateCard(MakeCard(
                cardName: spiderling,
                "Spiderling",
                attack: 0, health: 1)
                .SetPortraits(spiderling)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .AddAppearances(appearances)
                .SetEvolve("wstl_spiderBrood", 1)
                .SetAffectedByTidalLock());
        }
    }
}