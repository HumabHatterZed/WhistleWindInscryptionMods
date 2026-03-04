using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string pinocchio = "wstl_pinocchio";
        private static void Pinocchio_F01112() {
            string textureName = "pinocchio";
            CardManager.New(LobotomyPlugin.pluginPrefix, pinocchio, "Pinocchio",
                attack: 0, health: 1, "A wooden doll that mimics the beasts it encounters. Can you see through its lie?")
                .SetBonesCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Copycat.ID)
                .AddTribes(TribeBotanic)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}