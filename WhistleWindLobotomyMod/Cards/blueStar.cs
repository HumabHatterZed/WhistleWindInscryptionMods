using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BlueStar_O0393()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve,
                Ability.AllStrike
            };
            List<Tribe> tribes = new() { TribeDivine };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                StarSound.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedEmission.appearance
            };
            CreateCard(
                "wstl_blueStar3", "Blue Star", "",
                atk: 4, hp: 4,
                blood: 4, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances,
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "wstl_blueStar");

            abilities = new() { Ability.Evolve, Idol.ability };

            CreateCard(
                "wstl_blueStar2", "Blue Star", "",
                atk: 0, hp: 4,
                blood: 3, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "wstl_blueStar3");

            abilities = new() { Ability.Evolve };

            CreateCard(
                "wstl_blueStar", "Blue Star",
                "When this is over, let's meet again as stars.",
                atk: 0, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Aleph,
                evolveName: "wstl_blueStar2");
        }
    }
}