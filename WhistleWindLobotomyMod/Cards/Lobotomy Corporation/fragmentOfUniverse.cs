using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_FragmentOfUniverse_O0360()
        {
            const string fragmentOfUniverse = "fragmentOfUniverse";

            CardManager.New(pluginPrefix, fragmentOfUniverse, "Fragment of the Universe",
                attack: 1, health: 2, "You see a song in front of you. It's approaching, becoming more colourful by the second.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, fragmentOfUniverse)
                .AddAbilities(Piercing.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}