using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentOrchestra_T0131()
        {
            const string silentOrchestra = "silentOrchestra";
            const string silentEnsemble = "silentEnsemble";

            CardManager.New(pluginPrefix, silentEnsemble, "Chairs",
                attack: 0, health: 3)
                .SetPortraits(ModAssembly, silentEnsemble)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Orchestral)
                .Build();

            CardManager.New(pluginPrefix, silentOrchestra, "The Silent Orchestra",
                attack: 2, health: 6, "Soon, the song none can hear but all can listen to will begin.")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, silentOrchestra)
                .AddAbilities(Conductor.ability)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Orchestral)
                .SetDefaultEvolutionName("The Grand Silent Orchestra")
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}