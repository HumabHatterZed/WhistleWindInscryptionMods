using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string theLittlePrince = "wstl_theLittlePrince";
        private static void TheLittlePrince_O0466() {
            string textureName = "theLittlePrince";
            CardManager.New(LobotomyPlugin.pluginPrefix, theLittlePrince, "The Little Prince",
                attack: 1, health: 4, "A giant mushroom chunk. A mist of spores surrounds it.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Sporogenic.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(SporeFriend)
                .SetDefaultEvolutionName("The Little King")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}