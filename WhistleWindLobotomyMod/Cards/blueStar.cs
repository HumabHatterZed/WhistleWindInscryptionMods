using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        Tribe customTribe = TribeDivine;
        private void Card_BlueStar_O0393()
        {
            List<Ability> thirdFormeAbilities = new()
            {
                Ability.Evolve,
                Ability.AllStrike
            };
            List<Ability> secondFormeAbilities = new()
            {
                Ability.Evolve,
                Idol.ability
            };
            List<Ability> baseAbility = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                StarSound.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                Forced.appearance
            };
            LobotomyCardHelper.CreateCard(
                "wstl_blueStar3", "Blue Star", "",
                atk: 6, hp: 4,
                blood: 4, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: thirdFormeAbilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances,
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "wstl_blueStar", customTribe: customTribe);
            LobotomyCardHelper.CreateCard(
                "wstl_blueStar2", "Blue Star", "",
                atk: 0, hp: 4,
                blood: 3, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: secondFormeAbilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "wstl_blueStar3", customTribe: customTribe);
            LobotomyCardHelper.CreateCard(
                "wstl_blueStar", "Blue Star",
                "When this is over, let's meet again as stars.",
                atk: 0, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: baseAbility, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Aleph,
                evolveName: "wstl_blueStar2", customTribe: customTribe);
        }
    }
}