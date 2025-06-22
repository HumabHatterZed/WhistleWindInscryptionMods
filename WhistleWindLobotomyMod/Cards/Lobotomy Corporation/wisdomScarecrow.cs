using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string wisdomScarecrow = "wstl_wisdomScarecrow";
        public const string wisdomScarecrowPixel = "wstlGBC_wisdomScarecrow";
        private static void WisdomScarecrow_F0187() {
            string name = "Scarecrow Searching for Wisdom";
            string desc = "A hollow-headed scarecrow. Blood soaks its straw limbs.";
            string textureName = "wisdomScarecrow";
            CardManager.New(LobotomyPlugin.pluginPrefix, wisdomScarecrow, name,
                attack: 1, health: 1, desc)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, wisdomScarecrowPixel, name,
                attack: 1, health: 1, desc)
                .SetBonesCost(4)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}