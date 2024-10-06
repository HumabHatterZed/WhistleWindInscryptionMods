using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Cards_IndigoOrdeal()
        {
            const string sweeper = "sweeper";
            CardInfo sweeperCard = CardManager.New(pluginPrefix, sweeper, "Sweeper",
                attack: 2, health: 3)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(ModAssembly, sweeper)
                .AddAbilities(Persistent.ability, Bloodfiend.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission, SweeperAppearance.appearance)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal)
                .Build();
        }
    }
}