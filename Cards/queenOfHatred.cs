using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void QueenOfHatred_O0104()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialAbilityFledgling.specialAbility
            };
            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };
            CardHelper.CreateCard(
                "wstl_queenOfHatred", "The Queen of Hatred",
                "Heroes exist to fight evil. In its absence, they must create it.",
                7, 2, 1, 0,
                Resources.queenOfHatred, Resources.queenOfHatred_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                onePerDeck: true, riskLevel: 4);
        }
    }
}