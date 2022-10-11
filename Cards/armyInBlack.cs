using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ArmyInBlack_D01106()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability,
                Ability.Brittle
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.NonChoice
            };

            CardHelper.CreateCard(
                "wstl_armyInBlack", "Army in Black",
                "Duty-bound.",
                2, 1, 0, 0,
                Artwork.armyInBlack, Artwork.armyInBlack_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}