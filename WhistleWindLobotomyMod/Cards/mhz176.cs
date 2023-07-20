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

            NewCard(mhz176, "1.76 MHz", "This is a record, a record of a day we must never forget.",
                attack: 2, health: 1, energy: 3)
                .SetPortraits(mhz176)
                .AddAbilities(Ability.BuffEnemy)
                .SetTerrain(false)
                .SetDefaultEvolutionName("1.76 GHz")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}