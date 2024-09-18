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
        private void XCard_DingleDangle()
        {
            const string dingleDangle = "dingleDangle";
            
            CardManager.New(wonderlabPrefix, dingleDangle, "Dingle Dangle",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, dingleDangle)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}