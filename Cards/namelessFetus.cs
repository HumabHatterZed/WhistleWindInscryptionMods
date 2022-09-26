using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void NamelessFetus_O0115()
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
                Resources.namelessFetus, Resources.namelessFetus_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                isChoice: true, riskLevel: 3);
        }
    }
}