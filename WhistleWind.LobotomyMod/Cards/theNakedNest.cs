using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheNakedNest_O0274()
        {
            List<Ability> abilities = new()
            {
                SerpentsNest.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            LobotomyCardHelper.CreateCard(
                "wstl_theNakedNest", "The Naked Nest",
                "They can enter your body through any aperture.",
                atk: 0, hp: 3,
                blood: 0, bones: 5, energy: 0,
                Artwork.theNakedNest, Artwork.theNakedNest_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}