using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Nosferatu_F01113()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability,
                Bloodfiend.ability
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard
            };

            CardHelper.CreateCard(
                "wstl_nosferatu", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                2, 1, 2, 0,
                Artwork.nosferatu, Artwork.nosferatu_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: metaTypes);
        }
    }
}