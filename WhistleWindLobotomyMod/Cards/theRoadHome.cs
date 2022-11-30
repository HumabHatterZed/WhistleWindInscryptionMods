using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheRoadHome_F01114()
        {
            List<Ability> abilities = new()
            {
                CatLover.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_theRoadHome", "The Road Home",
                "A young girl on a quest to return home with her feline friend.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.theRoadHome, Artwork.theRoadHome_emission, pixelTexture: Artwork.theRoadHome_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                modTypes: LobotomyCardHelper.ModCardType.Ruina);
        }
    }
}