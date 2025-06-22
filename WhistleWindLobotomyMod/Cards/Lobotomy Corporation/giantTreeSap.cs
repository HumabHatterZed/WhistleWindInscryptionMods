using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string giantTreeSap = "wstl_giantTreeSap";
        private static void GiantTreeSap_T0980() {
            string textureName = "giantTreeSap";
            CardManager.New(LobotomyPlugin.pluginPrefix, giantTreeSap, "Giant Tree Sap",
                attack: 0, health: 3, "Sap from a tree at the end of the world. It is a potent healing agent.")
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sacrificial, Ability.Morsel)
                .AddSpecialAbilities(Sap.specialAbility)
                .AddTribes(TribeBotanic)
                .SetDefaultEvolutionName("Giant Elder Tree Sap")
                .Build(CardHelper.CardType.Rare, RiskLevel.He, true);
        }
    }
}