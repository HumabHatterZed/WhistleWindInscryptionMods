using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string stainingRose = "wstlWonder_stainingRose";
        private static void StainingRose() {
            return;
            string textureName = "stainingRose";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, stainingRose, "Staining Rose",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                .SetInstaGlobalSpell()
                .SetOnePerDeck()
                .SetNodeRestrictions(true, true, true, true)
                .Build();
        }
    }
}