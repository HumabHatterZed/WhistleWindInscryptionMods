using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string voidDream = "wstl_voidDream";
        public const string voidDreamRooster = "wstl_voidDreamRooster";
        private static void VoidDream_T0299()
        {
            string textureName = "voidDreamRooster";
            string textureName2 = "voidDream";
            CardInfo voidDreamRoosterCard = CardManager.New(LobotomyPlugin.pluginPrefix, voidDreamRooster, "Void Dream",
                attack: 2, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(Tribe.Hooved, Tribe.Bird)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, voidDream, "Void Dream",
                attack: 1, health: 1, "A sleeping goat. Or is it a sheep?")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Flying, Ability.Evolve)
                .AddTribes(Tribe.Hooved)
                .SetEvolve(voidDreamRoosterCard, 1)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}