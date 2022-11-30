using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BlueStar_O0393()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            LobotomyCardHelper.CreateCard(
                "wstl_blueStar", "Blue Star",
                "When this is over, let's meet again as stars.",
                atk: 0, hp: 4,
                blood: 4, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Aleph,
                evolveName: "wstl_blueStar2", numTurns: 2);
        }
    }
}