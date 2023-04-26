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
        private void Card_MountainOfBodies_T0175()
        {
            List<Ability> abilities = new() { Assimilator.ability };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Smile.specialAbility
            };
            CreateCard(
                "wstl_mountainOfBodies3", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                atk: 5, hp: 1,
                blood: 3, bones: 0, energy: 0,
                Artwork.mountainOfBodies3, Artwork.mountainOfBodies3_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "{0}");
            CreateCard(
                "wstl_mountainOfBodies2", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                atk: 3, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.mountainOfBodies2, Artwork.mountainOfBodies2_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "{0}");
            CreateCard(
                "wstl_mountainOfBodies", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.mountainOfBodies, Artwork.mountainOfBodies_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), choiceType: CardHelper.CardChoiceType.Rare,
                evolveName: "{0}");
        }
    }
}