using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MountainOfBodies2_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.NonChoice
            };

            CardHelper.CreateCard(
                "wstl_mountainOfBodies2", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                3, 1, 3, 0,
                Artwork.mountainOfBodies2, Artwork.mountainOfBodies2_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}