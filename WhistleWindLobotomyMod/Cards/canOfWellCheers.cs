using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CanOfWellCheers_F0552()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge
            };

            CardHelper.CreateCard(
                "wstl_canOfWellCheers", "Opened Can of WellCheers",
                "A vending machine dispensing ocean soda.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.canOfWellCheers, Artwork.canOfWellCheers_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Zayin,
                iceCubeName: "wstl_SKELETON_SHRIMP");
        }
    }
}