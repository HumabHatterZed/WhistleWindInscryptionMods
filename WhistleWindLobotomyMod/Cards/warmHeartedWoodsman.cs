using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WarmHeartedWoodsman_F0532()
        {
            List<Ability> abilities = new() { Woodcutter.ability };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_warmHeartedWoodsman", "Warm-Hearted Woodsman",
                "A tin woodsman on the search for a heart. Perhaps you can give him yours.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.warmHeartedWoodsman, Artwork.warmHeartedWoodsman_emission, pixelTexture: Artwork.warmHeartedWoodsman_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He);
        }
    }
}