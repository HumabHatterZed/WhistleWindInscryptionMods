using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void NothingThereEgg_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialAbilityFledgling.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };
            CardHelper.CreateCard(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                0, 3, 3, 0,
                Resources.nothingThereEgg, Resources.nothingThereEgg_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, evolveName: "wstl_nothingThereFinal", riskLevel: 5);
        }
    }
}