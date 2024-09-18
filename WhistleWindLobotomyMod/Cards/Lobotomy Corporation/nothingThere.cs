using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NothingThere_O0620()
        {
            const string nothingName = "Nothing There";
            const string nothingThere = "nothingThere";
            const string nothingThereTrue = "nothingThereTrue";
            const string nothingThereEgg = "nothingThereEgg";
            const string nothingThereFinal = "nothingThereFinal";
            Ability[] abilities = new[] { Ability.Evolve };

            CardInfo nothingThereFinalCard = CardManager.New(pluginPrefix, 
                nothingThereFinal, nothingName,
                attack: 8, health: 8)
                .SetBloodCost(4)
                .SetPortraits(ModAssembly, nothingThereFinal)
                .AddAbilities(Piercing.ability, Persistent.ability)
                .SetDefaultEvolutionName(nothingName)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardInfo nothingThereEggCard = CardManager.New(pluginPrefix, 
                nothingThereEgg, "An Egg",
                attack: 0, health: 3)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, nothingThereEgg)
                .AddAbilities(abilities)
                .SetEvolve(nothingThereFinalCard, 1)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(pluginPrefix, 
                nothingThereTrue, nothingName,
                attack: 3, health: 3)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, nothingThereTrue)
                .AddAbilities(abilities)
                .AddTribes(Tribe.Canine, Tribe.Hooved, Tribe.Reptile)
                .SetEvolve(nothingThereEggCard, 1)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(pluginPrefix, nothingThere, "Yumi",
                attack: 1, health: 1, "I don't remember this challenger...")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, nothingThere)
                .AddAbilities(abilities)
                .AddSpecialAbilities(Mimicry.specialAbility)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}