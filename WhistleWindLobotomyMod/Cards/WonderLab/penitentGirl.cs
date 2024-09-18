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
            
            CardManager.New(wonderlabPrefix, penitentGirl, "The Penitent Girl",
                attack: 0, health: 1)
                .SetBonesCost(3)
                .SetPortraits(ModAssembly, penitentGirl)
                .AddAbilities(Ability.Sharp, Ability.Sharp)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}