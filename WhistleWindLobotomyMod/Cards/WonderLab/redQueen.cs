using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void XCard_RedQueen()
        {
            const string redQueen = "redQueen";
            
            CardManager.New(wonderlabPrefix, redQueen, "Red Queen",
                attack: 2, health: 2)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, redQueen)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}