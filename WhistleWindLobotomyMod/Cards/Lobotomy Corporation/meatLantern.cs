using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MeatLantern_O0484()
        {
            const string meatLantern = "meatLantern";

            CardManager.New(pluginPrefix, meatLantern, "Meat Lantern",
                attack: 1, health: 2, "A beautiful flower attached to a mysterious creature.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, meatLantern)
                .AddAbilities(Ability.Reach, Punisher.ability)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}