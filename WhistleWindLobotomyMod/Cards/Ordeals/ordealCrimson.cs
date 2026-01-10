using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
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
            CardManager.New(LobotomyPlugin.pluginPrefix, skinCheers, OrdealUtils.GetOrdealTitle(OrdealType.Crimson, 0),
                attack: 1, health: 3)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetTitle(LobotomyPlugin.ModAssembly, "skinCheers_title.png")
                .AddAbilities(Withering.ability, Ability.ExplodeOnDeath)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .Build();

            CardInfo harmony = CardManager.New(LobotomyPlugin.pluginPrefix, skinHarmony, OrdealUtils.GetOrdealTitle(OrdealType.Crimson, 1),
                attack: 2, health: 4)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(HarmonyAbility.ability, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .AddTraits(Trait.Uncuttable)
                .Build();

            CardInfo struggle = CardManager.New(LobotomyPlugin.pluginPrefix, skinClimax, OrdealUtils.GetOrdealTitle(OrdealType.Crimson, 2),
                attack: 3, health: 5)
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Ability.IceCube, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .AddTraits(Trait.Uncuttable, PriorityMovement)
                .SetIceCube(harmony)
                .Build();
        }
    }
}