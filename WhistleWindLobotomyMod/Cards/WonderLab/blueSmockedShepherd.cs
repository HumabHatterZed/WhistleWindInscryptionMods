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
        private void XCard_BlueSmockedShepherd()
        {
            const string blueSmockedShepherd = "blueSmockedShepherd";
            
            CardManager.New(wonderlabPrefix, blueSmockedShepherd, "Blue-Smocked Shepherd",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, blueSmockedShepherd)
                .AddAbilities(Abusive.ability)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}