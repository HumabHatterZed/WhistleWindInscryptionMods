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
        private void XCard_BottleOfTears()
        {
            const string bottleOfTears = "bottleOfTears";
            
            NewCard(bottleOfTears, "Bottle of Tears",
                attack: 0, health: 0, bones: 0)
                .SetPortraits(bottleOfTears)
                .AddAbilities()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin, ModCardType.WonderLab);
        }
    }
}