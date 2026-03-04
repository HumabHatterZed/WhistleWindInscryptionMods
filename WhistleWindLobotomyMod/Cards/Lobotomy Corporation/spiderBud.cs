using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string spiderBud = "wstl_spiderBud";
        private static void SpiderBud_O0243() {
            string textureName = "spiderBud";
            CardManager.New(LobotomyPlugin.pluginPrefix, spiderBud, "Spider Bud",
                attack: 0, health: 2, "A grotesque mother of spiders. Its children are small but grow quickly.")
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BroodMother.ID)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}