using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Card_MiniMoon() {
            const string moon = "miniMoon";
            CardManager.New(pluginPrefix, moon, "Mini Moon", 1, 5)
                .SetPortraits(Assembly, moon)
                .AddAbilities(Ability.SplitStrike, Ability.Reach, Ability.MadeOfStone)
                .SetTerrain(false);
        }
    }
}