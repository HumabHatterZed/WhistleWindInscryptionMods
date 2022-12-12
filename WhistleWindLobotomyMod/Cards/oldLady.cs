using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_OldLady_O0112()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };
            LobotomyCardHelper.CreateCard(
                "wstl_oldLady", "Old Lady",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                atk: 1, hp: 2,
                blood: 0, bones: 2, energy: 0,
                Artwork.oldLady, Artwork.oldLady_emission, pixelTexture: Artwork.oldLady_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth,
                customTribe: TribeHumanoid);
        }
    }
}