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
            
            CardManager.New(wonderlabPrefix, bottleOfTears, "Bottle of Tears",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, bottleOfTears)
                .AddAbilities(Spilling.ability, Ability.Morsel)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}