using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentEnsemble_T0131()
        {
            const string silentEnsemble = "silentEnsemble";

            NewCard(silentEnsemble, "Chairs",
                attack: 0, health: 3)
                .SetPortraits(silentEnsemble)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Orchestral)
                .Build();
        }
    }
}