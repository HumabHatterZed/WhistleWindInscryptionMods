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
            string allAroundHelper = "allAroundHelper";
            string name = "All-Around Helper";
            string desc = "A murderous cleaning machine. Far nicer than a certain other...well, nevermind.";
            string evo = "All-Around Helper 2.0";

            CardManager.New(pluginPrefix, allAroundHelper, name,
                1, 2, desc)
                .SetEnergyCost(4)
                .SetPortraits(allAroundHelper)
                .AddAbilities(Ability.Strafe, Ability.SplitStrike)
                .SetDefaultEvolutionName(evo);

            if (Scrybes.P03Enabled)
            {
                NewCard(allAroundHelper, "All-Around Helper", "A murderous cleaning machine. Far nicer than a certain other...well, nevermind.",
                    attack: 1, health: 2, energy: 4, temple: CardTemple.Tech)
                    .SetPortraits(allAroundHelper)
                    .AddAbilities(Ability.Strafe, Ability.SplitStrike)
                    .AddTribes(TribeMechanical)
                    .SetDefaultEvolutionName($"All-Around Helper 2.0")
                    .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
            }
        }
    }
}