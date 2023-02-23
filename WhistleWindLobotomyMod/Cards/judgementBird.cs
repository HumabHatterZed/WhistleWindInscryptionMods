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
        private void Card_JudgementBird_O0262()
        {
            List<Ability> abilities = new()
            {
                Marksman.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            CreateCard(
                "wstl_judgementBird", "Judgement Bird",
                "A long bird that judges sinners with swift efficiency. It alone is above consequences.",
                atk: 1, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.judgementBird, Artwork.judgementBird_emission, pixelTexture: Artwork.judgementBird_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}