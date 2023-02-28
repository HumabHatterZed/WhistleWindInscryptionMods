using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TodaysShyLook_O0192()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopyOnDeath
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TodaysExpression.specialAbility
            };
            CreateCard(
                "wstl_todaysShyLook", "Today's Shy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLook, Artwork.todaysShyLook_emission, Artwork.todaysShyLook_pixel,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth,
                evolveName: "[name]Tomorrow's Shy Look");
            CreateCard(
                "wstl_todaysShyLookNeutral", "Today's Shy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLook, Artwork.todaysShyLook_emission, Artwork.todaysShyLook_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "[name]Tomorrow's Shy Look");
            CreateCard(
                "wstl_todaysShyLookHappy", "Today's Happy Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 1, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLookHappy, Artwork.todaysShyLookHappy_emission, Artwork.todaysShyLookHappy_pixel,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "[name]Tomorrow's Happy Look");
            CreateCard(
                "wstl_todaysShyLookAngry", "Today's Angry Look",
                "An indecisive creature. Her expression is different whenever you draw her.",
                atk: 2, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.todaysShyLookAngry, Artwork.todaysShyLookAngry_emission, Artwork.todaysShyLookAngry_pixel,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "[name]Tomorrow's Angry Look");
        }
    }
}