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
        private void Card_MirrorOfAdjustment_O0981()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialTriggeredAbility.Mirror
            };
            LobotomyCardHelper.CreateCard(
                "wstl_mirrorOfAdjustment", "The Mirror of Adjustment",
                "A mirror that reflects nothing on its surface.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.mirrorOfAdjustment, Artwork.mirrorOfAdjustment_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), statIcon: SpecialStatIcon.Mirror,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Zayin,
                metaTypes: CardHelper.CardMetaType.NoTerrainLayout);
        }
    }
}