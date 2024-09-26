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

            CardInfo infoO = CardManager.New(pluginPrefix, doubtO, "Doubt O",
                attack: 2, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(ModAssembly, doubtO)
                .SetTitle(ModAssembly, "doubtO_title.png")
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .AddTraits(Ordeal)
                .Build();

            CardInfo infoY = CardManager.New(pluginPrefix, doubtY, "Doubt Y",
                attack: 1, health: 3)
                .SetEnergyCost(3)
                .SetPortraits(ModAssembly, doubtY)
                .SetTitle(ModAssembly, "doubtY_title.png")
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoO, 1)
                .AddTraits(Ordeal)
                .Build();

            CardInfo infoB = CardManager.New(pluginPrefix, doubtB, "Doubt B",
                attack: 1, health: 2)
                .SetEnergyCost(2)
                .SetPortraits(ModAssembly, doubtB)
                .SetTitle(ModAssembly, "doubtB_title.png")
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoY, 1)
                .AddTraits(Ordeal)
                .Build();

            CardManager.New(pluginPrefix, doubtA, "Doubt A",
                attack: 1, health: 1)
                .SetEnergyCost(2)
                .SetPortraits(ModAssembly, doubtA)
                .SetTitle(ModAssembly, "doubtA_title.png")
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoB, 1)
                .AddTraits(Ordeal)
                .Build();

            CardManager.New(pluginPrefix, processUnderstanding, "Process of Understanding",
                attack: 3, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(ModAssembly, processUnderstanding)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .AddTraits(Ordeal)
                .Build();

            CardManager.New(pluginPrefix, whereWeReach, "Where We Must Reach",
                attack: 0, health: 15)
                .SetEnergyCost(5)
                .SetPortraits(ModAssembly, whereWeReach)
                .AddAbilities(Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .AddTraits(Ordeal, Trait.Uncuttable, Trait.Structure)
                .Build();

            CardManager.New(pluginPrefix, lastHelix, "Last Helix",
                attack: 1, health: 35)
                .SetEnergyCost(6)
                .SetPortraits(ModAssembly, lastHelix)
                .AddAbilities(Piercing.ability, Ability.MadeOfStone)
                .AddTribes(TribeMechanical)
                .AddTraits(Ordeal, Trait.Uncuttable, Trait.Structure, Trait.Giant)
                .Build();
        }
    }
}