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
        private void Card_FairyFestival_F0483()
        {
            const string fairyFestival = "fairyFestival";

            CardInfo fairyFestivalCard = NewCard(
                fairyFestival,
                "Fairy Festival",
                "Everything will be peaceful while you're under the fairies' care.",
                attack: 1, health: 2, blood: 1)
                .SetPortraits(fairyFestival)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeFae);

            CreateCard(fairyFestivalCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}