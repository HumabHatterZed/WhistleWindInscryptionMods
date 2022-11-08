using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 0, hp: 1,
                blood: 0, bones: 4, energy: 0,
                Artwork.namelessFetus, Artwork.namelessFetus_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}