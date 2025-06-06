using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_LaetitiaFriend_O0167()
        {
            const string laetitiaFriend = "laetitiaFriend";

            CardManager.New(pluginPrefix, laetitiaFriend, "Wee Witch's Friend", 1, 1)
                .SetBonesCost(2)
                .AddAbilities(DiskCardGame.Ability.ExplodeOnDeath)
                .SetPortraits(Assembly, laetitiaFriend)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("Wee Witch's Big Friend");
        }
    }
}