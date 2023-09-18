using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleGuardian_T0346()
        {
            const string guardianName = "Guardian Apostle";
            const string apostleGuardian = "apostleGuardian";
            const string apostleGuardianDown = "apostleGuardianDown";
            Ability[] abilities = new[] { Apostle.ability };
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { TraitApostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            NewCard(apostleGuardian, guardianName,
                attack: 4, health: 6)
                .SetPortraits(apostleGuardian)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);

            NewCard(apostleGuardianDown, guardianName,
                attack: 0, health: 1)
                .SetPortraits(apostleGuardianDown)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);
        }
    }
}