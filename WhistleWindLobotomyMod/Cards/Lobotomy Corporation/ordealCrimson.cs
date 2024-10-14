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
        private void Cards_CrimsonOrdeal()
        {
            const string skinCheers = "skinCheers";
            const string skinHarmony = "skinHarmony";
            const string skinClimax = "skinClimax";

            CardInfo cheer = CardManager.New(pluginPrefix, skinCheers, "Cheers for the Beginning",
                attack: 0, health: 3)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, skinCheers)
                .SetTitle(ModAssembly, "skinCheers_title.png")
                .AddAbilities(Withering.ability, Ability.Strafe, Ability.ExplodeOnDeath)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal)
                .Build();

            CardInfo harmony = CardManager.New(pluginPrefix, skinHarmony, "Harmony of Skin",
                attack: 1, health: 4)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, skinHarmony)
                .AddAbilities(HarmonyAbility.ability, Ability.StrafeSwap, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo struggle = CardManager.New(pluginPrefix, skinClimax, "Struggle at the Climax",
                attack: 3, health: 5)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, skinClimax)
                .AddAbilities(Ability.IceCube, Cycler.ability, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetIceCube(harmony)
                .Build();
        }
    }
}