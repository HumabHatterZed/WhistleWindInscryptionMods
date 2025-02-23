using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string redShoes = "wstl_redShoes";
        private static void RedShoes_O0408()
        {
            string textureName = "redShoes";
            CardManager.New(LobotomyPlugin.pluginPrefix, redShoes, "Red Shoes",
                attack: 0, health: 3, "How pretty. Maybe they'll fit.")
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sharp, Ability.GuardDog)
                .SetTerrain()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}