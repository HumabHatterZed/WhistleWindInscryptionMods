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
            
            NewCard(dingleDangle, "Dingle Dangle",
                attack: 0, health: 0, bones: 0)
                .SetPortraits(dingleDangle)
                .AddAbilities()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.WonderLab);
        }
    }
}