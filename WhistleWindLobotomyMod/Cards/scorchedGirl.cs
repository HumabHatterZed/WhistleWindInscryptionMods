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
        private void Card_ScorchedGirl_F0102()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_scorchedGirl", "Scorched Girl",
                "Though there's nothing left to burn, the fire won't go out.",
                atk: 1, hp: 1,
                blood: 0, bones: 2, energy: 0,
                Artwork.scorchedGirl, Artwork.scorchedGirl_emission, pixelTexture: Artwork.scorchedGirl_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth);
        }
    }
}