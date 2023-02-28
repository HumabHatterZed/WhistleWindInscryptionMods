using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MirrorOfAdjustment_O0981()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
            CreateCard(
                "wstl_mirrorOfAdjustment", "The Mirror of Adjustment",
                "A mirror that reflects nothing on its surface.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.mirrorOfAdjustment, Artwork.mirrorOfAdjustment_emission, Artwork.mirrorOfAdjustment_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), statIcon: SpecialStatIcon.Mirror,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin,
                evolveName: "[name]The Elder Mirror of Adjustment", metaTypes: CardHelper.CardMetaType.NoTerrainLayout);
        }
    }
}