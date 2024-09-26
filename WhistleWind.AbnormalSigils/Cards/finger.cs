using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_Finger()
        {
            const string finger = "finger";
            CardManager.New(pluginPrefix, finger, "Finger", 1, 1)
                .SetPortraits(Assembly, finger)
                .AddAbilities(Ability.Sniper, MindStrike.ability);
        }
    }
}