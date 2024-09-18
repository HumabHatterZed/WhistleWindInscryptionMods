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
            
            CardManager.New(wonderlabPrefix, whiteLake, "White Lake",
                attack: 1, health: 3)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, whiteLake)
                .AddAbilities(Damsel.ability)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}