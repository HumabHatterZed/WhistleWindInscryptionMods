using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleSpear_T0346()
        {
            const string spearName = "Spear Apostle";
            const string apostleSpear = "apostleSpear";
            const string apostleSpearDown = "apostleSpearDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { TraitApostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            NewCard(apostleSpear, spearName,
                attack: 4, health: 6)
                .SetPortraits(apostleSpear)
                .AddAbilities(Piercing.ability, Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);

            NewCard(apostleSpearDown, spearName,
                attack: 0, health: 1)
                .SetPortraits(apostleSpearDown)
                .AddAbilities(Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);
        }
    }
}