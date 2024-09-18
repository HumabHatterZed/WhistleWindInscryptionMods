using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BackwardClock_D09104()
        {
            const string backwardClock = "backwardClock";

            CardManager.New(pluginPrefix, backwardClock, "Backward Clock",
                attack: 0, health: 1, "A clock to rewind your wasted time. Will you pay the toll?")
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, backwardClock, "backwardClock_emission_0")
                .AddAbilities(TimeMachine.ability)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetTerrain()
                .SetNodeRestrictions(true, true, true, true)
                .AddMetaCategories(DonatorCard)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}