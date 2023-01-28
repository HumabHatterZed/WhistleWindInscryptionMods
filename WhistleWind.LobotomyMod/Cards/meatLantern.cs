using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MeatLantern_O0484()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Ability.Reach
            };
            LobotomyCardHelper.CreateCard(
                "wstl_meatLantern", "Meat Lantern",
                "A beautiful flower attached to a mysterious creature.",
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.meatLantern, Artwork.meatLantern_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth);
        }
    }
}