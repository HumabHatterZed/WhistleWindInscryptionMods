using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string fixerRed = "wstl_fixerRed";
        public const string fixerWhite = "wstl_fixerWhite";
        public const string fixerBlack = "wstl_fixerBlack";
        public const string fixerPale = "wstl_fixerPale";
        public const string fixerRed2 = "wstl_fixerRed2";
        public const string fixerWhite2 = "wstl_fixerWhite2";
        public const string fixerBlack2 = "wstl_fixerBlack2";
        public const string fixerPale2 = "wstl_fixerPale2";
        public const string claw = "wstl_claw";
        private static void Cards_WhiteOrdeal() {
            string textureName = "fixerRed";
            string textureName2 = "fixerWhite2";
            string textureName3 = "fixerWhite";
            string textureName4 = "fixerBlack";
            string textureName5 = "fixerPale";
            CardInfo red2 = CardManager.New(LobotomyPlugin.pluginPrefix, fixerRed2, "Red Fixer",
                attack: 3, health: 6)
                .SetEnergyCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.AllStrike, Piercing.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo red = CardManager.New(LobotomyPlugin.pluginPrefix, fixerRed, "Red Fixer",
                attack: 3, health: 6)
                .SetEnergyCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Piercing.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(red2, 2)
                .Build();
            red2.SetEvolve(red, 2);

            CardInfo white2 = CardManager.New(LobotomyPlugin.pluginPrefix, fixerWhite2, "White Fixer",
                attack: 0, health: 10)
                .SetEnergyCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(InfiniteShield.ability, Reflector.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo white = CardManager.New(LobotomyPlugin.pluginPrefix, fixerWhite, "White Fixer",
                attack: 1, health: 10)
                .SetEnergyCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Ability.Sniper, MindStrike.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(white2, 2)
                .Build();
            white2.SetEvolve(white, 2);

            CardInfo black2 = CardManager.New(LobotomyPlugin.pluginPrefix, fixerBlack2, "Black Fixer",
                attack: 2, health: 7)
                .SetEnergyCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName4)
                .AddAbilities(TeamLeader.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo black = CardManager.New(LobotomyPlugin.pluginPrefix, fixerBlack, "Black Fixer",
                attack: 2, health: 7)
                .SetBloodCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName4)
                .AddAbilities(Ability.BuffNeighbours, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(black2, 2)
                .Build();
            black2.SetEvolve(black, 2);

            CardInfo pale = CardManager.New(LobotomyPlugin.pluginPrefix, fixerPale, "Pale Fixer",
                attack: 2, health: 6)
                .SetBloodCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName5)
                .AddAbilities(IntenseVolley.ability, Piercing.ability, Persistent.ability, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo clawCard = CardManager.New(LobotomyPlugin.pluginPrefix, claw, "The Claw",
                attack: 3, health: 20)
                .SetBloodCost(15)
                //.SetPortraits(LobotomyPlugin.ModAssembly, textureName claw)
                .AddAbilities(/*ClawAbility.ability*/Piercing.ability, Persistent.ability, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath, PriorityMovement)
                .Build();
        }
    }
}