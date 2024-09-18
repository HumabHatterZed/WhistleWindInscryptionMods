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
        private void Card_GraveOfBlossoms_O04100()
        {
            const string graveOfBlossoms = "graveOfBlossoms";

            CardManager.New(pluginPrefix, graveOfBlossoms, "Grave of Cherry Blossoms",
                attack: 0, health: 3, "A blooming cherry tree. The more blood it has, the more beautiful it becomes.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, graveOfBlossoms)
                .AddAbilities(Bloodletter.ability)
                .AddTribes(TribeBotanic)
                .SetDefaultEvolutionName("Mass Grave of Cherry Blossoms")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}