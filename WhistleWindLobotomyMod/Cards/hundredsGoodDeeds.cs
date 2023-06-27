using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private const string oneSinName = "One Sin and Hundreds of Good Deeds";
        private const string oneSin = "oneSin";
        private void Card_HundredsGoodDeeds_O0303()
        {
            const string hundredsGoodDeeds = "hundredsGoodDeeds";

            CardInfo hundredsGoodDeedsCard = NewCard(
                hundredsGoodDeeds,
                oneSinName,
                attack: 0, health: 77)
                .SetPortraits(oneSin, "hundredsGoodDeeds_emission", "hundredsGoodDeeds_pixel")
                .AddAbilities(Confession.ability)
                .AddTraits(Trait.Uncuttable, TraitApostle)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetHideStats();

            CreateCard(hundredsGoodDeedsCard, CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.EventCard);
        }
    }
}