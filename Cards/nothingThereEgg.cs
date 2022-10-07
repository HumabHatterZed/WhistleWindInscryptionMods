using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_NothingThereEgg_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomFledgling.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                0, 3, 3, 0,
                Artwork.nothingThereEgg, Artwork.nothingThereEgg_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Rare, metaType: CardHelper.MetaType.NonChoice,
                evolveName: "wstl_nothingThereFinal");
        }
    }
}