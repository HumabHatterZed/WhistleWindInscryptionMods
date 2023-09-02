using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Pelts;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            CardInfo info = CardHelper.NewCard(true, "wstl", "wstlcard", "Debug",
                attack: 1, health: 100, blood: 0, bones: 0, energy: 0, gems: null)
                .AddAbilities(Ability.ShieldGems)
                .AddSpecialAbilities(Bind.specialAbility)
                //.SetTransformerCardId("Squirrel")//.SetEvolve("Squirrel", 1)
                .SetPortraits("misterWin_grimora", emissionName: "misterWin_grimora_emission", pixelPortraitName: "buffBell.png")
                ;

            //info.AddAppearances(ForcedWhiteEmission.appearance);
            //info.SetExtendedProperty("ForbiddenMoxCost", 1);
        }
    }
}
