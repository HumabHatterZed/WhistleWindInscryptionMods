using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheLittlePrince_O0466()
        {
            const string theLittlePrince = "theLittlePrince";

            CardManager.New(pluginPrefix, theLittlePrince, "The Little Prince",
                attack: 1, health: 4, "A giant mushroom chunk. A mist of spores surrounds it.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, theLittlePrince)
                .AddAbilities(Sporogenic.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(SporeFriend)
                .SetDefaultEvolutionName("The Little King")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}