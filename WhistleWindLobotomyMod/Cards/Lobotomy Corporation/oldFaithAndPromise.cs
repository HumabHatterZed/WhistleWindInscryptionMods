using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string oldFaithAndPromise = "wstl_oldFaithAndPromise";
        private static void OldFaithAndPromise_T0997()
        {
            string name = "Old Faith and Promise";
            string name2 = "Elder Faith and Promise";
            string desc = "A mysterious marble. Use it without desire or expectation and you may be rewarded.";
            string textureName = "oldFaithAndPromise";
            CardManager.New(LobotomyPlugin.pluginPrefix, oldFaithAndPromise, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Alchemist.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Alchemist.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}