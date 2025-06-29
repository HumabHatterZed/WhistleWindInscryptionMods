using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string blueSmockedShepherd = "wstlWonder_blueSmockedShepherd";
        private static void BlueSmockedShepherd() {
            string textureName = "blueShepherd";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, blueSmockedShepherd, "Blue-Smocked Shepherd",
                attack: 2, health: 2, "A cruel shepherd with a penchant for lying.")
                .SetBonesCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Abusive.ability, Ability.SplitStrike)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}