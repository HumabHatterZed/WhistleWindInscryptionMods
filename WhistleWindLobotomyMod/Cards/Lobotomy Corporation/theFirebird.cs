using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheFirebird_O02101()
        {
            const string theFirebird = "theFirebird";

            CardManager.New(pluginPrefix, theFirebird, "The Firebird",
                attack: 2, health: 3, "A bird that longs for the thrill of being hunted.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, theFirebird)
                .AddAbilities(Scorching.ability, Ability.Flying)
                .AddTribes(Tribe.Bird)
                .SetDefaultEvolutionName("The Grand Firebird")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}