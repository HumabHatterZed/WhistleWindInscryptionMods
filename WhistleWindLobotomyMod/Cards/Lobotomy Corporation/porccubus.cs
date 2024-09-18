using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Porccubus_O0298()
        {
            const string porccubus = "porccubus";

            CardManager.New(pluginPrefix, porccubus, "Porccubus",
                attack: 1, health: 1, "A prick from one of its quills creates a deadly euphoria.")
                .SetBonesCost(5)
                .SetPortraits(ModAssembly, porccubus)
                .AddAbilities(Ability.Deathtouch)
                .AddTribes(TribeBotanic)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}