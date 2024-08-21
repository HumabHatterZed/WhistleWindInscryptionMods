using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_OzmaPumpkinJack_F04116()
        {
            const string ozmaPumpkinJack = "ozmaPumpkinJack";
            const string ozmaPumpkin = "ozmaPumpkin";
            Tribe[] tribes = new[] { TribeBotanic };

            CardInfo jack = CardManager.New(pluginPrefix, ozmaPumpkinJack, "Jack", 2, 2)
                .SetPortraits(Assembly, ozmaPumpkinJack)
                .AddAbilities(Cursed.ability)
                .AddTribes(tribes);

            CardManager.New(pluginPrefix, ozmaPumpkin, "Pumpkin", 0, 2)
                .SetPortraits(Assembly, ozmaPumpkin)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(jack, 2);
        }
    }
}