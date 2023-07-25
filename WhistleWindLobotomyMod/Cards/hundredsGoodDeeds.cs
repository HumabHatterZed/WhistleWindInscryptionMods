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
        private void Card_HundredsGoodDeeds_O0303()
        {
            const string hundredsGoodDeeds = "hundredsGoodDeeds";

            NewCard(hundredsGoodDeeds, oneSinName,
                attack: 0, health: 77)
                .SetPortraits(oneSin, "hundredsGoodDeeds_emission", "hundredsGoodDeeds_pixel")
                .AddAbilities(Confession.ability)
                .AddTraits(Trait.Uncuttable, TraitApostle)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetHideStats()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.EventCard);
        }
    }
}