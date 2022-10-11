using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TheRoadHome_F01114()
        {
            List<Ability> abilities = new()
            {
                CatLover.ability
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard
            };

            CardHelper.CreateCard(
                "wstl_theRoadHome", "The Road Home",
                "A young girl on a quest to return home with her feline friend.",
                1, 1, 1, 0,
                Artwork.theRoadHome, Artwork.theRoadHome_emission, pixelTexture: Artwork.theRoadHome_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He,
                metaTypes: metaTypes);
        }
    }
}