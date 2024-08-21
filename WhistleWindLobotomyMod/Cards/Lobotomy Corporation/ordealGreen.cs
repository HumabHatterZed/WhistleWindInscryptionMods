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
        private void Cards_GreenOrdeal()
        {
            const string doubtA = "doubtA";
            const string doubtB = "doubtB";
            const string doubtY = "doubtY";
            const string doubtO = "doubtO";
            const string processUnderstanding = "processUnderstanding";
            const string whereWeReach = "whereWeReach";
            const string lastHelix = "lastHelix";

            CardInfo infoO = NewCard(doubtO, "Doubt δ",
                attack: 2, health: 4, energy: 4)
                .SetPortraits(doubtO)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .Build();

            CardInfo infoY = NewCard(doubtY, "Doubt γ",
                attack: 1, health: 3, energy: 3)
                .SetPortraits(doubtY)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoO, 1)
                .Build();

            CardInfo infoB = NewCard(doubtB, "Doubt β",
                attack: 1, health: 2, energy: 2)
                .SetPortraits(doubtB)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoY, 1)
                .Build();

            NewCard(doubtA, "Doubt α",
                attack: 1, health: 1, energy: 2)
                .SetPortraits(doubtA)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoB, 1)
                .Build();

            NewCard(processUnderstanding, "Process of Understanding",
                attack: 3, health: 4, energy: 4)
                .SetPortraits(processUnderstanding)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .Build();

            NewCard(whereWeReach, "Where We Must Reach",
                attack: 0, health: 15, energy: 5)
                .SetPortraits(whereWeReach)
                .AddAbilities(Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .AddTraits(Trait.Structure)
                .Build();

            NewCard(lastHelix, "Last Helix",
                attack: 1, health: 35, energy: 6)
                .SetPortraits(lastHelix)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .AddTraits(Trait.Uncuttable, Trait.Structure, Trait.Giant)
                .Build();
        }
    }
}