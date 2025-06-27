using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Card_Finger() {
            const string finger = "finger";
            CardManager.New(pluginPrefix, "finger_left", "Finger", 1, 1)
                .SetPortraits(Assembly, finger)
                .AddAbilities(RightStrike.ability, MindStrike.ability)
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "finger_right", "Finger", 1, 1)
                .SetPortraits(Assembly, finger)
                .AddAbilities(LeftStrike.ability, MindStrike.ability)
                .SetTerrain(false);
        }
    }
}