using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BeautyAndBeast_O0244()
        {
            List<Ability> abilities = new()
            {
                Cursed.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved,
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_beautyAndBeast", "Beauty and the Beast",
                "A pitiable creature. Death would be a mercy for it.",
                1, 1, 1, 0,
                Artwork.beautyAndBeast, Artwork.beautyAndBeast_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}