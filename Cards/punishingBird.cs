using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void PunishingBird_O0256()
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
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            CardHelper.CreateCard(
                "wstl_punishingBird", "Punishing Bird",
                "A small bird on a mission to punish evildoers.",
                1, 1, 1, 0,
                Resources.punishingBird, Resources.punishingBird_emission, gbcTexture: Resources.punishingBird_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                isChoice: true, onePerDeck: true, riskLevel: 2);
        }
    }
}