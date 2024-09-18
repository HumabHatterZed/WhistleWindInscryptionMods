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
        private void XCard_MySweetHome()
        {
            const string mySweetHome = "mySweetHome";
            
            CardManager.New(wonderlabPrefix, mySweetHome, "My Sweet Home",
                attack: 0, health: 2)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, mySweetHome)
                .AddAbilities(Ability.MadeOfStone, Ability.Reach)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}