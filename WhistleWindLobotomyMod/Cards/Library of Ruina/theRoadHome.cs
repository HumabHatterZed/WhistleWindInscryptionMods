using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheRoadHome_F01114()
        {
            const string theRoadHome = "theRoadHome";

            CardManager.New(pluginPrefix, theRoadHome, "The Road Home",
                attack: 1, health: 1, "A young girl on a quest to return home with her friends.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, theRoadHome)
                .AddAbilities(YellowBrickRoad.ability)
                .AddSpecialAbilities(TheHomingInstinct.specialAbility, YellowBrick.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}