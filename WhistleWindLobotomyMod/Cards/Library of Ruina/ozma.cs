using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string ozma = "wstl_ozma";
        private static void Ozma_F04116()
        {
            string name = "Ozma";
            string desc = "The former ruler of a far away land, now reduced to a shambling husk.";
            string textureName = "ozma";
            CardManager.New(LobotomyPlugin.pluginPrefix, ozma, name,
                attack: 1, health: 2, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(RightfulHeir.ability)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 2, desc)
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(RightfulHeir.ability)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}