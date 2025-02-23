using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string dellaLuna = "wstl_dellaLuna";
        private static void DellaLuna_D01105()
        {
            string textureName = "dellaLuna";
            CardManager.New(LobotomyPlugin.pluginPrefix, dellaLuna, "Il Pianto della Luna",
                attack: 2, health: 7, "In reality, man despairs at [c:bR]the moon[c:].")
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GroupHealer.ability)
                .AddTribes(Tribe.Bird)
                .AddMetaCategories(DonatorCard)
                .SetDefaultEvolutionName("Il Pianto della Luna Maggiore")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}