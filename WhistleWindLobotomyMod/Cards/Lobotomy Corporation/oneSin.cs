using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string oneSinName = "One Sin and Hundreds\nof Good Deeds";
        public const string oneSin = "wstl_oneSin";
        private static void OneSin_O0303() {
            string desc = "A floating skull. Its hollow sockets see through you.";
            string textureName = "oneSin";
            CardManager.New(LobotomyPlugin.pluginPrefix, oneSin, oneSinName,
                attack: 0, health: 1, desc)
                .SetBonesCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Martyr.ability)
                .AddTribes(TribeDivine)
                .AddTraits(DiskCardGame.Trait.Goat)
                .SetDefaultEvolutionName(oneSinName)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, oneSinName,
                attack: 0, health: 1, desc)
                .SetBonesCost(1)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Martyr.ability)
                .AddTribes(TribeDivine)
                .AddTraits(DiskCardGame.Trait.Goat)
                .SetDefaultEvolutionName(oneSinName)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}