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
                .AddAbilities(StartingDecay.ID, Soulbound.ID)
                .SetHideStats()
                .SetBoneless()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, skinCheers, "Cheers for\nthe Beginning",
                attack: 0, health: 2)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skinCheers")
                .AddAbilities(Withering.ID, Ability.ExplodeOnDeath)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .SetTail(tail)
                .Build();

            CardInfo harmony = CardManager.New(LobotomyPlugin.pluginPrefix, skinHarmony, "The Harmony\nof Skin",
                attack: 1, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skinHarmony")
                .AddAbilities(HarmonyAbility.ID, Challenging.ID)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .AddTraits(Trait.Uncuttable, PriorityMovement)
                .SetTail(tail)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, skinClimax, OrdealUtils.GetOrdealTitle(OrdealType.Crimson, 2),
                attack: 3, health: 5)
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, "skinClimax")
                .AddAbilities(Ability.IceCube, NimbleFoot.ID, Challenging.ID)
                .AddAppearances(OrdealBackgroundCrimsonClimax.appearance, CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeFae)
                .SetOrdealCard(Opponents.OrdealType.Crimson)
                .AddTraits(Trait.Uncuttable)
                .SetIceCube(harmony)
                .SetTail(tail)
                .RemoveAppearances(OrdealBackgroundCrimson.appearance)
                .Build();
        }
    }
}