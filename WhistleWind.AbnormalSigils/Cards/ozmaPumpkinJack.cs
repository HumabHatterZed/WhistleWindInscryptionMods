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

            CreateCard(MakeCard(
                ozmaPumpkinJack,
                "Jack",
                attack: 2, health: 2)
                .SetPortraits(ozmaPumpkinJack)
                .AddAbilities(Cursed.ability)
                .AddTribes(tribes));

            CreateCard(MakeCard(
                ozmaPumpkin,
                "Pumpkin",
                attack: 0, health: 2)
                .SetPortraits(ozmaPumpkin)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve("wstl_ozmaPumpkinJack", 2));
        }
    }
}