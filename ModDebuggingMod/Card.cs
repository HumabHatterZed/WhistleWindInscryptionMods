using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod {
    public partial class Plugin {
        private void CARD_DEBUG() {
            //MyTestCost.Init();

            CardInfo info = CardManager.New("wstl", "wstlcard", "Debug",
                attack: 1, health: 100)
                .AddAbilities(Ability.Sniper)
                //.SetCost(0, 0, 0, new List<GemType>() { GemType.Blue, GemType.Blue, GemType.Green, GemType.Green, GemType.Green, GemType.Green, GemType.Orange })
                //.AddAbilities(Test.ability)
                //.AddSpecialAbilities(BlindRage.specialAbility)
                //.SetTransformerCardId("Squirrel")
                .SetEvolve("Squirrel", 6)
                .SetPortraits(typeof(Plugin).Assembly, "misterWin_grimora", emissionName: "misterWin_grimora_emission", pixelPortraitName: "buffBell.png")
                ;
        }
    }
}
