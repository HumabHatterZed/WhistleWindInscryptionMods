using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void JudgementBird_O0262()
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
                Resources.judgementBird, Resources.judgementBird_emission, gbcTexture: Resources.judgementBird_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, onePerDeck: true, riskLevel: 4);
        }
    }
}