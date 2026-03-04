using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string giantTreeSap = "wstl_giantTreeSap";
        private static void GiantTreeSap_T0980() {
            string textureName = "giantTreeSap";
            CardManager.New(LobotomyPlugin.pluginPrefix, giantTreeSap, "Giant Tree Sap",
                attack: 0, health: 4, "Sap from a tree at the end of the world. It is a potent healing agent.")
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Morsel, Ability.ExplodeOnDeath, StartingDecay.ID, StartingDecay.ID)
                .AddTribes(TribeBotanic)
                .SetDefaultEvolutionName("Giant Elder Tree Sap")
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}