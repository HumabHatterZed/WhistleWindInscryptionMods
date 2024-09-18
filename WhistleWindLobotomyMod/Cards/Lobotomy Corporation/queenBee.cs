using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_QueenBee_T0450()
        {
            const string queenBee = "queenBee";

            CardManager.New(pluginPrefix, queenBee, "Queen Bee",
                attack: 0, health: 4, "A monstrous amalgam of a hive and a bee.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, queenBee)
                .AddAbilities(QueenNest.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}