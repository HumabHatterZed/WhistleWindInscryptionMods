using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string skinCheers = "wstl_skinCheers";
        public const string skinHarmony = "wstl_skinHarmony";
        public const string skinClimax = "wstl_skinClimax";
        public const string skinTail = "wstl_skinTail";

        private static void Cards_CrimsonOrdeal() {
            CardInfo tail = CardManager.New(LobotomyPlugin.pluginPrefix, skinTail, "Too slow!",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skin_tail")
                .AddAbilities(StartingDecay.ability, Soulbound.ability)
                .SetHideStats()
                .SetBoneless()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, skinCheers, OrdealUtils.GetOrdealTitle(OrdealType.Crimson, 0),
                attack: 1, health: 3)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skinCheers")
                .SetTitle(LobotomyPlugin.ModAssembly, "skinCheers_title.png")
                .AddAbilities(Withering.ability, Ability.ExplodeOnDeath)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .SetTail(tail)
                .Build();

            CardInfo harmony = CardManager.New(LobotomyPlugin.pluginPrefix, skinHarmony, "The Harmony\nof Skin",
                attack: 2, health: 4)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skinHarmony")
                .AddAbilities(HarmonyAbility.ability, Challenging.ability)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .AddTraits(Trait.Uncuttable)
                .SetTail(tail)
                .Build();

            CardInfo struggle = CardManager.New(LobotomyPlugin.pluginPrefix, skinClimax, OrdealUtils.GetOrdealTitle(OrdealType.Crimson, 2),
                attack: 3, health: 5)
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skinClimax")
                .AddAbilities(Ability.IceCube, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .AddTraits(Trait.Uncuttable, PriorityMovement)
                .SetIceCube(harmony)
                .SetTail(tail)
                .Build();
        }
    }
}