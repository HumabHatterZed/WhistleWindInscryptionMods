using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_PunishingBird_O0256()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Punisher.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            CardHelper.CreateCard(
                "wstl_punishingBird", "Punishing Bird",
                "A small bird on a mission to punish evildoers.",
                1, 1, 1, 0,
                Artwork.punishingBird, Artwork.punishingBird_emission, pixelTexture: Artwork.punishingBird_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, onePerDeck: true, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}