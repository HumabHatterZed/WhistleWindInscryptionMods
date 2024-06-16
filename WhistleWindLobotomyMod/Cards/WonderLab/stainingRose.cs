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
        private void XCard_StainingRose()
        {
            const string stainingRose = "stainingRose";
            
            NewCard(stainingRose, "Staining Rose",
                attack: 0, health: 0)
                .SetPortraits(stainingRose)
                .AddAbilities()
                .SetGlobalSpell()
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.None, RiskLevel.Zayin, ModCardType.WonderLab);
        }
    }
}