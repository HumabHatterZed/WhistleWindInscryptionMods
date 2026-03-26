using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string stainingRose = "wstlWonder_stainingRose";
        private static void StainingRose() {
            CardManager.New(LobotomyPlugin.wonderlabPrefix, stainingRose, "Staining Rose",
                attack: 0, health: 0)
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddAppearances(RoseBackground.appearance)
                .SetHideStats()
                .SetGlobalSpell()
                .SetOnePerDeck()
                .AddTraits(AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments, AbnormalPlugin.Boneless, Trait.Uncuttable, Trait.Terrain)
                .Build();
        }
    }
}