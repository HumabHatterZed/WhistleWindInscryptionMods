using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

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
            CreateCard(
                "wstl_theRoadHome", "The Road Home",
                "A young girl on a quest to return home with her friends.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.theRoadHome, Artwork.theRoadHome_emission, pixelTexture: Artwork.theRoadHome_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                modTypes: ModCardType.Ruina, customTribe: TribeHumanoid);
        }
    }
}