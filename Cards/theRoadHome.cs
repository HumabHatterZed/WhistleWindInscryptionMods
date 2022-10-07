using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

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

            CardHelper.CreateCard(
                "wstl_theRoadHome", "The Road Home",
                "A young girl on a quest to return home with her feline friend.",
                1, 1, 1, 0,
                Resources.theRoadHome, Resources.theRoadHome_emission, gbcTexture: Resources.theRoadHome_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}