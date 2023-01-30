using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheRoadHome_F01114()
        {
            List<Ability> abilities = new()
            {
                YellowBrickRoad.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TheHomingInstinct.specialAbility,
                YellowBrick.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_theRoadHome", "The Road Home",
                "A young girl on a quest to return home with her friends.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.theRoadHome, Artwork.theRoadHome_emission, pixelTexture: Artwork.theRoadHome_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                modTypes: LobotomyCardHelper.ModCardType.Ruina, customTribe: TribeHumanoid);
        }
    }
}