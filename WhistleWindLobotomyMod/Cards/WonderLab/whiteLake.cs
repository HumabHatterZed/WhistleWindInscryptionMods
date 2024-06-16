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
        private void XCard_WhiteLake()
        {
            const string whiteLake = "whiteLake";
            
            NewCard(whiteLake, "White Lake",
                attack: 1, health: 5, blood: 2)
                .SetPortraits(whiteLake)
                .AddAbilities()
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He, ModCardType.WonderLab);
        }
    }
}