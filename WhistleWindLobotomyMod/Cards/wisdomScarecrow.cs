using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WisdomScarecrow_F0187()
        {
            const string wisdomScarecrow = "wisdomScarecrow";

            CardInfo wisdomScarecrowCard = NewCard(
                wisdomScarecrow,
                "Scarecrow Searching for Wisdom",
                "A hollow-headed scarecrow. Blood soaks its straw limbs.",
                attack: 1, health: 2, bones: 4)
                .SetPortraits(wisdomScarecrow)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(TraitEmeraldCity)
                .SetOnePerDeck();

            CreateCard(wisdomScarecrowCard, CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}