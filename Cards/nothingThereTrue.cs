using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_NothingThereTrue_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomFledgling.specialAbility
            };
            List<Tribe> tribes = new()
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };

            CardHelper.CreateCard(
                "wstl_nothingThereTrue", "Nothing There",
                "What is that?",
                3, 3, 2, 0,
                Artwork.nothingThereTrue, Artwork.nothingThereTrue_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: new(),
                choiceType: CardHelper.ChoiceType.Rare, metaType: CardHelper.MetaType.NonChoice,
                evolveName: "wstl_nothingThereEgg");
        }
    }
}