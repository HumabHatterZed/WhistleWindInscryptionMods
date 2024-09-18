using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
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

            CardManager.New(pluginPrefix, derFreischutz, "Der Freischütz",
                attack: 2, health: 2, "A friendly hunter to some, a cruel gunsman to others. His bullets always hit their mark.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, derFreischutz)
                .AddAbilities(Ability.Sniper, Persistent.ability)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("Der Ältere Freischütz")
                .Build(CardHelper.CardType.Rare, RiskLevel.He, true);
        }
    }
}