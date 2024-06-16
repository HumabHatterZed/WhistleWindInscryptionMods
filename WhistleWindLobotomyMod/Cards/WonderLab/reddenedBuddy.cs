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
        private void XCard_ReddenedBuddy()
        {
            const string reddenedBuddy = "reddenedBuddy";
            
            NewCard(reddenedBuddy, "Reddened Buddy",
                attack: 0, health: 0, bones: 0)
                .SetPortraits(reddenedBuddy)
                .AddAbilities()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He, ModCardType.WonderLab);
        }
    }
}