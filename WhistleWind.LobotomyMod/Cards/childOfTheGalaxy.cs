using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ChildOfTheGalaxy_O0155()
        {
            List<Ability> abilities = new()
            {
                FlagBearer.ability,
                Ability.BoneDigger
            };
            LobotomyCardHelper.CreateCard(
                "wstl_childOfTheGalaxy", "Child of the Galaxy",
                "A small child longing for a friend. Will you be his?",
                atk: 1, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.childOfTheGalaxy, Artwork.childOfTheGalaxy_emission, pixelTexture: Artwork.childOfTheGalaxy_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                customTribe: TribeHumanoid);
        }
    }
}