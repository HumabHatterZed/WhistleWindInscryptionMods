using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_NamelessFetus_O0115()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood,
                Ability.Sacrificial
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Syrinx.specialAbility
            };
            List<Trait> traits = new()
            {
                Trait.Goat
            };

            CardHelper.CreateCard(
                "wstl_namelessFetus", "Nameless Fetus",
                "A neverending supply a blood. Just don't wake it.",
                0, 1, 0, 5,
                Artwork.namelessFetus, Artwork.namelessFetus_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}