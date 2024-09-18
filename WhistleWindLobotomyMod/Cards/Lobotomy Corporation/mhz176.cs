using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MHz176_T0727()
        {
            const string mhz176 = "mhz176";

            CardManager.New(pluginPrefix, mhz176, "1.76 MHz",
                attack: 2, health: 1, "This is a record. A record of a day we must never forget.")
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, mhz176)
                .AddAbilities(Ability.BuffEnemy)
                .SetTerrain(false)
                .SetDefaultEvolutionName("1.76 GHz")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}