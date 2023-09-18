using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleHeretic_T0346()
        {
            const string apostleHeretic = "apostleHeretic";
            NewCard(apostleHeretic, "Heretic",
                attack: 0, health: 7)
                .SetPortraits(apostleHeretic)
                .AddAbilities(Confession.ability)
                .AddTraits(Trait.Uncuttable, TraitApostle)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .Build(cardType: ModCardType.EventCard);
        }
    }
}