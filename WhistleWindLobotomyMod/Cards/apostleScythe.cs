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
        private void Card_ApostleScythe_T0346()
        {
            const string scytheName = "Scythe Apostle";
            const string apostleScythe = "apostleScythe";
            const string apostleScytheDown = "apostleScytheDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { TraitApostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardInfo apostle = NewCard(
                apostleScythe,
                scytheName,
                attack: 2, health: 6)
                .SetPortraits(apostleScythe)
                .AddAbilities(Ability.DoubleStrike, Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances);

            CardInfo apostleDown = NewCard(
                apostleScytheDown,
                scytheName,
                attack: 0, health: 1)
                .SetPortraits(apostleScytheDown)
                .AddAbilities(Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances);

            CreateCard(apostle, cardType: ModCardType.EventCard);
            CreateCard(apostleDown, cardType: ModCardType.EventCard);
        }
    }
}