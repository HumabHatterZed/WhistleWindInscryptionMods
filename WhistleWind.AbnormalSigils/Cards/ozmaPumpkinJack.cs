using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

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
                attack: 2, health: 2, blood: 1)
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
                .SetEvolveInfo("wstl_ozmaPumpkinJack", 2));
        }
    }
}