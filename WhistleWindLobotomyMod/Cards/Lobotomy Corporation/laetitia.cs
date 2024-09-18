using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Laetitia_O0167()
        {
            const string laetitia = "laetitia";

            CardManager.New(pluginPrefix, laetitia, "Laetitia",
                attack: 1, health: 2, "A little witch carrying a heart-shaped gift.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, laetitia)
                .AddAbilities(GiftGiver.ability)
                .AddTribes(TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}