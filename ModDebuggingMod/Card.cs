using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine.UIElements;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            CardInfo info = CardHelper.NewCard(true, "wstl", "wstlcard", "Debug",
                attack: 10, health: 100, blood: 0, bones: 0, energy: 1, gems: new() { })
                .AddAbilities(NimbleFoot.ability)
                //.AddSpecialAbilities(BlindRage.specialAbility)
                //.SetTransformerCardId("Squirrel")
                .SetEvolve("Squirrel", 1)
                .SetPortraits("misterWin_grimora", emissionName: "misterWin_grimora_emission", pixelPortraitName: "buffBell.png")
                ;

            //info.AddAppearances(ForcedWhiteEmission.appearance);
            //info.SetExtendedProperty("TestCost", 2);
        }
    }
}
