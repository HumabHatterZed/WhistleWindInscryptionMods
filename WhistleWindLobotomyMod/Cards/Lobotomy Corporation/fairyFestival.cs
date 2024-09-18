using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_FairyFestival_F0483()
        {
            const string fairyFestival = "fairyFestival";

            CardManager.New(pluginPrefix, fairyFestival, "Fairy Festival",
                attack: 1, health: 1, "Everything will be peaceful while you're under the fairies' care.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, fairyFestival)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}