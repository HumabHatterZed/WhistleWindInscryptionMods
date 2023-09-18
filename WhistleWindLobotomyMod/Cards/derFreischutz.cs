using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DerFreischutz_F0169()
        {
            const string derFreischutz = "derFreischutz";

            NewCard(derFreischutz, "Der Freischütz", "A friendly hunter to some, a cruel gunsman to others. His bullets always hit their mark.",
                attack: 1, health: 1, blood: 2)
                .SetPortraits(derFreischutz)
                .AddAbilities(Ability.Sniper, Ability.SplitStrike)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("Der Ältere Freischütz")
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.He);
        }
    }
}