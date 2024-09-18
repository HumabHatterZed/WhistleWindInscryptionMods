using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DellaLuna_D01105()
        {
            const string dellaLuna = "dellaLuna";

            CardManager.New(pluginPrefix, dellaLuna, "Il Pianto della Luna",
                attack: 2, health: 7, "In reality, man despairs at [c:bR]the moon[c:].")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, dellaLuna)
                .AddAbilities(GroupHealer.ability)
                .AddTribes(Tribe.Bird)
                .AddMetaCategories(DonatorCard)
                .SetDefaultEvolutionName("Il Pianto della Luna Maggiore")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}