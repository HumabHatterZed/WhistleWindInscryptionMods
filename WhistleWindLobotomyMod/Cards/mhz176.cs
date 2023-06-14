using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MHz176_T0727()
        {
            const string mhz176 = "mhz176";

            CardInfo mhz176Card = NewCard(
                mhz176,
                "1.76 MHz",
                "This is a record, a record of a day we must never forget.",
                attack: 2, health: 1, energy: 3)
                .SetPortraits(mhz176)
                .AddAbilities(Ability.BuffEnemy)
                .SetTerrain(false)
                .SetEvolveInfo("Loud {0}");

            CreateCard(mhz176Card, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}