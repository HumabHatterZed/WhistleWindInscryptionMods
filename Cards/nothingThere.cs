using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void NothingThere_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Reach
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                NothingThere.specialAbility
            };
            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };

            WstlUtils.Add(
                "wstl_nothingThere", "Yumi",
                "I don't remember this challenger...",
                1, 1, 2, 0,
                Resources.nothingThere, Resources.nothingThere_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                isRare: true, onePerDeck: true);
        }
    }
}