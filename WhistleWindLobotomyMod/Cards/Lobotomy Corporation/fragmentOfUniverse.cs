using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string fragmentOfUniverse = "wstl_fragmentOfUniverse";
        private static void FragmentOfUniverse_O0360()
        {
            string name = "Fragment of the Universe";
            string desc = "You see a song in front of you. It's approaching, becoming more colourful by the second.";
            string textureName = "fragmentOfUniverse";
            CardManager.New(LobotomyPlugin.pluginPrefix, fragmentOfUniverse, name,
                attack: 1, health: 2, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Piercing.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 2, desc)
                .SetGemsCost(GemType.Green)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Piercing.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}