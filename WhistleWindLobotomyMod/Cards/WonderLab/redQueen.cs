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
        private void XCard_RedQueen()
        {
            const string redQueen = "redQueen";
            
            NewCard(redQueen, "Red Queen",
                attack: 2, health: 2, blood: 2)
                .SetPortraits(redQueen)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.WonderLab);
        }
    }
}