using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_VoidDream_T0299()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Ability.Evolve
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };
            CardHelper.CreateCard(
                "wstl_voidDream", "Void Dream",
                "A sleeping goat. Or is it a sheep?",
                1, 1, 1, 0,
                Artwork.voidDream, Artwork.voidDream_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth,
                evolveName: "wstl_voidDreamRooster");
        }
    }
}