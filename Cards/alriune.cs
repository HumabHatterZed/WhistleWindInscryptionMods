using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Alriune_T0453()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };

            CardHelper.CreateCard(
                "wstl_alriune", "Alriune",
                "A doll yearning to be a human. A human yearning to be a doll.",
                4, 5, 3, 0,
                Artwork.alriune, Artwork.alriune_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}