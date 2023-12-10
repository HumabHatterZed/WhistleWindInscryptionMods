using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WeCanChangeAnything_T0985()
        {
            const string weCanChangeAnything = "weCanChangeAnything";

            NewCard(weCanChangeAnything, "We Can Change Anything", "Whatever you're dissatisfied with, this machine will fix it. You just have to step inside.",
                attack: 1, health: 1, blood: 1, temple: CardTemple.Tech)
                .SetPortraits(weCanChangeAnything)
                .AddAbilities(Grinder.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName("We Will Change Everything")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}