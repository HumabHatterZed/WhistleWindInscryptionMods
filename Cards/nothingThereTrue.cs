using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void NothingThereTrue_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                _AbilityHelper.specialAbility
            };

            List<Tribe> tribes = new()
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };

            WstlUtils.Add(
                "wstl_nothingThereTrue", "Nothing There",
                "What is that?",
                3, 3, 2, 0,
                Resources.nothingThereTrue, Resources.nothingThereTrue_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances, evolveName: "wstl_nothingThereEgg");
        }
    }
}