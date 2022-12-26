using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NamelessFetus_O0115()
        {
            List<Ability> abilities = new()
            {
                Aggravating.ability,
                Ability.PreventAttack
            };
            LobotomyCardHelper.CreateCard(
                "wstl_namelessFetusAwake", "Nameless Fetus",
                "Only a sacrifice will stop its piercing wails.",
                atk: 0, hp: 1,
                blood: 0, bones: 3, energy: 0,
                Artwork.namelessFetusAwake, Artwork.namelessFetusAwake,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());

            abilities = new()
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
            LobotomyCardHelper.CreateCard(
                "wstl_namelessFetus", "Nameless Fetus",
                "A neverending supply a blood. Just don't wake it.",
                atk: 0, hp: 1,
                blood: 0, bones: 3, energy: 0,
                Artwork.namelessFetus, Artwork.namelessFetus_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He);
        }
    }
}