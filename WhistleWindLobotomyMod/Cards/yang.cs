using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yang_O07103()
        {
            List<Ability> abilities = new()
            {
                Regenerator.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Concord.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                AlternateBattlePortrait.appearance
            };
            CreateCard(
                "wstl_yang", "Yang",
                "A white pendant that heals those nearby.",
                atk: 0, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.yang, Artwork.yang_emission,
                altTexture: Artwork.yangAlt, emissionAltTexture: Artwork.yangAlt_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}