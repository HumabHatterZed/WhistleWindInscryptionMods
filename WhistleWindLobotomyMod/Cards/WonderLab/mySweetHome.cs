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
            
            NewCard(mySweetHome, "My Sweet Home",
                attack: 0, health: 2, blood: 1)
                .SetPortraits(mySweetHome)
                .AddAbilities(Ability.MadeOfStone, Ability.Reach)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.WonderLab);
        }
    }
}