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
        private void Card_MeatLantern_O0484()
        {
            List<Ability> abilities = new()
            {
                Ability.Reach,
                Punisher.ability
            };

            CreateCard(
                "wstl_meatLantern", "Meat Lantern",
                "A beautiful flower attached to a mysterious creature.",
                atk: 1, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.meatLantern, Artwork.meatLantern_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth);
        }
    }
}