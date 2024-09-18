using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YoureBald_BaldIsAwesome()
        {
            const string youreBald = "youreBald";

            CardManager.New(pluginPrefix, youreBald, "You're Bald...",
                attack: 0, health: 2, "I've always wondered what it's like to be bald.")
                .SetEnergyCost(2)
                .SetPortraits(ModAssembly, youreBald)
                .AddAbilities(Ability.DrawCopy)
                .SetDefaultEvolutionName("You're Really Bald...")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}