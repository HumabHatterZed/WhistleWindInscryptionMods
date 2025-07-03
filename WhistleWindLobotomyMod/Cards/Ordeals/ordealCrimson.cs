using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string skinCheers = "wstl_skinCheers";
        public const string skinHarmony = "wstl_skinHarmony";
        public const string skinClimax = "wstl_skinClimax";
        private static void Cards_CrimsonOrdeal() {
            string textureName = "skinCheers";
            string textureName2 = "skinHarmony";
            string textureName3 = "skinClimax";
            CardManager.New(LobotomyPlugin.pluginPrefix, skinCheers, "Cheers for the Beginning",
                attack: 1, health: 3)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetTitle(LobotomyPlugin.ModAssembly, "skinCheers_title.png")
                .AddAbilities(Withering.ability, Ability.ExplodeOnDeath)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal)
                .Build();

            CardInfo harmony = CardManager.New(LobotomyPlugin.pluginPrefix, skinHarmony, "Harmony of Skin",
                attack: 2, health: 4)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(HarmonyAbility.ability, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();

            CardInfo struggle = CardManager.New(LobotomyPlugin.pluginPrefix, skinClimax, "Struggle at the Climax",
                attack: 3, health: 5)
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Ability.IceCube, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .SetIceCube(harmony)
                .Build();
        }
    }
}