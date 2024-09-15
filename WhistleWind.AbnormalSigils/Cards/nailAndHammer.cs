using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_NailHammer_O010()
        {
            const string nail = "nail";
            const string hammer = "hammer";
            
            CardManager.New(pluginPrefix, nail, "Nail", 1, 2)
                .SetPortraits(Assembly, nail)
                .AddAbilities(Piercing.ability, RightStrike.ability);

            CardManager.New(pluginPrefix, hammer, "Hammer", 1, 2)
                .SetPortraits(Assembly, hammer)
                .AddAbilities(Driver.ability, LeftStrike.ability);
        }
    }
}