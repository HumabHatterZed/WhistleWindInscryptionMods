using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NothingThere_O0620()
        {
            List<Ability> abilities = new() { Ability.Evolve };
            List<SpecialTriggeredAbility> specialAbilities = new() { CustomEvolveHelper.specialAbility };

            List<Tribe> tribes = new()
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };
            CreateCard(
                "wstl_nothingThereFinal", "Nothing There",
                "A grotesque attempt at mimicry. Pray it does not improve its disguise.",
                atk: 9, hp: 9,
                blood: 4, bones: 0, energy: 0,
                Artwork.nothingThereFinal, Artwork.nothingThereFinal_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new() { TribeAnthropoid }, traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice);

            CreateCard(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                atk: 0, hp: 3,
                blood: 3, bones: 0, energy: 0,
                Artwork.nothingThereEgg, Artwork.nothingThereEgg_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "wstl_nothingThereFinal");

            CreateCard(
                "wstl_nothingThereTrue", "Nothing There",
                "What is that?",
                atk: 3, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.nothingThereTrue, Artwork.nothingThereTrue_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "wstl_nothingThereEgg");

            specialAbilities = new() { Mimicry.specialAbility };

            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };
            CreateCard(
                "wstl_nothingThere", "Yumi",
                "I don't remember this challenger...",
                atk: 1, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.nothingThere, Artwork.nothingThere_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Aleph);
        }
    }
}