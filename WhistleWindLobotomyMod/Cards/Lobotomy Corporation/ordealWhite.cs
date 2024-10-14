using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Cards_WhiteOrdeal()
        {
            const string fixerRed = "fixerRed";
            const string fixerWhite = "fixerWhite";
            const string fixerBlack = "fixerBlack";
            const string fixerPale = "fixerPale";

            CardInfo red2 = CardManager.New(pluginPrefix, "fixerRed2", "Red Fixer",
                attack: 5, health: 15)
                .SetEnergyCost(6)
                .SetPortraits(ModAssembly, fixerRed)
                .AddAbilities(Ability.AllStrike, Piercing.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo red = CardManager.New(pluginPrefix, fixerRed, "Red Fixer",
                attack: 1, health: 15)
                .SetEnergyCost(6)
                .SetPortraits(ModAssembly, fixerRed)
                .AddAbilities(Ability.Transformer, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(red2, 2)
                .Build();
            red2.SetEvolve(red, 2);

            CardInfo white2 = CardManager.New(pluginPrefix, "fixerWhite2", "White Fixer",
                attack: 0, health: 18)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, "fixerWhite2")
                .AddAbilities(InfiniteShield.ability, Reflector.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo white = CardManager.New(pluginPrefix, fixerWhite, "White Fixer",
                attack: 2, health: 18)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, fixerWhite)
                .AddAbilities(MindStrike.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(white2, 2)
                .Build();
            white2.SetEvolve(white, 2);

            CardInfo black2 = CardManager.New(pluginPrefix, "fixerBlack2", "Black Fixer",
                attack: 2, health: 15)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, fixerBlack)
                .AddAbilities(TeamLeader.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo black = CardManager.New(pluginPrefix, fixerBlack, "Black Fixer",
                attack: 2, health: 15)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, fixerBlack)
                .AddAbilities(Ability.Transformer, Challenging.ability)
                .AddAppearances(ForcedEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(black2, 2)
                .Build();
            black2.SetEvolve(black, 2);

            CardInfo pale2 = CardManager.New(pluginPrefix, "fixerPale2", "Pale Fixer",
                attack: 2, health: 12)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, fixerPale)
                .AddAbilities(Ability.Sniper, Persistent.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo pale = CardManager.New(pluginPrefix, fixerPale, "Pale Fixer",
                attack: 2, health: 12)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, fixerPale)
                .AddAbilities(Persistent.ability, Ability.Transformer, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetEvolve(pale2, 2)
                .Build();
            pale2.SetEvolve(pale, 2);

            CardInfo claw = CardManager.New(pluginPrefix, "claw", "The Claw",
                attack: 2, health: 30)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, "claw")
                .AddAbilities(Piercing.ability, Persistent.ability, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();
        }
    }
}