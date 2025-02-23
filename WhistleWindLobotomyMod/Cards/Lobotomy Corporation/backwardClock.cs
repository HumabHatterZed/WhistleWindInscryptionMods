using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string backwardClock = "wstl_backwardClock";
        private static void BackwardClock_D09104()
        {
            string name = "Backward Clock";
            string desc = "A clock to rewind your wasted time, in exchange for anothers.";
            string textureName = "backwardClock";
            string textureName2 = "backwardClock_emission_0.png";
            CardManager.New(LobotomyPlugin.pluginPrefix, backwardClock, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName, textureName2)
                .AddAbilities(TimeMachine.ability)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetTerrain()
                .SetNodeRestrictions(true, true, true, true)
                .AddMetaCategories(DonatorCard)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName, textureName2)
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