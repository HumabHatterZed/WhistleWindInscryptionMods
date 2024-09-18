using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_OldFaithAndPromise_T0997()
        {
            const string oldFaithAndPromise = "oldFaithAndPromise";

            CardManager.New(pluginPrefix, oldFaithAndPromise, "Old Faith and Promise",
                attack: 0, health: 1, "A mysterious marble. Use it without desire or expectation and you may be rewarded.")
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, oldFaithAndPromise)
                .AddAbilities(Alchemist.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName("Elder Faith and Promise")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}