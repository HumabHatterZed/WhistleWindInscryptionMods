using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_VoidDream_T0299()
        {
            const string dreamName = "Void Dream";
            const string voidDream = "voidDream";
            const string voidDreamRooster = "voidDreamRooster";

            CardInfo voidDreamRoosterCard = CardManager.New(pluginPrefix, voidDreamRooster, dreamName,
                attack: 2, health: 3)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, voidDreamRooster)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(Tribe.Hooved, Tribe.Bird)
                .Build();

            CardManager.New(pluginPrefix, voidDream, dreamName,
                attack: 1, health: 1, "A sleeping goat. Or is it a sheep?")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, voidDream)
                .AddAbilities(Ability.Flying, Ability.Evolve)
                .AddTribes(Tribe.Hooved)
                .SetEvolve(voidDreamRoosterCard, 1)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}