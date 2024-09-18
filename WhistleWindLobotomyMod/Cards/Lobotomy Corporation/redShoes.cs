using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RedShoes_O0408()
        {
            const string redShoes = "redShoes";

            CardManager.New(pluginPrefix, redShoes, "Red Shoes",
                attack: 0, health: 3, "How pretty. Maybe they'll fit.")
                .SetBonesCost(3)
                .SetPortraits(ModAssembly, redShoes)
                .AddAbilities(Ability.Sharp, Ability.GuardDog)
                .SetTerrain()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}