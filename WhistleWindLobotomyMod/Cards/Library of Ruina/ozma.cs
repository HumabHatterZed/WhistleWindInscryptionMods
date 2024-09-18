using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ozma_F04116()
        {
            const string ozma = "ozma";

            CardManager.New(pluginPrefix, ozma, "Ozma",
                attack: 1, health: 2, "The former ruler of a far away land, now reduced to this.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, ozma)
                .AddAbilities(RightfulHeir.ability)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}