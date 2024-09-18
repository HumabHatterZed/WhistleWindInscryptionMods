using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BigBird_O0240()
        {
            const string bigBird = "bigBird";

            CardManager.New(pluginPrefix, bigBird, "Big Bird",
                attack: 2, health: 4, "Its eyes light up the darkness like stars.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, bigBird)
                .AddAbilities(Cycler.ability)
                .AddSpecialAbilities(ThreeBirds.specialAbility)
                .AddTribes(Tribe.Bird)
                .AddTraits(BlackForest)
                .SetDefaultEvolutionName("Bigger Bird")
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}