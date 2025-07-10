using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Card_Littlecello() {
            const string moon = "littlecello";
            CardManager.New(pluginPrefix, moon, "The Littlecello", 5, 1)
                .SetPortraits(Assembly, moon)
                .AddAbilities(Ability.SkeletonStrafe, Ability.Submerge)
                .SetTerrain(false);
        }
    }
}