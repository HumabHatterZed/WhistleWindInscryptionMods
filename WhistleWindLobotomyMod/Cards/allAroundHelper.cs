using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_AllAroundHelper_T0541()
        {
            const string allAroundHelper = "allAroundHelper";
            CardInfo helper = NewCard(
                cardName: allAroundHelper, displayName: "All-Around Helper",
                description: "A murderous machine originally built to do chores. It reminds me of someone I know.",
                attack: 1, health: 3, energy: 4)
                .SetPortraits(allAroundHelper)
                .AddAbilities(Ability.Strafe, Ability.SplitStrike)
                .AddTribes(TribeMechanical)
                .SetEvolveInfo("{0} 2.0");

            CreateCard(helper, CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}