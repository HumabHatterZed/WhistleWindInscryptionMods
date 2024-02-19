using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DontTouchMe_O0547()
        {
            const string dontTouchMe = "dontTouchMe";

            NewCard(dontTouchMe, "Don't Touch Me", "Don't touch it.",
                attack: 0, health: 1, energy: 2, temple: CardTemple.Tech)
                .SetPortraits(dontTouchMe)
                .AddAbilities(Punisher.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName("Please Don't Touch Me")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}