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
            CreateCard(
                "wstl_punishingBird", "Punishing Bird",
                "A small bird on a mission to punish evildoers.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.punishingBird, Artwork.punishingBird_emission, pixelTexture: Artwork.punishingBird_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, onePerDeck: true, riskLevel: RiskLevel.Teth);
        }
    }
}