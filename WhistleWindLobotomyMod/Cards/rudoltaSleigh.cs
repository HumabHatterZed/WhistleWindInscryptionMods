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
            CreateCard(
                "wstl_rudoltaSleigh", "Rudolta of the Sleigh",
                "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.rudoltaSleigh, Artwork.rudoltaSleigh_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He);
        }
    }
}