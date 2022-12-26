using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NothingThereTrue_O0620()
        {
            List<Ability> abilities = new() { Ability.Evolve };
            List<SpecialTriggeredAbility> specialAbilities = new() { CustomEvolveHelper.specialAbility };
            List<Tribe> tribes = new()
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };

            LobotomyCardHelper.CreateCard(
                "wstl_nothingThereFinal", "Nothing There",
                "A grotesque attempt at mimicry. Pray it does not improve its disguise.",
                atk: 9, hp: 9,
                blood: 4, bones: 0, energy: 0,
                Artwork.nothingThereFinal, Artwork.nothingThereFinal_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                customTribe: TribeHumanoid);

            LobotomyCardHelper.CreateCard(
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

            LobotomyCardHelper.CreateCard(
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
        }
    }
}