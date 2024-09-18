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
        private void XCard_Nobodyis()
        {
            const string nobodyIs = "nobodyIs";
            
            CardManager.New(wonderlabPrefix, nobodyIs, "Nobody Is",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, nobodyIs)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}