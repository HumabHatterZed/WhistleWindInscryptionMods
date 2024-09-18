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
            
            CardManager.New(wonderlabPrefix, stainingRose, "Staining Rose",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, stainingRose)
                .AddAbilities()
                .SetInstaGlobalSpell()
                .SetOnePerDeck()
                .SetNodeRestrictions(true, true, true, true)
                .Build();
        }
    }
}