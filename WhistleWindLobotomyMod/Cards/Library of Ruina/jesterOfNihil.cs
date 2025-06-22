using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string jesterOfNihil = "wstl_jesterOfNihil";
        private static void JesterOfNihil_O01118() {
            string textureName = "jesterOfNihil";
            CardManager.New(LobotomyPlugin.pluginPrefix, jesterOfNihil, "The Jester of Nihil",
                attack: 0, health: 7)
                .SetBonesCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(ReturnToNihil.ability)
                .AddSpecialAbilities(BoardEffects.specialAbility)
                .SetStatIcon(Nihil.Icon)
                .AddTribes(TribeFae)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetNodeRestrictions(true, false, false, true)
                .SetOnePerDeck()
                .SetEventCard(true)
                .AddMetaCategories(RuinaCard)
                .Build();
        }
    }
}