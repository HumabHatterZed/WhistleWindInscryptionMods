using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/2 Bloodletter
        /// </summary>
        public const string penitentGirl = "wstlWonder_penitentGirl";
        private static void PenitentGirl() {
            string textureName = "penitentGirl";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, penitentGirl, "The Penitent Girl",
                attack: 1, health: 2, "A once-vain child, now prostrating on bloody stumps.")
                .SetBonesCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodletter.ID)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, "The Penitent Girl",
                attack: 1, health: 2, "A once-vain child, now prostrating on bloody stumps.")
                .SetBonesCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodletter.ID)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .SetCardTemple(CardTemple.Undead)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}