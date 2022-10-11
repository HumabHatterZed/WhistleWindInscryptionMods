using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_LuminousBracelet_O0995()
        {
            List<Ability> abilities = new()
            {
                GreedyHealing.ability,
                TargetGainSigils.ability
            };

            CardHelper.CreateCard(
                "wstl_luminousBracelet", "Luminous Bracelet",
                "A bracelet that will heal those nearby. It does not forgive the greedy.",
                0, 0, 0, 3,
                Artwork.luminousBracelet, Artwork.luminousBracelet_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth,
                spellType: CardHelper.SpellType.TargetedSigils);
        }
    }
}