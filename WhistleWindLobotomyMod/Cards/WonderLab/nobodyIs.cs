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
            
            NewCard(nobodyIs, "Nobody Is",
                attack: 0, health: 0, bones: 0)
                .SetPortraits(nobodyIs)
                .AddAbilities()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.WonderLab);
        }
    }
}