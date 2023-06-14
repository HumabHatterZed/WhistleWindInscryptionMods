using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
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
                nothingThereFinal,
                nothingName,
                attack: 9, health: 9, blood: 4)
                .SetPortraits(nothingThereFinal)
                .AddAbilities(Piercing.ability, ThickSkin.ability, ThickSkin.ability)
                .SetEvolveInfo("{0}")
                .SetOnePerDeck();

            CardInfo nothingThereEggCard = NewCard(
                nothingThereEgg,
                "An Egg",
                attack: 0, health: 3, blood: 2)
                .SetPortraits(nothingThereEgg)
                .AddAbilities(abilities)
                .SetEvolveInfo("wstl_nothingThereFinal")
                .SetOnePerDeck();

            CardInfo nothingThereTrueCard = NewCard(
                nothingThereTrue,
                nothingName,
                attack: 3, health: 3, blood: 2)
                .SetPortraits(nothingThereTrue)
                .AddAbilities(abilities)
                .AddTribes(Tribe.Canine, Tribe.Hooved, Tribe.Reptile)
                .SetEvolveInfo("wstl_nothingThereEgg")
                .SetOnePerDeck();

            CardInfo nothingThereCard = NewCard(
                nothingThere,
                "Yumi",
                "I don't remember this challenger...",
                attack: 1, health: 1, blood: 2)
                .SetPortraits(nothingThere)
                .AddAbilities(abilities)
                .AddSpecialAbilities(Mimicry.specialAbility)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck();

            CreateCard(nothingThereFinalCard, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(nothingThereEggCard, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(nothingThereTrueCard, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(nothingThereCard, CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
        }
    }
}