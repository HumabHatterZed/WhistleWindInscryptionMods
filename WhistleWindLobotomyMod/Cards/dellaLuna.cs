using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DellaLuna_D01105()
        {
            const string dellaLuna = "dellaLuna";

            NewCard(dellaLuna, "Il Pianto della Luna", "In reality, man despairs at [c:bR]the moon[c:].",
                attack: 2, health: 7, blood: 3)
                .SetPortraits(dellaLuna)
                .AddAbilities(GroupHealer.ability)
                .AddTribes(Tribe.Bird)
                .SetDefaultEvolutionName("Il Pianto della Luna Maggiore")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Donator);
        }
    }
}