using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string youreBald = "wstl_youreBald";
        private static void YoureBald_BaldIsAwesome()
        {
            string textureName = "youreBald";
            CardManager.New(LobotomyPlugin.pluginPrefix, youreBald, "You're Bald...",
                attack: 0, health: 2, "I've always wondered what it's like to be bald.")
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DrawCopy)
                .SetDefaultEvolutionName("You're Really Bald...")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}