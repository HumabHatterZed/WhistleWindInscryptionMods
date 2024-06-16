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
        private void XCard_PenitentGirl()
        {
            const string penitentGirl = "penitentGirl";
            
            NewCard(penitentGirl, "The Penitent Girl",
                attack: 0, health: 1, bones: 3)
                .SetPortraits(penitentGirl)
                .AddAbilities(Ability.Sharp, Ability.Sharp)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin, ModCardType.WonderLab);
        }
    }
}