using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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

            CardHelper.CreateCard(
                "wstl_judgementBird", "Judgement Bird",
                "A long bird that judges sinners with swift efficiency. Only it is above consequences.",
                1, 1, 2, 0,
                Artwork.judgementBird, Artwork.judgementBird_emission, pixelTexture: Artwork.judgementBird_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                onePerDeck: true,
                riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}