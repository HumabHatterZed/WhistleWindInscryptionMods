using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HonouredMonk_D01110()
        {
            const string honouredMonk = "honouredMonk";
            const string cloudedMonk = "cloudedMonk";
            Tribe[] tribes = new[] { TribeAnthropoid };
            CardInfo cloudedMonkCard = NewCard(
                cloudedMonk,
                "Clouded Monk",
                attack: 4, health: 3, blood: 3)
                .SetPortraits(cloudedMonk)
                .AddTribes(tribes);

            CardInfo honouredMonkCard = NewCard(
                honouredMonk,
                "Honoured Monk",
                "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...",
                attack: 2, health: 1, blood: 2)
                .SetPortraits(cloudedMonk)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_cloudedMonk");

            CreateCard(cloudedMonkCard);
            CreateCard(honouredMonkCard, CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Donator);
        }
    }
}