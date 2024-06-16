using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowQueen_F0137()
        {
            const string snowQueen = "snowQueen";

            NewCard(snowQueen, "The Snow Queen", "A queen from far away. Those who enter her palace never leave.",
                attack: 2, health: 2, blood: 2, temple: CardTemple.Wizard)
                .SetPortraits(snowQueen)
                .AddAbilities(FrostRuler.ability)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("The Snow Empress")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}