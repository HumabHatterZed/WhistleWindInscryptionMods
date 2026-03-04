using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/1 Follow the Leader
        /// </summary>
        public const string theRoadHome = "wstl_theRoadHome";
        public const string theRoadHomePixel = "wstlGBC_theRoadHome";
        private static void TheRoadHome_F01114() {
            string name = "The Road Home";
            string desc = "A young girl on a quest to return home with her friends.";
            string textureName = "theRoadHome";
            CardManager.New(LobotomyPlugin.pluginPrefix, theRoadHome, name,
                attack: 1, health: 1, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(YellowBrickRoad.ID)
                .AddSpecialAbilities(TheHomingInstinct.specialAbility, YellowBrick.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, theRoadHomePixel, name,
                attack: 1, health: 1, desc)
                .SetGemsCost(GemType.Green)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(YellowBrickRoad.ID)
                .AddSpecialAbilities(TheHomingInstinct.specialAbility, YellowBrick.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}