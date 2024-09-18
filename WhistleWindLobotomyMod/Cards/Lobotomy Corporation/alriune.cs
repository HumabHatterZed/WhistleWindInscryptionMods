using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Alriune_T0453()
        {
            const string alriune = "alriune";
            const string description = "A doll yearning to be a human. A human yearning to be a doll.";
            CardManager.New(pluginPrefix, alriune, "Alriune",
                attack: 4, health: 5, description)
                .SetPortraits(ModAssembly, alriune)
                .AddAbilities(Ability.Strafe)
                .AddTribes(TribeBotanic, Tribe.Hooved)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}