using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MeltingLove_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.DonatorClass
            };

            CardHelper.CreateCard(
                "wstl_meltingLove", "Melting Love",
                "Don't let your beasts get too close now.",
                4, 2, 3, 0,
                Artwork.meltingLove, Artwork.meltingLove_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Aleph,
                metaTypes: metaTypes);
        }
    }
}