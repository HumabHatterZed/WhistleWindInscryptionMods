using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string stainingRose = "wstlWonder_stainingRose";
        public const string stainingRose2 = "wstlWonder_stainingRose2";
        public const string stainingRose3 = "wstlWonder_stainingRose3";
        public const string stainingRose4 = "wstlWonder_stainingRose4";
        private static void StainingRose() {
            CardInfo info = CardManager.New(LobotomyPlugin.wonderlabPrefix, stainingRose, "Staining Rose",
                attack: 0, health: 0)
                .SetCustomCost<RoseCost>(1)
                .AddAbilities(OrigamiGarden.ID)
                .SetHideStats()
                .SetGlobalSpell()
                .SetOnePerDeck()
                .AddTraits(AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments, AbnormalPlugin.Boneless, Trait.Uncuttable, Trait.Terrain);
            info.appearanceBehaviour.Clear(); // remove spell background

            info.AddAppearances(RoseBackground.appearance)
                .Build();

            CardInfo info2 = CardManager.New(LobotomyPlugin.wonderlabPrefix, stainingRose2, "Staining Rose",
                attack: 0, health: 0)
                .SetCustomCost<RoseCost>(1)
                .AddAbilities(OrigamiGarden.ID)
                .SetHideStats()
                .SetGlobalSpell()
                .SetOnePerDeck()
                .AddTraits(AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments, AbnormalPlugin.Boneless, Trait.Uncuttable, Trait.Terrain);
            info2.appearanceBehaviour.Clear(); // remove spell background

            info2.AddAppearances(Rose2Background.appearance)
                .Build();

            CardInfo info3 = CardManager.New(LobotomyPlugin.wonderlabPrefix, stainingRose3, "Staining Rose",
                attack: 0, health: 0)
                .SetCustomCost<RoseCost>(1)
                .AddAbilities(OrigamiGarden.ID)
                .SetHideStats()
                .SetGlobalSpell()
                .SetOnePerDeck()
                .AddTraits(AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments, AbnormalPlugin.Boneless, Trait.Uncuttable, Trait.Terrain);
            info3.appearanceBehaviour.Clear(); // remove spell background

            info3.AddAppearances(Rose3Background.appearance)
                .Build();

            CardInfo info4 = CardManager.New(LobotomyPlugin.wonderlabPrefix, stainingRose4, "Staining Rose",
                attack: 0, health: 0)
                .AddAbilities()
                .SetHideStats()
                .SetGlobalSpell()
                .SetOnePerDeck()
                .AddTraits(AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments, AbnormalPlugin.Boneless, Trait.Uncuttable, Trait.Terrain);
            info4.appearanceBehaviour.Clear(); // remove spell background

            info4.AddAppearances(Rose4Background.appearance)
                .Build();
        }
    }
}