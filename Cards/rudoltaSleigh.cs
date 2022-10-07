using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_RudoltaSleigh_F0249()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                GiftGiver.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };

            CardHelper.CreateCard(
                "wstl_rudoltaSleigh", "Rudolta of the Sleigh",
                "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.",
                2, 3, 2, 0,
                Artwork.rudoltaSleigh, Artwork.rudoltaSleigh_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}