using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Card_OzmaPumpkinJack_F04116() {
            const string ozmaPumpkinJack = "ozmaPumpkinJack";
            const string ozmaPumpkin = "ozmaPumpkin";
            Tribe[] tribes = new[] { TribeBotanic };

            CardInfo jack = CardManager.New(pluginPrefix, "ozmaPumpkinJack", "Jack", 2, 2)
                .SetPortraits(Assembly, ozmaPumpkinJack)
                .AddTribes(tribes);

            CardInfo jack2 = CardManager.New(pluginPrefix, "ozmaPumpkinJackHollow", "Hollow Jack", 1, 1)
                .SetPortraits(Assembly, "ozmaPumpkinJackHollow")
                .AddAbilities(Ability.Brittle)
                .AddTribes(tribes);

            CardManager.New(pluginPrefix, "ozmaPumpkinWeak", "Rotten Pumpkin", 0, 1)
                .SetPortraits(Assembly, "ozmaPumpkinWeak")
                .AddAbilities(Ability.Evolve, Ability.IceCube)
                .AddTribes(tribes)
                .SetTerrain()
                .SetIceCube(jack2, new List<CardModificationInfo>() { new(-1, 0) { abilities = new() { StartingDecay.ID } } })
                .SetEvolve(jack, 2, new List<CardModificationInfo>() { new(-1, -1) { abilities = new() { Ability.Brittle } } });

            CardManager.New(pluginPrefix, ozmaPumpkin, "Sturdy Pumpkin", 0, 2)
                .SetPortraits(Assembly, ozmaPumpkin)
                .AddAbilities(ThickSkin.ID, Ability.IceCube, Ability.Evolve)
                .AddTribes(tribes)
                .SetTerrain()
                .SetIceCube(jack2)
                .SetEvolve(jack, 2);
        }
    }
}