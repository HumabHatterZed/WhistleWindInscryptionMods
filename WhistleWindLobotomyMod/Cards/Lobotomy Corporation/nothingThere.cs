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

            CardInfo nothingThereFinalCard = NewCard(
                nothingThereFinal, nothingName,
                attack: 8, health: 8, blood: 4)
                .SetPortraits(nothingThereFinal)
                .AddAbilities(Piercing.ability, Persistent.ability)
                .SetDefaultEvolutionName(nothingName)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            CardInfo nothingThereEggCard = NewCard(
                nothingThereEgg, "An Egg",
                attack: 0, health: 3, blood: 2)
                .SetPortraits(nothingThereEgg)
                .AddAbilities(abilities)
                .SetEvolve(nothingThereFinalCard, 1)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            NewCard(
                nothingThereTrue, nothingName,
                attack: 3, health: 3, blood: 2)
                .SetPortraits(nothingThereTrue)
                .AddAbilities(abilities)
                .AddTribes(Tribe.Canine, Tribe.Hooved, Tribe.Reptile)
                .SetEvolve(nothingThereEggCard, 1)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            NewCard(nothingThere, "Yumi", "I don't remember this challenger...",
                attack: 1, health: 1, blood: 2)
                .SetPortraits(nothingThere)
                .AddAbilities(abilities)
                .AddSpecialAbilities(Mimicry.specialAbility)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
        }
    }
}