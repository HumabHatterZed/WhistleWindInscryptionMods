using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            CardHelper.NewCard(true, "wstl", "wstlcard", "Debug",
                attack: 1, health: 99, blood: 0, bones: 0, energy: 0, gems: null)
                .AddAbilities(Persistent.ability, Ability.Sniper, Piercing.ability);
        }
    }
}
